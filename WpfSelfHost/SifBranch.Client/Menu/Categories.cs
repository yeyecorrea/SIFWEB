using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SifBranch.Client.Menu
{
	public class Categories
	{
		public Int32 ProductCategory { get; set; }

		public String Description { get; set; }

		public Collection<Transaction> TransactionsList
		{
			get
			{
				return this.fTransactionsList;
			}
		}

		private Collection<Transaction> fTransactionsList = new Collection<Transaction>(new List<Transaction>());
	}
}
