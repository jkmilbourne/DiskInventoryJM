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
    }
}
