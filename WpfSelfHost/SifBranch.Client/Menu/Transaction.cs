using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SifBranch.Client.Menu
{
	public class Transaction
	{
		public Int32 TransactionId { get; set; }
		public String TransactionCode { get; set; }

		public Int32 ProductCategory { get; set; }

		public String TransactionDescription {
			get => fTransactionDescription;
			set
			{
				if (String.IsNullOrEmpty(value))
				{
					value = String.Empty;
				}
				this.fTransactionDescription = value.Replace(".html", "");
			}
		}
		private String fTransactionDescription;
	}
}
