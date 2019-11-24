using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EnglishGram.Models
{
    public class Photo : Base
    {
        public Photo()
        {
            this.ApplicationUsers = new HashSet<ApplicationUser>();
        }

        [Required]
        [Url]
        public string PhotoUrl { get; set; }

        [Required]
        [Url]
        public string SubPhotoUrl { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(200)]
        public string SubDescription { get; set; }

        [Required]
        public int SeenCount { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}