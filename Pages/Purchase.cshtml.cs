using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreM8.Infrastructure;
using BookStoreM8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStoreM8.Pages
{
    public class PurchaseModel : PageModel
    {
        private IBookStoreRepository repo { get; set; }
        public PurchaseModel (IBookStoreRepository temp)
        {
            repo = temp;
        }

        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        // Isbn is a string, not an integer
        public IActionResult OnPost(string Isbn, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.Isbn == Isbn);

            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            cart.AddItem(b, 1);

            HttpContext.Session.SetJson("cart", cart);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
