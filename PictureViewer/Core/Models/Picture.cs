using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public byte[] Image { get; set; }
        public int UploaderId { get; set; }
        [ForeignKey("UploaderId")]
        public Customer Uploader { get; set; }
    }
}
