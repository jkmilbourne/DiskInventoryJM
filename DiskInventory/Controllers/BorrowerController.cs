using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiskInventory.Models;

namespace DiskInventory.Controllers
{
    public class BorrowerController : Controller
    {
        //Create database context
        private disk_inventoryjmContext context { get; set; }

        public BorrowerController(disk_inventoryjmContext ctx)
        {
            context = ctx;
        }

        //Index method that lists all borrowers on the database
        public IActionResult Index()
        {
            var borrowers = context.Borrowers
                .OrderBy(b => b.Lname)
                .ThenBy(b => b.Fname)
                .ToList();
            return View(borrowers);
        }

        //Add Method that runs and lists all borrowers from HttpGet request
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Borrower());
        }

        //Edit method that runs and lists all borrowers from HttpGet request
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }

        //Edit Method that uses stored procedures to either add a borrower if borrower id is 0 or edits that passed borrower
        [HttpPost]
        public IActionResult Edit(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                if (borrower.BorrowerId == 0)
                {
                    //context.Borrowers.Add(borrower);
                    context.Database.ExecuteSqlRaw("execute sp_ins_borrower @p0, @p1, @p2, @p3",
                        parameters: new[] { borrower.Fname, borrower.Mi, borrower.Lname, borrower.Phone.ToString() });
                }
                else
                {
                    //context.Borrowers.Update(borrower);
                    context.Database.ExecuteSqlRaw("execute sp_upd_borrower @p0, @p1, @p2, @p3, @p4",
                        parameters: new[] { borrower.BorrowerId.ToString(), borrower.Fname, borrower.Mi, borrower.Lname, borrower.Phone.ToString() });
                }
                //context.SaveChanges();
                return RedirectToAction("Index", "Borrower");
            }
            else
            {
                ViewBag.Action = (borrower.BorrowerId == 0) ? "Add" : "Edit";
                return View(borrower);
            }
        }

        //Delete method that runs on HttpGet request
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }

        //Delete method that uses a stored prodcedure to delete passed Borrower
        [HttpPost]
        public IActionResult Delete(Borrower borrower)
        {
            //context.Borrowers.Remove(borrower);
            //context.SaveChanges();
            context.Database.ExecuteSqlRaw("execute sp_del_borrower @p0",
                        parameters: new[] { borrower.BorrowerId.ToString() });
            return RedirectToAction("Index", "Artist");
        }
    }
}
