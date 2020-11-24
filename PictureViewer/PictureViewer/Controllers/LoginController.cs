using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Models.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using PictureViewer.ExtensionMethods;
using PictureViewer.Services;
using PictureViewer.ViewModels;

namespace PictureViewer.Controllers
{
    public class LoginController : Controller
    {
        private readonly DatabaseService _databaseService;
        private readonly IMapper _autoMapper;

        public LoginController(DatabaseService databaseService, IMapper autoMapper)
        {
            _databaseService = databaseService;
            _autoMapper = autoMapper;
        }

        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoginViewModel loginViewModel)
        {
            try
            {
                var customer = _databaseService.CheckLogin(new LoginData(loginViewModel.Customer.Username, loginViewModel.Customer.Password));
                if (customer == null)
                {
                    var vm = loginViewModel;
                    vm.Failed = true;
                    return View("Index", vm);
                }

                HttpContext.SignIn(customer);

                return RedirectToAction("Index", "Pictures");
            }
            catch
            {
                return View("Index");
            }
        }

        public ActionResult SignOut()
        {
            HttpContext.SignOut();
            return RedirectToAction("Index", "Login");
        }

        public async Task<ActionResult> Delete()
        {
            try
            {
                var currentUser = HttpContext.GetCurrentUser();

                await _databaseService.DeleteCustomer(currentUser.Id);

                return RedirectToAction("Index", "Login");
            }
            catch
            {
                return RedirectToAction("Index", "Pictures");
            }
        }
    }
}
