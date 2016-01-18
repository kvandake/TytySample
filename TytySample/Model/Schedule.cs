using System.Collections.Generic;
using Newtonsoft.Json;

namespace TytySample
{
	public class Schedule 
	{
		[JsonProperty("citiesFrom")]
		public List<SectionStation> CitiesFrom {get;set;}

		[JsonProperty("citiesTo")]
		public List<SectionStation> CitiesTo { get; set;}
	}
}

