using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PeteRichardson.BuildSimulator;

namespace WSim
{
	public class WSim : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btStart;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox tbLog;
		private System.Windows.Forms.TextBox tbClockScale;
		private System.Windows.Forms.Label lblScale;
		private System.Windows.Forms.Label lblPendingEvents;
		private System.Windows.Forms.Label lblPendingEventsLabel;
		private Simulator simulator;
		private BuildMachineControl[] buildMachineControls;
		private System.Windows.Forms.Button btStop;
		private BuildSimulatorConfiguration configuration;
		private bool formDirty=false;

		public WSim(string configFilePath)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			configuration = new BuildSimulatorConfiguration();
			configuration.LoadFromXml(configFilePath);
			BuildForm();
		}

		public void BuildForm()
		{
			tbLog.Text = "";
			simulator = new Simulator(configuration);
			Simulator.SimEventEventHandler handler = new Simulator.SimEventEventHandler(OnSimEvent);
			simulator.OnSimEventHandler += handler;

			// remove old buildMachineControls
			if (buildMachineControls != null)
				foreach (BuildMachineControl oldbmc in buildMachineControls)
				{
					if(this.Controls.Contains(oldbmc))
					{
						this.Controls.Remove(oldbmc);
						oldbmc.Dispose();
					}
				}

			int bmIndex = 0;
			buildMachineControls = new BuildMachineControl[simulator.machinePool.Length];
			for (bmIndex = 0; bmIndex < simulator.machinePool.Length; bmIndex++)
			{
				BuildMachineControl bmc = new BuildMachineControl(simulator.machinePool[bmIndex]);
				bmc.Location = new Point (10, 70 + bmIndex * 40);
				buildMachineControls[bmIndex] = bmc;
				this.Controls.Add(bmc);
			}
			formDirty=false;
			Redraw();
		}

		private void btStart_Click(object sender, System.EventArgs e)
		{
			if (formDirty)
				BuildForm();
			formDirty = true;
			simulator.Restart();
		}

		private void btStop_Click(object sender, System.EventArgs e)
		{
			simulator.Abort();
		}

		public void OnSimEvent(object sender, SimEventEventArgs e)
		{
            Action<int> updateStuff = i =>
            {
                tbLog.AppendText(e.EventObj.ToString() + "\r\n");
                lblPendingEvents.Text = simulator.PendingEvents.ToString();
                if (e.EventObj is SimulationFinishedEvent)
                    tbLog.AppendText(String.Format("Virtual Time: {0}\tActual Time {1}", simulator.virtualTime, simulator.actualTime));
            };
            if (InvokeRequired)
                Invoke(updateStuff, 3);
            else
                updateStuff(3);
			Redraw();
		}

		public  void Redraw()
		{
			tbLog.Invalidate(new Rectangle(tbLog.Location, tbLog.Size));
			lblPendingEvents.Invalidate(new Rectangle(lblPendingEvents.Location, lblPendingEvents.Size));
			foreach (BuildMachineControl bmc in buildMachineControls)
				bmc.Refresh(simulator.clock.Now);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.btStart = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.tbClockScale = new System.Windows.Forms.TextBox();
            this.lblScale = new System.Windows.Forms.Label();
            this.lblPendingEvents = new System.Windows.Forms.Label();
            this.lblPendingEventsLabel = new System.Windows.Forms.Label();
            this.btStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // btStart
            //
            this.btStart.Location = new System.Drawing.Point(8, 8);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 0;
            this.btStart.Text = "Start";
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            //
            // tbLog
            //
            this.tbLog.AcceptsReturn = true;
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLog.Location = new System.Drawing.Point(368, 0);
            this.tbLog.MaxLength = 100000;
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLog.Size = new System.Drawing.Size(496, 366);
            this.tbLog.TabIndex = 1;
            this.tbLog.WordWrap = false;
            //
            // tbClockScale
            //
            this.tbClockScale.Location = new System.Drawing.Point(232, 40);
            this.tbClockScale.Name = "tbClockScale";
            this.tbClockScale.Size = new System.Drawing.Size(56, 20);
            this.tbClockScale.TabIndex = 2;
            this.tbClockScale.Text = "0";
            this.tbClockScale.TextChanged += new System.EventHandler(this.tbClockScale_TextChanged);
            //
            // lblScale
            //
            this.lblScale.AutoSize = true;
            this.lblScale.Location = new System.Drawing.Point(160, 40);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(64, 13);
            this.lblScale.TabIndex = 3;
            this.lblScale.Text = "Clock Scale";
            this.lblScale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblPendingEvents
            //
            this.lblPendingEvents.Location = new System.Drawing.Point(96, 40);
            this.lblPendingEvents.Name = "lblPendingEvents";
            this.lblPendingEvents.Size = new System.Drawing.Size(56, 16);
            this.lblPendingEvents.TabIndex = 4;
            this.lblPendingEvents.Text = "0";
            this.lblPendingEvents.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblPendingEventsLabel
            //
            this.lblPendingEventsLabel.AutoSize = true;
            this.lblPendingEventsLabel.Location = new System.Drawing.Point(8, 40);
            this.lblPendingEventsLabel.Name = "lblPendingEventsLabel";
            this.lblPendingEventsLabel.Size = new System.Drawing.Size(82, 13);
            this.lblPendingEventsLabel.TabIndex = 5;
            this.lblPendingEventsLabel.Text = "Pending Events";
            this.lblPendingEventsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btStop
            //
            this.btStop.Location = new System.Drawing.Point(96, 8);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(75, 23);
            this.btStop.TabIndex = 6;
            this.btStop.Text = "Stop";
            //
            // WSim
            //
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(864, 366);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.lblPendingEventsLabel);
            this.Controls.Add(this.lblScale);
            this.Controls.Add(this.tbClockScale);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.lblPendingEvents);
            this.Controls.Add(this.btStart);
            this.Name = "WSim";
            this.Text = "Hudson";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


		private void tbClockScale_TextChanged(object sender, System.EventArgs e)
		{
			if (tbClockScale.Text == "")
				simulator.clock.Scale = 0;
			else
				simulator.clock.Scale = Convert.ToInt32(tbClockScale.Text);
		}
	}
}
