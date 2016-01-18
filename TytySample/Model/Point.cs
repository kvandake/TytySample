using System;
using Newtonsoft.Json;

namespace TytySample
{
	public class Point
	{
		[JsonProperty("longitude")]
		public double Longitude { get; set;}

		[JsonProperty("latitude")]
		public double Latitude { get; set;}
	}
}

