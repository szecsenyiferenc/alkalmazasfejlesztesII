using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Output
{
    public class CustomerOutputDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public IEnumerable<PictureOutputDto> Pictures { get; set; }
    }
}
