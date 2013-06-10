using System;
using System.Collections;
using System.Xml;

namespace PeteRichardson.BuildSimulator
{
	public class XmlChangeSource : ChangeSource
	{
		private string xmlFileName;
		XmlTextReader reader;

		public XmlChangeSource( Simulator sim, string xmlFileName) : base(sim)
		{
			this.xmlFileName = xmlFileName;
		}

		public override void Init()
		{
			reader = new XmlTextReader(xmlFileName);
			reader.WhitespaceHandling = WhitespaceHandling.None;
			Change change = GetNextChange(reader);
			simulator.AddEvent( new ChangeEvent(change.change_date.Ticks, new SimAction(ChangeHandler), change));
		}

		public override void Cleanup()
		{
			if (reader != null)
				reader.Close();
		}

		public override void ChangeHandler(object sender)
		{
			ChangeEvent ce = (ChangeEvent) sender;

			// Queue the builds for this change
			// Console.WriteLine("\tQueueing builds for change #{0}", ce.changeNumber);
			if (ce.change.targets != null && ce.change.targets.Length > 0)
				foreach (Target t in ce.change.targets)
					simulator.manager.targetQueue.Add(t);

			// schedule the next change
			Change change = GetNextChange(reader);
			if (change != null)
				simulator.AddEvent( new ChangeEvent(change.change_date.Ticks, new SimAction(ChangeHandler), change));
		}


		static Change GetNextChange (XmlTextReader reader)
		{
			while (reader.Read())
				if (reader.NodeType == XmlNodeType.Element && reader.Name == "change") break;
			if (reader.EOF) return null;

			Change change = new Change();
			change.change_id = Convert.ToInt32(reader["change_id"]);
			change.change_date = DateTime.Parse(reader["change_date"]);

			ArrayList foundTargets = new ArrayList();
			Target t = GetNextTarget(change.change_id, reader);
			while ( t != null )
			{
				foundTargets.Add(t);
				t = GetNextTarget(change.change_id, reader);
			}
			change.targets = (Target[]) foundTargets.ToArray(typeof(Target));

			return change;
		}

		static Target GetNextTarget (int buildNumber, XmlTextReader reader)
		{
			reader.Read();
			if (reader.EOF || reader.NodeType != XmlNodeType.Element || reader.Name != "target") return null;

			Target target = new Target();
			target.buildNumber = buildNumber;
			target.name = reader["target_name"];
			target.time_requested = Convert.ToDateTime(reader["target_time_requested"]);
			target.time_started = Convert.ToDateTime(reader["target_time_started"]);
			target.time_finished = Convert.ToDateTime(reader["target_time_finished"]);
			target.build_time = target.time_finished.Subtract(target.time_started);
			target.successful = Convert.ToInt32(reader["target_successful"]);
			target.clean = reader["target_clean"] == "0" ? false : true;

			return target;
		}


	}
}
