﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bricks_auction_application.Models;
using Bricks_auction_application.Models.Items;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Bricks_auction_application.Models.Items;
using Bricks_auction_application.StaticDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Globalization;

namespace Bricks_auction_application.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class SetsController : Controller
    {
        private readonly BricksAuctionDbContext _context;

        public SetsController(BricksAuctionDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Sets
        public async Task<IActionResult> Index()
        {
            var bricksAuctionDbContext = _context.Sets.Include(s => s.Category);
            return View(await bricksAuctionDbContext.ToListAsync());
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateFromCSV(IFormFile csvFile)
        //{
        //    if (csvFile == null || csvFile.Length == 0)
        //    {
        //        ModelState.AddModelError("", "File is empty or not selected.");
        //        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
        //        return View("Create");
        //    }

        //    var sets = new List<Set>();

        //    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        //    {
        //        HeaderValidated = null, // Ignoruje brakujące nagłówki
        //        MissingFieldFound = null // Ignoruje brakujące pola
        //    };

        //    using (var reader = new StreamReader(csvFile.OpenReadStream()))
        //    using (var csv = new CsvHelper.CsvReader(reader, config))
        //    {
        //        try
        //        {
        //            sets = csv.GetRecords<Set>().ToList();
        //        }
        //        catch (CsvHelperException e)
        //        {
        //            ModelState.AddModelError("", "Error reading CSV file: " + e.Message);
        //            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
        //            return View("Create");
        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        foreach (var set in sets)
        //        {
        //            // Ustawiamy CategoryId na stałą wartość (np. 1)
        //            set.CategoryId = 1;
        //            _context.Add(set);
        //        }
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
        //    return View("Create");
        //}


        // GET: Admin/Sets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @set = await _context.Sets
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@set == null)
            {
                return NotFound();
            }

            return View(@set);
        }

        // GET: Admin/Sets/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Admin/Sets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SetId,Name,EnglishName,Description,Pieces,Minifigures,ReleaseYear,CategoryId,ListPrice,ImagePath")] Set @set)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@set);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", @set.CategoryId);
            return View(@set);
        }

        // GET: Admin/Sets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @set = await _context.Sets.FindAsync(id);
            if (@set == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", @set.CategoryId);
            return View(@set);
        }

        // POST: Admin/Sets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SetId,Name,EnglishName,Description,Pieces,Minifigures,ReleaseYear,CategoryId,ListPrice,ImagePath")] Set @set)
        {
            if (id != @set.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@set);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SetExists(@set.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", @set.CategoryId);
            return View(@set);
        }

        // GET: Admin/Sets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @set = await _context.Sets
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@set == null)
            {
                return NotFound();
            }

            return View(@set);
        }

        // POST: Admin/Sets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @set = await _context.Sets.FindAsync(id);
            if (@set != null)
            {
                _context.Sets.Remove(@set);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SetExists(int id)
        {
            return _context.Sets.Any(e => e.Id == id);
        }
    }
}
