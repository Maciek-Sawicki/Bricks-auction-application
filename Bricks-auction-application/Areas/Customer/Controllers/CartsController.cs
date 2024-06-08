using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bricks_auction_application.Models;
using Bricks_auction_application.Models.Users;
using Bricks_auction_application.Models.System.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bricks_auction_application.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId(); // metoda pomocnicza do uzyskania ID bieżącego użytkownika
            var cart = await _unitOfWork.Cart.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                // Jeśli koszyk nie istnieje, tworzymy nowy dla użytkownika
                cart = new Cart { UserId = userId };
                _unitOfWork.Cart.Add(cart);
                await _unitOfWork.SaveAsync();
            }
            return View(cart.Items); // Możemy przekazać Items lub cały koszyk w zależności od potrzeb widoku
        }

        // Metoda pomocnicza do uzyskania ID zalogowanego użytkownika
        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier); // Uzyskanie ID bieżącego użytkownika
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _unitOfWork.Cart.GetAsync(id.Value);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_unitOfWork.User.GetAll(), "UserId", "Email");
            return View();
        }

        // POST: Carts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Cart.Add(cart);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_unitOfWork.User.GetAll(), "UserId", "Email", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _unitOfWork.Cart.GetAsync(id.Value);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_unitOfWork.User.GetAll(), "UserId", "Email", cart.UserId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,UserId")] Cart cart)
        {
            if (id != cart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                /*try
                {
                    _unitOfWork.Cart.Update(cart);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.Cart.Exists(cart.CartId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }*/
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_unitOfWork.User.GetAll(), "UserId", "Email", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _unitOfWork.Cart.GetAsync(id.Value);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _unitOfWork.Cart.GetAsync(id);
            if (cart != null)
            {
                _unitOfWork.Cart.Remove(cart);
                await _unitOfWork.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
