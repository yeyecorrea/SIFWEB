using Newtonsoft.Json;
using System;

namespace Sif.Base.Models
{
	public class DataDictSecurity
	{
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
			NullValueHandling = NullValueHandling.Ignore, PropertyName = "userLogon")]
		public String UserLogOn { get; set; }

		[JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
			NullValueHandling = NullValueHandling.Ignore, PropertyName = "userPassword")]
		public String UserPassword { get; set; }
	}
}
