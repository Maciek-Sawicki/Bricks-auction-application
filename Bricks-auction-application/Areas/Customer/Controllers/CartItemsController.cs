using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bricks_auction_application.Models;
using Bricks_auction_application.Models.Users;
using Bricks_auction_application.Models.System.Repository.IRepository;
using System.Security.Claims;

namespace Bricks_auction_application.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartItemsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            // Get the logged-in user's ID
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            // Retrieve cart items for the current user
            var cartItems = await _unitOfWork.CartItem.GetAllAsync(
                filter: ci => ci.Cart.UserId == userId,
                includeProperties: "Cart,Offer,Offer.LEGOSet,Offer.User"
            );

            return View(cartItems);
        }

        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _unitOfWork.CartItem.GetAsync(id.Value);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: CartItems/Create
        public IActionResult Create()
        {
            ViewData["CartId"] = new SelectList(_unitOfWork.Cart.GetAll(), "CartId", "UserId");
            ViewData["OfferId"] = new SelectList(_unitOfWork.Offer.GetAll(), "OfferId", "OfferId");
            return View();
        }

        // POST: CartItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartItemId,CartId,OfferId")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CartItem.Add(cartItem);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_unitOfWork.Cart.GetAll(), "CartId", "UserId", cartItem.CartId);
            ViewData["OfferId"] = new SelectList(_unitOfWork.Offer.GetAll(), "OfferId", "OfferId", cartItem.OfferId);
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _unitOfWork.CartItem.GetAsync(id.Value);
            if (cartItem == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_unitOfWork.Cart.GetAll(), "CartId", "UserId", cartItem.CartId);
            ViewData["OfferId"] = new SelectList(_unitOfWork.Offer.GetAll(), "OfferId", "OfferId", cartItem.OfferId);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartItemId,CartId,OfferId")] CartItem cartItem)
        {
            if (id != cartItem.CartItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                /*try
                {
                    _unitOfWork.CartItem.Update(cartItem);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.CartItem.Exists(cartItem.CartItemId))
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
            ViewData["CartId"] = new SelectList(_unitOfWork.Cart.GetAll(), "CartId", "UserId", cartItem.CartId);
            ViewData["OfferId"] = new SelectList(_unitOfWork.Offer.GetAll(), "OfferId", "OfferId", cartItem.OfferId);
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _unitOfWork.CartItem.GetFirstOrDefaultAsync(
                ci => ci.CartItemId == id && ci.Cart.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)
            );
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartItem = await _unitOfWork.CartItem.GetFirstOrDefaultAsync(
                ci => ci.CartItemId == id && ci.Cart.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)
            );
            if (cartItem != null)
            {
                _unitOfWork.CartItem.Remove(cartItem);
                await _unitOfWork.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}