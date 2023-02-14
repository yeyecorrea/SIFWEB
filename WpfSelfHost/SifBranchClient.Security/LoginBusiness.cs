using Sif.Base.Enums;
using Sif.Base.Models;
using System.Threading;

namespace SifBranchClient.Security
{
	public class LoginBusiness
	{
		public DataDict Dictionary { get; set; }
		public SifWebResponse Process()
		{
			SifWebResponse resp = new SifWebResponse();
			if (this.Dictionary.Security.UserLogOn == "Yeferson" 
				&& this.Dictionary.Security.UserPassword == "12345")
			{
				resp.State = ServiceState.Accepted;
			}
			else
			{
				resp.State = ServiceState.Rejected;
			}
			Thread.Sleep(1000);
			return resp;
		}
	}
}
