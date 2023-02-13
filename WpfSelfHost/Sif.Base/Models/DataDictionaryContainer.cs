using Newtonsoft.Json;

namespace Sif.Base.Models
{
	public class DataDictionaryContainer
	{
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore,
			PropertyName = "params")]
		public SifRestParameters ParametersList { get; set; }
	}
}
