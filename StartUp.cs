using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BulkEmailMarketing.App_Start.StartUp))]
namespace BulkEmailMarketing.App_Start
{
    public partial class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}