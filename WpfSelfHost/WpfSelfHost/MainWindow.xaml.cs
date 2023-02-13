using SifBranch.Client;
using SifBranch.Client.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;

namespace WpfSelfHost
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public MainWindow()
		{
			InitializeComponent();
			this.WindowState = WindowState.Maximized;
		}

		public void SetTransactionsList(String value)
		{
			List<Categories> listCategories = new List<Categories>();

			//Cargar XML
			XmlDocument xml = new XmlDocument();
			xml.LoadXml(value);
			XmlNodeList xmlCategories = xml.SelectNodes("ROOT/CATEGORIES/CATEGORY");
			foreach (XmlNode item in xmlCategories)
			{
				Categories categories = new Categories();
				Categories categoriesAux = new Categories();
				XmlNode nodeTemp = item.SelectSingleNode("PRODCAT_ID");
				if (nodeTemp != null)
				{
					Int32 prodCate = 0;
					if (Int32.TryParse(nodeTemp.InnerXml, out prodCate))
					{
						categories.ProductCategory = prodCate;
					}
				}
				nodeTemp = item.SelectSingleNode("DESCRIPTION");
				if (nodeTemp != null)
				{
					categories.Description = nodeTemp.InnerXml;
				}
				XmlNodeList xmlTransactions = xml.SelectNodes("ROOT/TRANSACTIONS/TRANSACTION[PRODCAT_ID = " + categories.ProductCategory + " and IN_MENU = 1]");
				foreach (XmlNode itemT in xmlTransactions)
				{
					Transaction transaction = new Transaction();
					nodeTemp = itemT.SelectSingleNode("TRANS_ID");
					if (nodeTemp != null)
					{
						Int32 transId = 0;
						if (Int32.TryParse(nodeTemp.InnerXml, out transId))
						{
							transaction.TransactionId = transId;
						}
					}

					nodeTemp = itemT.SelectSingleNode("TRAN_CODE");
					if (nodeTemp != null)
					{
						transaction.TransactionCode = nodeTemp.InnerXml;
					}

					nodeTemp = itemT.SelectSingleNode("SERVICE_NAME");
					if (nodeTemp != null)
					{
						transaction.TransactionDescription = nodeTemp.InnerXml;
					}

					nodeTemp = itemT.SelectSingleNode("PRODCAT_ID");
					if (nodeTemp != null)
					{
						Int32 productCategory = 0;
						if (Int32.TryParse(nodeTemp.InnerXml, out productCategory))
						{
							transaction.ProductCategory = productCategory;
						}
					}
					categoriesAux.TransactionsList.Add(transaction);
				}
				foreach (Transaction itemList in categoriesAux.TransactionsList.OrderBy(o => o.TransactionCode).ToList())
				{
					categories.TransactionsList.Add(itemList);
				}
				listCategories.Add(categories);
			}

			this.lbxCategories.ItemsSource = listCategories;
			this.lbxCategories.Focus();
			this.lbxCategories.SelectedIndex = 0;
		}

		private void lbxTransactions_MouseDoubleClick(Object sender, MouseButtonEventArgs e)
		{
			Transaction transaction = this.lbxTransactions.SelectedItem as Transaction;
			if (transaction != null)
			{
				WindowManager.OpenTransactionWindow("", transaction.TransactionCode);
			}
		}

		private void lbxTransactions_KeyDown(Object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Enter:

					Transaction transaction = this.lbxTransactions.SelectedItem as Transaction;
					if (transaction != null)
					{
						this.txtFastCode.Text = transaction.TransactionCode;
						e.Handled = true;
					}
					break;
				case Key.Back:
					if (this.txtFastCode.Text.Length > 0 )
					{
						this.txtFastCode.Text = this.txtFastCode.Text.Remove(this.txtFastCode.Text.Length - 1);
					}
					e.Handled = true;
					break;
				case Key.Delete:
					this.txtFastCode.Text = String.Empty;
					e.Handled = true;
					break;
				case Key.NumPad0:
				case Key.NumPad1:
				case Key.NumPad2:
				case Key.NumPad3:
				case Key.NumPad4:
				case Key.NumPad5:
				case Key.NumPad6:
				case Key.NumPad7:
				case Key.NumPad8:
				case Key.NumPad9:
				case Key.D0:
				case Key.D1:
				case Key.D2:
				case Key.D3:
				case Key.D4:
				case Key.D5:
				case Key.D6:
				case Key.D7:
				case Key.D8:
				case Key.D9:
					this.txtFastCode.Text += (Char)(e.Key - 26);
					e.Handled = true;
					break;
				case Key.Add:
					this.txtFastCode.Text = this.txtFastCode.Text.PadLeft(4, '0');
					e.Handled = true;
					break;
				case Key.Subtract:
					this.txtFastCode.Text = this.txtFastCode.Text.PadRight(4, '0');
					e.Handled = true;
					break;
				default:
					break;
			}

			if (this.txtFastCode.Text.Length == 4)
			{
				WindowManager.OpenTransactionWindow("", this.txtFastCode.Text);
			}
		}

		private void menuItem_Click(Object sender, RoutedEventArgs e)
		{
			MenuItem menu = (MenuItem)e.OriginalSource;
			if (menu != null)
			{
				String header = menu.Header.ToString();
				if (!String.IsNullOrEmpty(header))
				{
					String transactionCode = header.Substring(0, 4);
					WindowManager.OpenTransactionWindow("", transactionCode);
				}
			}
		}
	}
}
