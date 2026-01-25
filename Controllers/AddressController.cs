using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechSolutions.Web.Data;
using TechSolutions.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace TechSolutions.Web.Controllers
{
    [Authorize]
    public class AddressesController : Controller
    {
        private readonly AppDbContext _context;

        public AddressesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ByCustomer(int customerId)
        {
            var addresses = await _context.Addresses
                .Where(a => a.CustomerID == customerId)
                .ToListAsync();

            ViewBag.CustomerID = customerId;
            return View(addresses);
        }


        public IActionResult Create(int customerId)
        {
            ViewBag.CustomerID = customerId;
            return View(new Address { CustomerID = customerId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Address address)
        {
            if (!ModelState.IsValid)
                return View(address);

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ByCustomer), new { customerId = address.CustomerID });
        }


      [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Delete(int id)
{
    var address = await _context.Addresses.FindAsync(id);
    if (address == null)
        return NotFound();

    _context.Addresses.Remove(address);
    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(ByCustomer),
        new { customerId = address.CustomerID });
}

    }
}
