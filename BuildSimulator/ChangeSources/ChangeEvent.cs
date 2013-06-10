using System;

namespace PeteRichardson.BuildSimulator
{
	public class ChangeEvent : SimEvent
	{
		public Change change;

		public ChangeEvent(long timestamp, SimAction changeHandler, Change change ):
			base(timestamp, changeHandler)
		{
			this.change = change;
		}

		public override string ToString()
		{
			return String.Format("[{0,-22}] change {1} - {2}", new DateTime(virtualTime), change.change_id, change.clean ? "clean" : "");
		}
	}
}
