using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureViewer.ViewModels
{
    public class SinglePictureViewModel
    {
        public Picture Picture { get; set; }
        public Customer Customer { get; set; }
    }
}
