using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EnglishGram.Startup))]
namespace EnglishGram
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
