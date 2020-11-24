using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PictureViewer.Services;
using PictureViewer.ViewModels;

namespace PictureViewer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly DatabaseService _databaseService;
        private readonly IMapper _autoMapper;

        public RegisterController(DatabaseService databaseService, IMapper autoMapper)
        {
            _databaseService = databaseService;
            _autoMapper = autoMapper;
        }

        // GET: RegisterController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RegisterController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegisterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegisterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel registerViewModel)
        {
            try
            {
                var customerObject = _autoMapper.Map<Customer>(registerViewModel.Customer);
                var customer = await _databaseService.AddCustomer(customerObject);

                return RedirectToAction("Index", "Login");
            }
            catch
            {
                var vm = registerViewModel;
                vm.Failed = true;
                return View("Index", vm);
            }
        }

        // GET: RegisterController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RegisterController/Edit/5
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

        // GET: RegisterController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RegisterController/Delete/5
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
