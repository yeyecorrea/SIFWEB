using Newtonsoft.Json;

namespace Sif.Base.Models
{
	public class DataDict
	{
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
			NullValueHandling = NullValueHandling.Ignore,
			PropertyName = "security")]
		public DataDictSecurity Security { get; set; }
	}
}
