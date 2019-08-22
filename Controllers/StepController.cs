using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Formary.Models;
using Formary.Services;
using Microsoft.AspNetCore.Mvc;

namespace Formary.Controllers
{
    public class StepController : Controller
    {
        private readonly IStepService _stepService;
        public StepController(IStepService stepService)
        {
            _stepService = stepService;
        }

        [ActionName("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _stepService.QueryStepsAsync("SELECT * FROM c"));
        }

        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,Location,Date")] Step item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid().ToString();
                await _stepService.AddStepAsync(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Step item = await _stepService.GetStepAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("Id,Location,Date")] Step item)
        {
            if (ModelState.IsValid)
            {
                await _stepService.UpdateStepAsync(item);
                return RedirectToAction("Index");
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

            Step item = await _stepService.GetStepAsync(id);
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
            await _stepService.DeleteStepAsync(id);
            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            return View(await _stepService.GetStepAsync(id));
        }
    }
}