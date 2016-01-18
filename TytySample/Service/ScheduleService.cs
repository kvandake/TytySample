using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace TytySample
{
	/// <summary>
	/// Сервис для работы с расписанием
	/// </summary>
	public class ScheduleService
	{

		Schedule schedule;

		public Schedule Schedule {
			get {
				return schedule;
			}
		}



		public ScheduleService ()
		{
			//банальный парсинг с помощью Newtonsoft
			var json = File.ReadAllText ("allStations.json");
			var h  = JsonConvert.DeserializeObject <Schedule> (json);
			schedule = new Schedule ();
			schedule.CitiesFrom = h.CitiesFrom.OrderBy (x => x.CityTitle).ToList ();
			schedule.CitiesTo = h.CitiesTo.OrderBy (x => x.CityTitle).ToList ();
		}


		public IList<SectionStation> FromStations(string id){
			return Schedule.CitiesFrom;
		}


		public IList<SectionStation> ToStations(string id){
			return Schedule.CitiesTo;
		}
	}
}

