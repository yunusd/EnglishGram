using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnglishGram.Models
{
    public class Photo : Base
    {
        [Required]
        [Url]
        public string PhotoUrl { get; set; }

        [StringLength(200)]
        public string Description { get; set; }   

        [Required]
        public int SeenCount { get; set; }

    }
}