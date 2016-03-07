using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookList.Startup))]
namespace BookList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
