using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;


namespace PeteRichardson.BuildSimulator
{

	public class SimEventEventArgs : EventArgs
	{
		protected object eventObj;

		public SimEventEventArgs(object eventObj)
		{
			this.eventObj = eventObj;
		}

		public object EventObj
		{
			get { return eventObj; }
		}
	}

	public class Simulator
	{
		private EventQueue eq = new EventQueue();
		public Clock clock = null;
		public BuildMachine[] machinePool = null;
		public BuildManager manager = null;
		public ChangeSource changeSource = null;
		public long firstEventVirtualTime;
		public long lastEventVirtualTime;
		public TimeSpan virtualTime;
		public TimeSpan actualTime;
		public long firstEventActualTime;
		public long lastEventActualTime;
		public bool StopOnIdle = true;
		public int nonIdleEvents = 0;
		private Thread backgroundThread;
		private BuildSimulatorConfiguration configuration;
		public int PendingEvents
		{
			get { return nonIdleEvents + manager.targetQueue.Count; }
		}

		public delegate void SimEventEventHandler (object sendr, SimEventEventArgs e);

		public event SimEventEventHandler OnSimEventHandler;

		public Simulator(BuildSimulatorConfiguration configuration)
		{
			this.configuration = configuration;
			InitializeFromConfiguration();
		}

		public void InitializeFromConfiguration()
		{
			clock = new Clock(this);

			manager = new BuildManager(this);
			//simulator.changeSource = new XmlChangeSource(simulator, @"c:\pete\littletools\BuildSimulator\wsim\changes.xml");
			changeSource = new RandomChangeSource(this,50);
			//simulator.clock.Set(DateTime.Parse("5/19/2003 8:00:00 AM"));
			clock.Set(configuration.startTime);

			machinePool = new BuildMachine[] {
												 new AssignedFlavorMachine(this, "BLD05", "Thor_hammer_milestone"),
												 new AssignedFlavorMachine(this, "BLD06", "Thor_hammer_milestone"),
												 new AssignedFlavorMachine(this, "BLD08", "Parallel_Thor_hammer"),
												 new AssignedFlavorMachine(this, "BLD13", "Loki")
											 };
		}

		public void Run()
		{
			// Init everything
			clock.Init();
			changeSource.Init();

			manager.Init();
			foreach (BuildMachine bm in machinePool)
				bm.Init();

			if (eq.Count == 0)
				throw new ApplicationException("No events in the event queue");

			SimEvent se = eq.Pop();

			while (true)
			{
				if (StopOnIdle && nonIdleEvents == 0 && manager.targetQueue.Count == 0)
					break;

				clock.Set(se.virtualTime);
				if (!(se is MachineIdleEvent) && !(se is ClockEvent))
				{
					if (firstEventActualTime == 0)
					{
						firstEventActualTime = se.actualTime;
						firstEventVirtualTime = se.virtualTime;
					}
					nonIdleEvents--;
					SimEventEventArgs args = new SimEventEventArgs(se);
					OnSimEvent(args);
				}
				if (se.someAction != null)
					se.someAction(se);

				se = eq.Pop();
			}
			lastEventActualTime = se.actualTime;
			lastEventVirtualTime = se.virtualTime;
			virtualTime = new TimeSpan(lastEventVirtualTime - firstEventVirtualTime);
			actualTime = new TimeSpan(lastEventActualTime - firstEventActualTime);
			OnSimEvent(new SimEventEventArgs(new SimulationFinishedEvent(lastEventVirtualTime)));

			// Cleanup everything
			changeSource.Cleanup();
			foreach (BuildMachine bm in machinePool)
				bm.Cleanup();
			manager.Cleanup();

		}

		public void Restart()
		{
			if (backgroundThread != null && backgroundThread.IsAlive)
				backgroundThread.Abort();
			backgroundThread = new Thread(new ThreadStart(this.Run));
			if (backgroundThread.ThreadState != System.Threading.ThreadState.Unstarted)
				InitializeFromConfiguration();
			backgroundThread.Start();
		}

		public void Abort()
		{
			backgroundThread.Abort();
		}

		public void OnSimEvent(SimEventEventArgs e)
		{
			if (OnSimEventHandler != null)
				OnSimEventHandler(this, e);
		}

		public void AddEvent(SimEvent se)
		{
			if (!(se is MachineIdleEvent) && !(se is ClockEvent))
				nonIdleEvents++;
			eq.Add(se);
		}

		private class EventQueue : SortedList
		{
			public void Add(SimEvent se)
			{
				long newtime = se.virtualTime;
				while (base.ContainsKey(newtime))
					newtime++;
				se.virtualTime = newtime;
				base.Add (se.virtualTime, se);
			}

			public SimEvent Pop()
			{
				SimEvent se = null;
				if (this.Count > 0)
				{
					se = (SimEvent) GetByIndex(0);
					RemoveAt(0);
				}
				Debug.Assert(se != null, "No events in event queue!  Not even Idle events!");
				se.actualTime = DateTime.Now.Ticks;
				return se;
			}
		}

	}

	public delegate void SimAction(object sender);

	public class SimEvent
	{
		public long virtualTime;
		public long actualTime;
		public SimAction someAction = null;

		public SimEvent(long virtualTime, SimAction myAction)
		{
			this.virtualTime = virtualTime;
			this.someAction = myAction;
		}

		public override string ToString()
		{
			return String.Format("{0}: [{2}]", new DateTime(virtualTime), this.someAction.ToString());
		}
	}

	public class SimulationFinishedEvent : SimEvent
	{
		public SimulationFinishedEvent(long timestamp): base(timestamp, null)
		{
		}

		public override string ToString()
		{
			return String.Format("[{0,-22}] Simulation Finished.", new DateTime(virtualTime));
		}
	}

	public class SimEntity
	{
		protected Simulator simulator = null;
		public virtual void Init()
		{
		}
		public virtual void Cleanup()
		{
		}
		public SimEntity(Simulator simulator)
		{
			this.simulator = simulator;
		}
	}

}
