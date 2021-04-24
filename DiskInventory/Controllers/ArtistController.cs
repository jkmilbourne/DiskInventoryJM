using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiskInventory.Models;

namespace DiskInventory.Controllers
{
    public class ArtistController : Controller
    {
        private disk_inventoryjmContext context { get; set; }

        public ArtistController(disk_inventoryjmContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            var artists = context.Artists
                .OrderBy(a => a.ArtistLname)
                .ThenBy(a => a.ArtistFname)
                .ToList();
            return View(artists);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.ArtistTypeDesc).ToList();
            return View("Edit", new Artist());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.ArtistTypeDesc).ToList();
            var artist = context.Artists.Find(id);
            return View(artist);
        }

        [HttpPost]
        public IActionResult Edit(Artist theartist)
        {
            if (ModelState.IsValid)
            {
                if (theartist.ArtistId == 0)
                {
                    context.Artists.Add(theartist);
                }
                else
                {
                    context.Artists.Update(theartist);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Artist");

            } else
            {
                ViewBag.Action = (theartist.ArtistId == 0) ? "Add" : "Edit";
                ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.ArtistTypeDesc).ToList();
                return View(theartist);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var artist = context.Artists.Find(id);
            return View(artist);
        }
        [HttpPost]
        public IActionResult Delete(Artist theartist)
        {
            context.Artists.Remove(theartist);
            context.SaveChanges();
            return RedirectToAction("Index", "Artist");
        }
    }
}
