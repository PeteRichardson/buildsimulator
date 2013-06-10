using System;
using System.Threading;

namespace PeteRichardson.BuildSimulator
{
	public class Clock : SimEntity
	{
		private long now = 0;
		private long scale = 0;

		public Clock(Simulator simulator) : base(simulator)
		{
		}

		public long Now
		{
			get { return now; }
			set { now = value; }
		}

		public long Scale
		{
			get { return scale; }
			set { scale = value; }
		}

		public override void Init()
		{
			simulator.AddEvent( new ClockEvent(simulator.clock.MinutesFromNow(1), new SimAction(ClockEventHandler), this) );
		}


		public void Set(DateTime time)
		{
			now = time.Ticks;
		}
		public void Set(long ticks)
		{
			if (ticks < now)
			{
				string error = String.Format("Error: ticks < now.  ticks={0}, now={1}", new DateTime(ticks).ToString(), new DateTime(now).ToString());
				throw new ArgumentException(error );
			}
			if (scale > 0)
			{
                TimeSpan sleeptime = new TimeSpan((ticks - now) / scale);
                //TimeSpan sleeptime = new TimeSpan(scale * 5000);
                Thread.Sleep(sleeptime);
			}
			now = ticks;
		}

		public long MinutesFromNow(int minutes)
		{
			long result = Now + minutes * 600000000L;
			return result;  // 600 million ticks/minute
		}

		public void ClockEventHandler(object sender)
		{
			simulator.AddEvent( new ClockEvent(simulator.clock.MinutesFromNow(1), new SimAction(ClockEventHandler), this) );
		}

	}

	public class ClockEvent : SimEvent
	{
		Clock clock;
		public ClockEvent(long timestamp, SimAction myAction, Clock clock):
			base(timestamp, myAction)
		{
			this.clock = clock;
		}

		public override string ToString()
		{
			return String.Format("[{0,-22}] {1,-14}",
				new DateTime(virtualTime), "Clock Event");
		}
	}

}
