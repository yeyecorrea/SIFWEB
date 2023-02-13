using Newtonsoft.Json;

namespace Sif.Base.Models
{
	public class SifRestParameters
	{
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
			PropertyName = "dictionary")]
		public DataDict Dictionary { get; set; }
	}
}
