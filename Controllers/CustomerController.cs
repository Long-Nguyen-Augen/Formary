using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Formary.Models;
using Formary.Services;
using Microsoft.AspNetCore.Mvc;

namespace Formary.Controllers
{    
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;            
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _customerService.QueryCustomersAsync("SELECT * FROM c"));
        }

        [ActionName("Create")]
        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,Name,Contact,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Id = Guid.NewGuid().ToString();
                await _customerService.AddCustomerAsync(customer);
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Customer item = await _customerService.GetCustomerAsync(id);
            if (item == null)
            {
                return NotFound();
            }            
            return View(item);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("Id,Name,Contact,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.UpdateCustomerAsync(customer);
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Customer item = await _customerService.GetCustomerAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            return View(await _customerService.GetCustomerAsync(id));
        }
    }
}