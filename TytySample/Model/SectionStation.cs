using System.Collections.Generic;
using Newtonsoft.Json;

namespace TytySample
{
	public class SectionStation : StationBase
	{

		[JsonProperty("stations")]
		public List<Station> Stations { get; set;}

		public override string ToString ()
		{
			return string.Format ("{0}, {1}", CityTitle, CountryTitle);
		}

	}
}

