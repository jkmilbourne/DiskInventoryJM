using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiskInventory.Models;

namespace DiskInventory.Controllers
{
    public class DiskController : Controller
    {
        private disk_inventoryjmContext context { get; set; }

        public DiskController(disk_inventoryjmContext ctx)
        {
            context = ctx;
        }

        //Index method that lists all disks on database
        public IActionResult Index()
        {
            var disks = context.Disks
                .OrderBy(d => d.DiskName)
                .Include(g => g.GenreCodeNavigation)
                .Include(s => s.StatusCodeNavigation)
                .Include(d => d.DiskType)
                .ToList();
            return View(disks);
        }

        //Add method that runs on HttpGet and lists all disks on database
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.DiskTypes = context.DiskTypes.OrderBy(dt => dt.DiskTypeDesc).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.StatusDesc).ToList();
            ViewBag.Genres = context.Genres.OrderBy(g => g.GenreDesc).ToList();
            Disk newdisk = new Disk();
            newdisk.ReleaseDate = DateTime.Today;
            return View("Edit", newdisk);
        }

        //Edit method that runs on HttpGet and lists all disks on database
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Genres = context.Genres.OrderBy(g => g.GenreDesc).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.StatusDesc).ToList();
            ViewBag.DiskTypes = context.DiskTypes.OrderBy(dt => dt.DiskTypeDesc).ToList();
            var disk = context.Disks.Find(id);
            return View(disk);
        }

        //Edit method that uses stored procedures and either add a new disk if diskId is zero or edits passed disk on database
        [HttpPost]
        public IActionResult Edit(Disk disk)
        {
            if (ModelState.IsValid)
            {
                if (disk.DiskId == 0)
                {
                    //context.Disks.Add(disk);
                    context.Database.ExecuteSqlRaw("execute sp_ins_disk @p0, @p1, @p2, @p3, @p4", 
                        parameters: new[] { disk.DiskName, disk.ReleaseDate.ToString(), disk.StatusCode.ToString(), disk.GenreCode.ToString(), disk.DiskTypeId.ToString() });
                }
                else
                {
                    //context.Disks.Update(disk);
                    context.Database.ExecuteSqlRaw("execute sp_upd_disk @p0, @p1, @p2, @p3, @p4, @p5",
                        parameters: new[] { disk.DiskId.ToString(), disk.DiskName, disk.ReleaseDate.ToString(), disk.StatusCode.ToString(), disk.GenreCode.ToString(), disk.DiskTypeId.ToString() });
                }
                //context.SaveChanges();
                return RedirectToAction("Index", "Disk");
            }
            else
            {
                ViewBag.Action = (disk.DiskId == 0) ? "Add" : "Edit";
                ViewBag.Genres = context.Genres.OrderBy(g => g.GenreDesc).ToList();
                ViewBag.Statuses = context.Statuses.OrderBy(s => s.StatusDesc).ToList();
                ViewBag.DiskTypes = context.DiskTypes.OrderBy(dt => dt.DiskTypeDesc).ToList();
                return View(disk);
            }
        }

        //Delete method that runs on HttpGet
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var disk = context.Disks.Find(id);
            return View(disk);
        }

        //Delete method that uses stored procedures to delete the passed diskId
        [HttpPost]
        public IActionResult Delete(Disk disk)
        {
            //context.Disks.Remove(disk);
            //context.SaveChanges();
            context.Database.ExecuteSqlRaw("execute sp_del_disk @p0",
                        parameters: new[] { disk.DiskId.ToString() });
            return RedirectToAction("Index", "Disk");
        }
    }
}
