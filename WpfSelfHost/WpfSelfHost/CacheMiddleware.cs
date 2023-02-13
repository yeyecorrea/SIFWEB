using Microsoft.Owin;
using System.Threading.Tasks;

namespace WpfSelfHost
{
	public class CacheMiddleware : OwinMiddleware
	{
		public CacheMiddleware(OwinMiddleware next) : base(next)
		{

		}
		public override async Task Invoke(IOwinContext context)
		{
			await Next.Invoke(context);
			context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
			context.Response.Headers["Pragma"] = "no-cache";
			context.Response.Headers["Expires"] = "0";
		}
	}
}
