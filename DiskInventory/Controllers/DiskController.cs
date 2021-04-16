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
    }
}
