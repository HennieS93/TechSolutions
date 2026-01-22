using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechSolutions.Web.Data;
using TechSolutions.Web.Models;

namespace TechSolutions.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers
    .Where(c => c.IsActive)
    .ToListAsync();
            return View(customers);
        }

        // GET: Customers Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,SurName,DateOfBirth,Email,Phone,IsActive")] Customer customer)
        {
              customer.IsActive = true;
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }


      [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, Customer model)
{
    if (id != model.ID)
    {
        return NotFound();
    }

    if (!ModelState.IsValid)
    {
        return View(model);
    }

    var customer = await _context.Customers.FindAsync(id);
    if (customer == null)
    {
        return NotFound();
    }


    customer.FirstName = model.FirstName;
    customer.SurName = model.SurName;
    customer.DateOfBirth = model.DateOfBirth;
    customer.Email = model.Email;
    customer.Phone = model.Phone;



    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.ID == id && m.IsActive);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var customer = await _context.Customers.FindAsync(id);

    if (customer == null)
    {
        return NotFound();
    }


    customer.IsActive = false;

    _context.Customers.Update(customer);
    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}


        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.ID == id);
        }
    }
}
