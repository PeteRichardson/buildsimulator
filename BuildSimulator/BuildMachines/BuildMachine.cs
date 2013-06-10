using System;

namespace PeteRichardson.BuildSimulator
{
	public class BuildMachine : SimEntity
	{
		protected string name = "unnamed";
		public string status = "Idle";
		public int buildCount = 0;
		protected long aliveTime;
		protected TimeSpan inUseTime;
		public float utilization = 0;
		public long startTime = 0;
		public long buildTime = 0;

		public BuildMachine(Simulator simulator, string name) : base(simulator)
		{
			this.Name = name;
		}

		public virtual string Name
		{
			get { return name; }
			set { name = value; }
		}

		public override void Init()
		{
			simulator.AddEvent( new MachineIdleEvent(simulator.clock.Now, new SimAction(idleHandler), this ));
		}

		public void BuildStartedHandler(object sender)
		{
			// schedule a buildFinished event for the time this build ends
			BuildEvent be = (BuildEvent) sender;
			startTime = simulator.clock.Now;
			buildTime = be.target.build_time.Ticks;
			aliveTime += be.target.build_time.Ticks;
			long timeFinished = new DateTime(simulator.clock.Now).Add(be.target.build_time).Ticks;
			simulator.AddEvent( new BuildEvent(timeFinished,
				new SimAction(BuildFinishedHandler), this, be.target, "finished") );
			status = String.Format("Building {0}: {1}", be.target.buildNumber, be.target.name);
			buildCount++;
		}

		public void BuildFinishedHandler(object sender)
		{
			simulator.AddEvent( new MachineIdleEvent(simulator.clock.Now, new SimAction(idleHandler), this) );
			startTime = simulator.clock.Now;
			buildTime = 0;
			status = "Idle";
		}

		public void idleHandler(object sender)
		{
			long overalltime = simulator.clock.Now - simulator.firstEventVirtualTime;
			utilization = overalltime > 0 ? aliveTime * 100 / overalltime : 0;
			// ask the manager if it wants to assign a build...
			Target target = simulator.manager.AssignTarget(this);
			if (target != null)
				simulator.AddEvent( new BuildEvent(simulator.clock.Now, new SimAction(BuildStartedHandler), this, target, "started") );
			else
			{
				// check each minute for stuff to do.
				simulator.AddEvent( new MachineIdleEvent(simulator.clock.MinutesFromNow(1), new SimAction(idleHandler), this) );
			}
		}
	}


	public class MachineIdleEvent : SimEvent
	{
		public BuildMachine machine;

		public MachineIdleEvent(long timestamp, SimAction myAction, BuildMachine machine):
			base(timestamp, myAction)
		{
			this.machine = machine;
		}


		public override string ToString()
		{
			return String.Format("[{0,-22}] {1,-14} - {2}",
				new DateTime(virtualTime), machine.Name, machine.status);
		}
	}

	public class BuildEvent : SimEvent
	{
		public BuildMachine machine;
		public Target target;
		string buildAction;

		public BuildEvent(long virtualTime, SimAction myAction, BuildMachine machine, Target target, string buildAction):
			base(virtualTime, myAction )
		{
			this.machine = machine;
			this.target = target;
			this.buildAction = buildAction;
		}

		public override string ToString()
		{
			return String.Format("[{0,-22}] {1,-14} - build {2,-8} - {3} {4}",
				new DateTime(virtualTime), machine.Name, target.buildNumber, buildAction , target.name );
		}
	}
}
