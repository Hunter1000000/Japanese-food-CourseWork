using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Website.DB;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace Website
{
    public class Startup 
    {
        public IConfigurationRoot _confstring;
        public void Configure(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        public Startup(IHostingEnvironment hostEnv)
        {
            hostEnv = (IHostingEnvironment)new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }
    }
}
