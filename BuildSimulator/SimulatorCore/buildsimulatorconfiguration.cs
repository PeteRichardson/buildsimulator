using System;

namespace PeteRichardson.BuildSimulator
{
	public class BuildSimulatorConfiguration
	{
		public DateTime startTime;
		public BuildSimulatorConfiguration()
		{
			startTime = DateTime.Parse("5/12/2010 12:00:00 AM");
		}
		public void LoadFromXml(string configFilePath)
		{
		}
	}
}
