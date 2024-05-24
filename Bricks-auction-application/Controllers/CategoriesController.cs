using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bricks_auction_application.Models.Sets;
using Bricks_auction_application.Models.System.Repository.IRepository;
using Bricks_auction_application.Models.System.Respository;

namespace Bricks_auction_application.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public CategoriesController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        // GET: Categories
        public IActionResult Index()
        {
            var objCategoryList = _unitofwork.Category.GetAll();
            return View(objCategoryList);
        }

        // GET: Categories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _unitofwork.Category.Get(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.Category.Add(category);
                _unitofwork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _unitofwork.Category.Get(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,CategoryName")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitofwork.Category.Update(category);
                    _unitofwork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitofwork.Category.Exists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["success"] = "Category updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _unitofwork.Category.Get(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _unitofwork.Category.Get(id);
            if (category != null)
            {
                _unitofwork.Category.Delete(category);
                _unitofwork.Save();
                TempData["success"] = "Category deleted successfully";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
