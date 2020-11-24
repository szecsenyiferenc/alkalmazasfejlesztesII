using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Models.Input;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PictureViewer.ExtensionMethods;
using PictureViewer.Services;
using PictureViewer.ViewModels;

namespace PictureViewer.Controllers
{
    public class UploadController : Controller
    {

        private readonly DatabaseService _databaseService;
        private readonly IMapper _autoMapper;

        public UploadController(DatabaseService databaseService, IMapper autoMapper)
        {
            _databaseService = databaseService;
            _autoMapper = autoMapper;
        }

        // GET: UploadController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UploadController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UploadController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UploadController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UploadViewModel uploadViewModel)
        {
            try
            {
                byte[] array;

                if(uploadViewModel.Picture == null)
                {
                    throw new Exception();
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    await uploadViewModel.Picture.CopyToAsync(ms);
                    array = ms.ToArray();
                }

                var customerId = HttpContext.GetCurrentUserId();

                if(customerId == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                var pictureDto = new PictureInputDto(uploadViewModel.Title, uploadViewModel.Description, array);

                var picture = _autoMapper.Map<Picture>(pictureDto);

                await _databaseService.AddPicture(customerId.Value, picture);

                return RedirectToAction("Index", "Pictures");
            }
            catch(Exception e)
            {
                uploadViewModel.Failed = true;
                return View("Index", uploadViewModel);
            }
        }

        // GET: UploadController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UploadController/Edit/5
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

        // GET: UploadController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UploadController/Delete/5
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
