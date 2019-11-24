using EnglishGram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnglishGram.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ApplicationDbContext ctx = new ApplicationDbContext();
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                ctx.Dispose();
            }
        }
    }
}