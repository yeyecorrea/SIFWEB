using System;
using System.IO;
using System.Windows;

namespace SifBranch.Client
{
	public class WindowManager
	{
		public static Window CurrentWindow { set; get; }

		public static void OpenTransactionWindow(String pageName, String transactionCode)
		{
			if (!String.IsNullOrEmpty(transactionCode))
			{
				DirectoryInfo caseDirInfo = new DirectoryInfo("AppWeb/");
				FileInfo[] file = caseDirInfo.GetFiles(transactionCode + "*" + ".html");
				if (file.Length > 0)
				{
					pageName = file[0].Name;
				}
			}
			if (!String.IsNullOrEmpty(pageName))
			{
				String transactionUrl = $"http://{Environment.MachineName}:12345/AppWeb/" + pageName;

				TransactionWindow window = new TransactionWindow();
				WindowManager.CurrentWindow = window;
				window.Browser.Navigate(new Uri(transactionUrl));
				window.ShowDialog();
			}
		}
	}
}
