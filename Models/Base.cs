using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishGram.Models
{
    public class Base
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}