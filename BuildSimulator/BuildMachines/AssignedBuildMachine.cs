using System;

namespace PeteRichardson.BuildSimulator
{
	/// <summary>
	/// This is a buildmachine that remembers what flavor it built last and should only be assigned
	/// builds of that same flavor.
	/// </summary>
	public class AssignedFlavorMachine : BuildMachine
	{
		public string lastFlavor;

		public override string Name
		{
			get { return String.Format("{0} ({1})", name, lastFlavor); }
			set { name = value; }
		}


		public AssignedFlavorMachine( Simulator simulator, string name, string flavor) : base(simulator, name)
		{
			this.lastFlavor = flavor;
		}
	}
}
