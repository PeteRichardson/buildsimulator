using System;

namespace PeteRichardson.BuildSimulator
{
	public class Change
	{
		public DateTime change_date;
		public int change_id;
		public bool clean;
		public Target[] targets;

		public Change()
		{
		}

		public Change(int change_id, DateTime change_date,  bool clean)
		{
			this.change_date = change_date;
			this.change_id = change_id;
			this.clean = clean;
		}
	}
}
