using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_Ressource_MVC.Controllers
{
    public class FertilizerController : Controller
    {
        // GET: FertilizerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FertilizerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FertilizerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FertilizerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FertilizerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FertilizerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FertilizerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FertilizerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
