using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AcademicStaff.Startup))]
namespace AcademicStaff
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
