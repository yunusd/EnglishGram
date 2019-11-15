using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishGram.Models
{
    public class UserDetail : Base
    {
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}