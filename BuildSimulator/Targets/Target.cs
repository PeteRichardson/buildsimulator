using System;

namespace PeteRichardson.BuildSimulator
{
	public class Target
	{
		public int buildNumber;

		public string name;
		public DateTime time_requested;
		public DateTime time_started;
		public DateTime time_finished;
		public TimeSpan build_time;
		public int successful;
		public bool clean;

		public Target()
		{
		}

		public Target (int buildNumber, string name, bool clean)
		{
			this.buildNumber = buildNumber;

			this.name=name;
			this.clean = clean;
		}

		public Target (int buildNumber, string name, DateTime time_requested, DateTime time_started,
			DateTime time_finished, TimeSpan build_time, int successful, bool clean)
		{
			this.buildNumber = buildNumber;

			this.name=name;
			this.time_requested = time_requested;
			this.time_started = time_started;
			this.time_finished = time_finished;
			this.build_time = build_time;
			this.successful = successful;
			this.clean = clean;
		}
	}
}
