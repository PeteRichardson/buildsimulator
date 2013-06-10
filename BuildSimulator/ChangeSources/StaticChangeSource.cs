using System;

namespace PeteRichardson.BuildSimulator
{

	public class StaticChangeSource : ChangeSource
	{
		private int currentChangeIndex = 0;
		Change[] changes = new Change[4];

		public StaticChangeSource(Simulator sim) : base(sim) {}

		public override void Init()
		{
			changes[0] = new Change(12345, DateTime.Parse("5/8/2003 17:30"), true);
			changes[1] = new Change(12346, DateTime.Parse("5/8/2003 17:35"), false);
			changes[2] = new Change(12349, DateTime.Parse("5/8/2003 17:36"), true);
			changes[3] = new Change(12353, DateTime.Parse("5/8/2003 17:50"), false);
		}

		public override Change GetNextChange()
		{
			if (currentChangeIndex <= changes.Length-1)
				return changes[currentChangeIndex++];
			else
				return null;
		}

	}
}
