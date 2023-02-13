using Sif.Base.Models;
using System;

namespace SifBranch.Client
{
	public class CloseTransactionBusiness
	{
		public SifWebResponse BeginProcess()
		{
			if (WindowManager.CurrentWindow != null)
			{
				try
				{
					_ = WindowManager.CurrentWindow.Dispatcher.InvokeAsync(() =>
					{

						WindowManager.CurrentWindow.Close();
						WindowManager.CurrentWindow = null;
					});
				}
				catch (Exception e)
				{

					throw;
				}
			}
			return new SifWebResponse();
		}
	}
}
