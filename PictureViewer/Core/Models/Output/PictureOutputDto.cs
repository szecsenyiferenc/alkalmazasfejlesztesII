using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Output
{
    public class PictureOutputDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string Uploader { get; set; }
    }
}
