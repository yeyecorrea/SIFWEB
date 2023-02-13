using Microsoft.Owin.Hosting;
using SifBranch.Client;
using System;
using System.Windows;

namespace WpfSelfHost
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			//String baseAdress = "http://localhost:9000";
			//String baseAdress2 = "http://198.19.237:9000";
			String baseAdress = "http://+:12345";

			//StartOptions options = new StartOptions();
			//options.Urls.Add(baseAdress);
			//options.Urls.Add(baseAdress2);

			WebApp.Start<Startup>(baseAdress);

			MessagesManager.UserMessagesManager = SifBranch.Client.SifBranch.ResourceManager;

			MainWindow main = new MainWindow();
			//main.majoWebBrowser.Navigate($"http://{Environment.MachineName}:12345/AppWeb/login.html");
			//WindowManager.CurrentWindow = main;

			String xmlTransactions = "<ROOT><CATEGORIES><CATEGORY><PRODCAT_ID>1</PRODCAT_ID><DESCRIPTION>Seguridad</DESCRIPTION></CATEGORY><CATEGORY><PRODCAT_ID>2</PRODCAT_ID><DESCRIPTION>Efectivo</DESCRIPTION></CATEGORY></CATEGORIES><TRANSACTIONS><TRANSACTION><PRODCAT_ID>1</PRODCAT_ID><IN_MENU>1</IN_MENU><TRANS_ID>1</TRANS_ID><TRAN_CODE>0232</TRAN_CODE><SERVICE_NAME>Cambio de Oficina</SERVICE_NAME></TRANSACTION><TRANSACTION><PRODCAT_ID>2</PRODCAT_ID><IN_MENU>1</IN_MENU><TRANS_ID>2</TRANS_ID><TRAN_CODE>2022</TRAN_CODE><SERVICE_NAME>Cambio de efectivo</SERVICE_NAME></TRANSACTION></TRANSACTIONS></ROOT>";
			main.SetTransactionsList(xmlTransactions);
			main.Show();
		}
	}
}
