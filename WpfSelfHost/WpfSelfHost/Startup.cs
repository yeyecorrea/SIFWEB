using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(WpfSelfHost.Startup))]

namespace WpfSelfHost
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			//app.Run((owinContext) =>
			//{
			//	owinContext.Response.ContentType = "text/plain";
			//	return owinContext.Response.WriteAsync("Hello World");
			//});

			app.UseCors(CorsOptions.AllowAll);
			app.Use<SifMiddleware>();
			app.Use<CacheMiddleware>();
			app.UseStaticFiles(@"/AppWeb");
		}
	}
}
