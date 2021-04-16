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
        private disk_inventoryjmContext context { get; set; }

        public BorrowerController(disk_inventoryjmContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            var borrowers = context.Borrowers.ToList();
            return View(borrowers);
        }
    }
}
