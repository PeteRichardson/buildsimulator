using System;
using System.Collections;

namespace PeteRichardson.BuildSimulator
{
	public class BuildManager : SimEntity
	{
		public ArrayList targetQueue;

		public BuildManager(Simulator simulator) : base(simulator)
		{
			targetQueue = new ArrayList();
		}

		public Target AssignTarget(BuildMachine machine)
		{
			if ( targetQueue.Count == 0)
				return null;

			if (machine is AssignedFlavorMachine)
			{
				string lastFlavor = ((AssignedFlavorMachine) machine).lastFlavor;
				foreach (Target target in targetQueue)
					if (lastFlavor == target.name)
					{
						targetQueue.Remove(target);
						return target;
					}
			}
			else
			{
				Target target = (Target) targetQueue[0];
				targetQueue.Remove(target);
				return target;
			}

			return null;
		}

	}
}
