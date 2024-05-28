using Bricks_auction_application.Models.System.Repository.IRepository;
using Bricks_auction_application.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

public class CategoriesController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CategoriesController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        var categories = _unitOfWork.Category.GetAllCategories();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View(new CategoryVM());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryVM categoryVM, IFormFile ImageFile)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            // Upload Image
            if (ImageFile != null && ImageFile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                string folderPath = Path.Combine(wwwRootPath, "images", "category");
                string filePath = Path.Combine(folderPath, fileName);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                categoryVM.category.ImagePath = Path.Combine("images", "category", fileName).Replace("\\", "/");
            }

            _unitOfWork.Category.Add(categoryVM.category);
            _unitOfWork.Save();
            TempData["success"] = "Category created successfully";
            return RedirectToAction(nameof(Index));
        }
        return View(categoryVM);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        return View(new CategoryVM { category = category });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CategoryVM categoryVM, IFormFile ImageFile)
    {
        if (id != categoryVM.category.Id)
        {
            return NotFound();
        }

        var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }

        string wwwRootPath = _webHostEnvironment.WebRootPath;

        // Upload Image
        if (ImageFile != null && ImageFile.Length > 0)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            string folderPath = Path.Combine(wwwRootPath, "images", "category");
            string filePath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }

            categoryFromDb.ImagePath = Path.Combine("images", "category", fileName).Replace("\\", "/");
        }

        categoryFromDb.CategoryName = categoryVM.category.CategoryName;
        _unitOfWork.Category.Update(categoryFromDb);
        _unitOfWork.Save();
        TempData["success"] = "Category updated successfully";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        var categoryVM = new CategoryVM { category = category };
        return View(categoryVM);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }

        var categoryVM = new CategoryVM { category = category };
        return View(categoryVM);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
        if (category != null)
        {
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
        }
        return RedirectToAction(nameof(Index));
    }

}
