using Microsoft.AspNetCore.Mvc;
using PierresTreats.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PierresTreats.Controllers
{
    public class FlavorsController : Controller
    {
        private readonly PierresTreatsContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public FlavorsController(UserManager<ApplicationUser> userManager, PierresTreatsContext db)
        {
                        _userManager = userManager;
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            List<Flavor> model = _db.Flavors.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Flavor flavor)
        {
            _db.Flavors.Add(flavor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Flavor thisFlavor = _db.Flavors
                .Include(flavor => flavor.Treats)
                    .ThenInclude(join => join.Treat)
                .FirstOrDefault(flavor => flavor.FlavorId == id);
            return View(thisFlavor);
        }

        public ActionResult Edit(int id)
        {
            Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
            return View(thisFlavor);
        }

        [HttpPost]
        public ActionResult Edit(Flavor flavor)
        {
            _db.Entry(flavor).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
            return View(thisFlavor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
            _db.Flavors.Remove(thisFlavor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteFlavor(int joinId)
        {
            FlavorTreat joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == joinId);
            _db.FlavorTreat.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}