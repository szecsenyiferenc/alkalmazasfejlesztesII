using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureViewer.ViewModels
{
    public class PicturesViewModel
    {
        public Customer Customer { get; set; }
        public List<Picture> Pictures { get; set; }

        public PicturesViewModel(Customer customer, List<Picture> pictures)
        {
            Customer = customer;
            Pictures = new List<Picture>();
        }

        public PicturesViewModel()
        {
        }
    }
}
