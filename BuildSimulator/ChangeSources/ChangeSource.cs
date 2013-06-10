using System;

namespace PeteRichardson.BuildSimulator
{

	public class ChangeSource : SimEntity
	{
		public ChangeSource(Simulator sim) : base(sim) {}

		public virtual void ChangeHandler(object sender)
		{
			ChangeEvent ce = (ChangeEvent) sender;

			// Queue the builds for this change
			// Console.WriteLine("\tQueueing builds for change #{0}", ce.changeNumber);
			if (ce.change.targets != null && ce.change.targets.Length > 0)
				foreach (Target t in ce.change.targets)
					simulator.manager.targetQueue.Add(t);

			// schedule the next change
			Change change = GetNextChange();
			if (change != null)
				simulator.AddEvent( new ChangeEvent(change.change_date.Ticks, new SimAction(ChangeHandler), change));
		}

		public virtual Change GetNextChange()
		{
			return null;
		}

	}
}
