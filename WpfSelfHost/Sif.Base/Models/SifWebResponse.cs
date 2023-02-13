using Sif.Base.Enums;
using System;

namespace Sif.Base.Models
{
	public class SifWebResponse
	{
		public ServiceState State { get; set; }
		public SifMessage Message { get; set; }
		public String ResponseData { get; set; }
		public object JsonResponseObject { get; set; }
		public object DataDictionary { get; set; }
	}
}
