using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace DiskInventory.Controllers
{
    public class DiskHasBorrowerController : Controller
    {
        //Create context
        private disk_inventoryjmContext context { get; set; }

        public DiskHasBorrowerController(disk_inventoryjmContext ctx)
        {
            context = ctx;
        }

        //Index method that lists all info important for checkout process
        public IActionResult Index()
        {
            var diskHasBorrowers = context.DiskHasBorrowers
                .Include(d => d.Disk)
                .OrderBy(d => d.Disk.DiskName)
                .Include(b => b.Borrower)
                .ToList();
            return View(diskHasBorrowers);
        }

        //Method that adds new checkout to database
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Disks = context.Disks.OrderBy(d => d.DiskName).ToList();
            ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
            DiskHasBorrower newdiskHasBorrower = new DiskHasBorrower();
            newdiskHasBorrower.BorrowedDate = DateTime.Today;
            newdiskHasBorrower.DueDate = DateTime.Today.AddDays(30);
            return View("Edit", newdiskHasBorrower);
        }

        //Edit method that runs on HttpGet
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Disks = context.Disks.OrderBy(d => d.DiskName).ToList();
            ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
            var diskHasBorrower = context.DiskHasBorrowers.Find(id);
            return View(diskHasBorrower);
        }

        //Edit method that either adds a new checkout if diskhasborrowerid is 0 or edits the checkout that is passed
        [HttpPost]
        public IActionResult Edit(DiskHasBorrower diskHasBorrower)
        {
            if (ModelState.IsValid)
            {
                if (diskHasBorrower.DiskHasBorrowerId == 0)
                {
                    context.DiskHasBorrowers.Add(diskHasBorrower);
                }
                else
                {
                    context.DiskHasBorrowers.Update(diskHasBorrower);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "DiskHasBorrower");
            }
            else
            {
                ViewBag.Action = (diskHasBorrower.DiskHasBorrowerId == 0) ? "Add" : "Edit";
                ViewBag.Disks = context.Disks.OrderBy(d => d.DiskName).ToList();
                ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.Lname).ToList();
                return View(diskHasBorrower);
            }
        }

    }
}
