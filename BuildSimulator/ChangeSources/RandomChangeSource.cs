using System;

namespace PeteRichardson.BuildSimulator
{
	public class RandomChangeSource : ChangeSource
	{
		private int currentChangeNumber;
		private int maxChanges;
		private DateTime nextChangeDate;
		private TimeSpan meanTimeBetweenCheckins;
		private int percentClean;
		private Random rand;

		public RandomChangeSource(Simulator sim) : this(sim, 200)
		{
 		}

		public RandomChangeSource(Simulator sim, int changeCount) : base(sim)
		{
			rand = new Random();
			maxChanges = changeCount;
			this.currentChangeNumber = 0;
			this.nextChangeDate = DateTime.Parse("5/12/2010 12:00 AM");
			this.meanTimeBetweenCheckins = new TimeSpan(0,2,00,0,0);  // 1 hour
			this.percentClean = 100;
		}


		public override void Init()
		{
			Change change = GetNextChange();
			simulator.AddEvent( new ChangeEvent(change.change_date.Ticks, new SimAction(ChangeHandler), change));
		}

		public override Change GetNextChange()
		{
			currentChangeNumber++;
			if (currentChangeNumber > maxChanges)
				return null;

			// get the next change
			Change change = new Change();
			change.change_id = currentChangeNumber;
			change.change_date = nextChangeDate;
			nextChangeDate = RandomDateTimeAround(nextChangeDate.AddTicks(meanTimeBetweenCheckins.Ticks), 300);
			change.clean = rand.Next(100) < percentClean;

            // Each change builds 3 targets
			change.targets = new Target[3];
            for (int i = 0; i < change.targets.Length; i++)
			{
				Target t = new Target();
				t.buildNumber = change.change_id;
				t.clean = change.clean;
				//if (t.clean)
				//	t.build_time = RandomTimeSpanAround(new TimeSpan(0,2,0,0,0), 900);
				//else
				//	t.build_time = RandomTimeSpanAround(new TimeSpan(0,0,15,0,0), 300);
				change.targets[i] = t;
			}
            change.targets[0].name = "Thor_hammer_milestone";
            change.targets[0].build_time = RandomTimeSpanAround(new TimeSpan(0,2,0,0,0), 900);
            change.targets[1].name = "Parallel_Thor_hammer";
            change.targets[1].build_time = RandomTimeSpanAround(new TimeSpan(0,1,0,0,0), 900);
            change.targets[2].name = "Loki";
            change.targets[2].build_time = RandomTimeSpanAround(new TimeSpan(0,1,30,0,0), 900);


			return change;
		}

		private DateTime RandomDateTimeAround(DateTime middle, int seconds)
		{
			long delta = rand.Next(2*seconds) - seconds/2;
			return middle.AddTicks(delta * 10000000L);
		}
		private TimeSpan RandomTimeSpanAround(TimeSpan middle, int seconds)
		{
			long delta = rand.Next(2*seconds) - seconds/2;
			return middle.Add(new TimeSpan(delta * 10000000L));
		}
	}
}
