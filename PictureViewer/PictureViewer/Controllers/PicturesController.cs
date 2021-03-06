﻿using System;
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
    public class PicturesController : Controller
    {
        private readonly DatabaseService _databaseService;
        private readonly IMapper _autoMapper;

        public PicturesController(DatabaseService databaseService, IMapper autoMapper)
        {
            _databaseService = databaseService;
            _autoMapper = autoMapper;
        }

        // GET: PicturesController
        public ActionResult Index()
        {
            if (!HttpContext.IsSignedIn())
            {
                return RedirectToAction("Index", "Login");
            }

            var customer = HttpContext.GetCurrentUser();

            if(customer == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var vm = new PicturesViewModel()
            {
                Customer = customer,
                Pictures = _databaseService.GetPictures().ToList()
            };

            return View(vm);
        }

        // GET: PicturesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PicturesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PicturesController/Create
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

        // GET: PicturesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PicturesController/Edit/5
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

        // GET: PicturesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PicturesController/Delete/5
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
