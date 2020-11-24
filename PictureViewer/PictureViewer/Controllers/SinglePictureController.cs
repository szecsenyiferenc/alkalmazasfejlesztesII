using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PictureViewer.ExtensionMethods;
using PictureViewer.Services;
using PictureViewer.ViewModels;

namespace PictureViewer.Controllers
{
    public class SinglePictureController : Controller
    {
        private readonly DatabaseService _databaseService;
        private readonly IMapper _autoMapper;

        public SinglePictureController(DatabaseService databaseService, IMapper autoMapper)
        {
            _databaseService = databaseService;
            _autoMapper = autoMapper;
        }

        // GET: SinglePictureController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SinglePictureController/Details/5
        public ActionResult Details(int id)
        {
            var picture = _databaseService.GetPicture(id);

            var customer = HttpContext.GetCurrentUser();

            if (customer == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var vm = new SinglePictureViewModel()
            {
                Customer = customer,
                Picture = picture
            };

            return View(vm);
        }

        // GET: SinglePictureController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SinglePictureController/Create
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

        // GET: SinglePictureController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SinglePictureController/Edit/5
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

        // GET: SinglePictureController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var customer = HttpContext.GetCurrentUser();

            if (customer == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var picture = _databaseService.GetPicture(id);

            if(picture.UploaderId == customer.Id)
            {
                await _databaseService.DeletePicture(picture.Id);
            }

            return RedirectToAction("Index", "Pictures");
        }

        // POST: SinglePictureController/Delete/5
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
