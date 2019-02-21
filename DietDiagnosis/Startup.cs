using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DietDiagnosis.Startup))]
namespace DietDiagnosis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
