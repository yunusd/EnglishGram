using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EnglishGram.Models
{
    public class UserLibrary : Base
    {
        [Required]
        public bool IsLearned { get; set; }

        [Required]
        public bool IsPassed { get; set; }

        [ForeignKey("Photo")]
        public int PhotoId { get; set; }

        public virtual Photo Photo { get; set; }
    }
}