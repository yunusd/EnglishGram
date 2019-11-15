using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EnglishGram.Models
{
    public class UserDetail : Base
    {
        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("UserLibrary")]
        public int UserLibraryId { get; set; }

        public UserLibrary UserLibrary { get; set; }
    }
}