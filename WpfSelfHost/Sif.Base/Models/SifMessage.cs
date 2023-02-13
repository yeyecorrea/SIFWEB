using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sif.Base.Models
{
	public class SifMessage
	{
		public Int16 MessageCategory { get; set; }

		public String MessageCode { get; set; }

		public String Text { get; set; }

		public Boolean IsException { get; set; }
	}
}
