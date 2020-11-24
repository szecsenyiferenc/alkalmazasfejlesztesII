using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureViewer.ViewModels
{
    public class UploadViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public bool Failed { get; set; }
        public byte[] Image { get; set; }
        public IFormFile Picture { get; set; }

    }
}
