using System;
using PeteRichardson.BuildSimulator;

namespace PeteRichardson.BuildSimulator.TestApps
{

	class CSim
	{
		[STAThread]
		static void Main(string[] args)
		{
		    BuildSimulatorConfiguration configuration = new BuildSimulatorConfiguration();
            configuration.LoadFromXml("config.xml");
            Simulator simulator = new Simulator(configuration);
			simulator.manager = new BuildManager(simulator);
			simulator.changeSource = new XmlChangeSource(simulator, @"wsim\changes.xml");
			simulator.clock.Set(DateTime.Parse("2002/12/26 09:19"));

			BuildMachine[] machinePool = new BuildMachine[] {
								new AssignedFlavorMachine(simulator, "BLD-08", "loki-client-Yeti-release"),
								new AssignedFlavorMachine(simulator, "BLD-09", "loki-server-Generic-debug"),
							//	new BuildMachine(simulator, "BLD-14")
			};
			simulator.machinePool = machinePool;

			//simulator.Init();
			simulator.Run();
			//simulator.Cleanup();

			Console.Write("Press any key to continue...");
			Console.Read();
		}
	}

}
