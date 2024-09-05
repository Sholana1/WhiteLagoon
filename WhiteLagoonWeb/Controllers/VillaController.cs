using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoonWeb.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VillaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var villa = _context.Villas.ToList();
            return View(villa);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa villa)
        {
            if (ModelState.IsValid) {
                _context.Villas.Add(villa);
                _context.SaveChanges();
                TempData["success"] = "Villa has been created successfully.";
                return RedirectToAction("Index");
            }
            
            return View();
        }

        public IActionResult Update(int villaId)
        {
            Villa? villa = _context.Villas.FirstOrDefault(x => x.Id == villaId);
            if (villa == null) { 
                return RedirectToAction("Error", "Home");
            }

            return View(villa);
        }

        [HttpPut]
        public IActionResult Update(Villa villa)
        {
            if (!ModelState.IsValid && villa.Id >0)
            {
                _context.Villas.Update(villa);
                _context.SaveChanges();
                TempData["success"] = "Villa has been update successfully.";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Delete(int villaId) { 
            Villa? villa = _context.Villas.FirstOrDefault(u => u.Id == villaId);
            if (villa == null) {
                return RedirectToAction("Error", "Home");
            }
            return View(villa);

        }

        [HttpPost]
        public IActionResult Delete(Villa villa)
        {
            Villa? objFromDb = _context.Villas.FirstOrDefault(u => u.Id == villa.Id);
            if (objFromDb is not null) { 
                _context.Villas.Remove(objFromDb);
                _context.SaveChanges();
                TempData["success"] = "Villa has been deleted successfully.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Villa could not be deleted.";
            return View();
        }
    }
}
