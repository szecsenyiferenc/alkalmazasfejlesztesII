using Core.Models.Output;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.Input
{
    public class PictureInputDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public byte[] Image { get; set; }

        public PictureInputDto()
        {

        }

        public PictureInputDto(string title, string description, byte[] image)
        {
            Title = title;
            Description = description;
            Image = image;
        }
    }
}
