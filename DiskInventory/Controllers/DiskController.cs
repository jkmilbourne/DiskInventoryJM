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

        public IActionResult Index()
        {
            var disks = context.Disks
                .OrderBy(d => d.DiskName)
                .ToList();
            return View(disks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.DiskTypes = context.DiskTypes.OrderBy(dt => dt.DiskTypeDesc).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.StatusDesc).ToList();
            ViewBag.Genres = context.Genres.OrderBy(g => g.GenreDesc).ToList();
            return View("Edit", new Disk());
        }

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

        [HttpPost]
        public IActionResult Edit(Disk disk)
        {
            if (ModelState.IsValid)
            {
                if (disk.DiskId == 0)
                {
                    context.Disks.Add(disk);
                }
                else
                {
                    context.Disks.Update(disk);
                }
                context.SaveChanges();
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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var disk = context.Disks.Find(id);
            return View(disk);
        }

        [HttpPost]
        public IActionResult Delete(Disk disk)
        {
            context.Disks.Remove(disk);
            context.SaveChanges();
            return RedirectToAction("Index", "Disk");
        }
    }
}
