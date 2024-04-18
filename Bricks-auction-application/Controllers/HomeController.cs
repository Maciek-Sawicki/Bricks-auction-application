using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bricks_auction_application.Models;
using Bricks_auction_application.Models.Offers;

namespace Bricks_auction_application.Controllers
{
    public class HomeController : Controller
    {
        private readonly BricksAuctionDbContext _context;

        public HomeController(BricksAuctionDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            // Pobranie ofert wraz z powi¹zanymi zestawami LEGO i u¿ytkownikami
            var offers = from o in _context.Offers.Include(o => o.LEGOSet).Include(o => o.User)
                         select o;

            // Jeœli jest zdefiniowany ci¹g wyszukiwania, filtrujemy oferty
            if (!string.IsNullOrEmpty(searchString))
            {
                offers = offers.Where(o => o.LEGOSet.Name.Contains(searchString) || o.LEGOSet.Id.ToString().Contains(searchString));
            }

            return View(await offers.ToListAsync());
        }

        [HttpPost]
        public IActionResult IndexPost(string searchFilter)
        {
            return RedirectToAction("Index", new { searchString = searchFilter });
        }

        /*public async Task<IActionResult> Index()
        {
            var offers = await _context.Offers.Include(o => o.LEGOSet).Include(o => o.User).ToListAsync();
            return View(offers);
        }*/


        /* public IActionResult About()
         {
             ViewData["Message"] = "Your application description page.";

             return View();
         }

         public IActionResult Contact()
         {
             ViewData["Message"] = "Your contact page.";

             return View();
         }

         public IActionResult Privacy()
         {
             return View();
         }
        */

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
