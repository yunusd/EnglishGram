using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishGram.Models
{
    public class Photo : Base
    {
        public string PhotoUrl { get; set; }

        public bool IsPassed { get; set; }

        public string Description { get; set; }
    
        public bool IsLearned { get; set; }

        public int SeenCount { get; set; }
    }
}