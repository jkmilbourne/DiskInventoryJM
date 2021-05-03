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
        //Create database context
        private disk_inventoryjmContext context { get; set; }

        public ArtistController(disk_inventoryjmContext ctx)
        {
            context = ctx;
        }

        // Index method to display list of artists on database
        public IActionResult Index()
        {
            var artists = context.Artists
                .OrderBy(a => a.ArtistLname)
                .ThenBy(a => a.ArtistFname)
                .Include(at => at.ArtistTypeCodeNavigation)
                .ToList();
            return View(artists);
        }

        // Add Method on HttpGet to list artist on database
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.ArtistTypeDesc).ToList();
            return View("Edit", new Artist());
        }

        //Edit method on HttpGet to list artists on database
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.ArtistTypeDesc).ToList();
            var artist = context.Artists.Find(id);
            return View(artist);
        }

        //Edit method on HttpPost that uses stored procedured to insert new artist if artistId is 0 or edits existing artist
        [HttpPost]
        public IActionResult Edit(Artist theartist)
        {
            if (ModelState.IsValid)
            {
                if (theartist.ArtistId == 0)
                {
                    //context.Artists.Add(theartist);
                    context.Database.ExecuteSqlRaw("execute sp_ins_artist @p0, @p1, @p2",
                        parameters: new[] { theartist.ArtistFname, theartist.ArtistLname, theartist.ArtistTypeCode.ToString() });
                }
                else
                {
                    //context.Artists.Update(theartist);
                    context.Database.ExecuteSqlRaw("execute sp_upd_artist @p0, @p1, @p2, @p3",
                        parameters: new[] { theartist.ArtistId.ToString(), theartist.ArtistFname, theartist.ArtistLname, theartist.ArtistTypeCode.ToString() });
                }
                //context.SaveChanges();
                return RedirectToAction("Index", "Artist");

            } else
            {
                ViewBag.Action = (theartist.ArtistId == 0) ? "Add" : "Edit";
                ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.ArtistTypeDesc).ToList();
                return View(theartist);
            }
        }

        //Delete method that runs on HttpGet
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var artist = context.Artists.Find(id);
            return View(artist);
        }

        //Delete method that runs on HttpPost and deletes the artist that is passed to it useing a stored procedure
        [HttpPost]
        public IActionResult Delete(Artist theartist)
        {
            //context.Artists.Remove(theartist);
            //context.SaveChanges();
            context.Database.ExecuteSqlRaw("execute sp_del_artist @p0",
                        parameters: new[] { theartist.ArtistId.ToString() });
            return RedirectToAction("Index", "Artist");
        }
    }
}
