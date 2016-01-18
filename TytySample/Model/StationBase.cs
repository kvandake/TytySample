using Newtonsoft.Json;

namespace TytySample
{
	public abstract class StationBase
	{
		[JsonProperty("countryTitle")]
		public string CountryTitle {get;set;}

		[JsonProperty("point")]
		public Point Point { get; set;}

		[JsonProperty("districtTitle")]
		public string DistrictTitle { get; set;}

		[JsonProperty("cityId")]
		public int CityId { get; set;}

		[JsonProperty("cityTitle")]
		public string CityTitle { get; set;}

		[JsonProperty("regionTitle")]
		public string RegionTitle { get; set;}
	}
}

