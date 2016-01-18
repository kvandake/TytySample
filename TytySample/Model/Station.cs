using Newtonsoft.Json;

namespace TytySample
{
	public class Station : StationBase
	{

		[JsonProperty("stationId")]
		public int StationId {get;set;}

		[JsonProperty("stationTitle")]
		public string StationTitle { get; set;}
	}
}

