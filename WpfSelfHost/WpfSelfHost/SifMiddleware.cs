using Microsoft.Owin;
using Newtonsoft.Json;
using Sif.Base.Enums;
using Sif.Base.Models;
using SifBranch.Client;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WpfSelfHost
{
	public class SifMiddleware : OwinMiddleware
	{
		public SifMiddleware(OwinMiddleware next) : base(next)
		{

		}

		public override async Task Invoke(IOwinContext context)
		{
			if (context.Request.Method == "POST")
			{
				String path = context.Request.Path.Value;
				String serviceName = path.Replace("/", "");
				if (context.Request.Body.CanRead)
				{
					using (TextReader stream = new StreamReader(context.Request.Body))
					{
						String data = stream.ReadToEnd();
						//Deserealizar la solicitud:
						DataDict dataDictionary;
						DataDictionaryContainer container = null;
						MessageContainer messageContainer = null;

						JsonSerializerSettings settings = new JsonSerializerSettings();
						settings.NullValueHandling = NullValueHandling.Ignore;

						SifWebResponse resp = new SifWebResponse();

						if (serviceName == "Login")
						{
							container = JsonConvert.DeserializeObject<DataDictionaryContainer>(data, settings);

							if (container.ParametersList != null)
							{
								dataDictionary = container.ParametersList.Dictionary;
								SifBranchClient.Security.LoginBusiness login =
									new SifBranchClient.Security.LoginBusiness();
								login.Dictionary = dataDictionary;
								resp = login.Process();
							}
						}
						else if (serviceName == "messageGet")
						{
							messageContainer = JsonConvert.DeserializeObject<MessageContainer>(data, settings);
							if (!String.IsNullOrEmpty(messageContainer?.MessageCode))
							{
								resp.Message = new SifMessage();
								resp.Message.Text = MessagesManager.UserMessagesManager.GetString(messageContainer?.MessageCode);
								resp.State = ServiceState.Accepted;
							}
						}
						else if (serviceName == "closeTransaction")
						{
							CloseTransactionBusiness close = new CloseTransactionBusiness();
							close.BeginProcess();
						}
						SifMiddleware.WriteResponse(context, "application/json", JsonConvert.SerializeObject(resp));
					}
				}
			}
			await this.Next.Invoke(context);
		}

		internal static void WriteResponse(IOwinContext context, String contentType, String body)
		{
			Byte[] bytes;
			if (context != null)
			{
				String[] fContentType = new String[1];
				fContentType[0] = contentType;
				context.Response.Headers.Add("Content-Type", fContentType);
				bytes = Encoding.UTF8.GetBytes(body);
				Stream bodyStream = context.Response.Body;
				try
				{
					bodyStream.Write(bytes, 0, bytes.Length);
				}
				catch (Exception e)
				{

					Console.WriteLine($"Excepción lanzada: {e}");
				}
				bodyStream.Flush();
			}

		}
	}
}
