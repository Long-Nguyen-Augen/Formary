namespace Formary.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Formary.Services;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class DeliveryController : Controller
    {
        private readonly IDeliveryService _itemService;
        public DeliveryController(IDeliveryService itemService)
        {
            _itemService = itemService;
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _itemService.QueryDeliveriesAsync("SELECT * FROM c"));
        }

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,Reference,TotalWeight,DeliveryDate")] Delivery item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid().ToString();
                await _itemService.AddDeliveryAsync(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("Id,Reference,TotalWeight,DeliveryDate")] Delivery item)
        {
            if (ModelState.IsValid)
            {
                await _itemService.UpdateDeliveryAsync(item.Id, item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Delivery item = await _itemService.GetDeliveryAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Delivery item = await _itemService.GetDeliveryAsync(id);
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
            await _itemService.DeleteDeliveryAsync(id);
            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            return View(await _itemService.GetDeliveryAsync(id));
        }
    }
}