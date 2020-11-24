using Core.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureViewer.ViewModels
{
    public class RegisterViewModel
    {
        public CustomerInputDto Customer { get; set; }
        public bool Failed { get; set; }
    }
}
