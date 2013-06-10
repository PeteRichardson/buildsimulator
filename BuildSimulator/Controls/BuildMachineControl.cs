using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace PeteRichardson.BuildSimulator
{
	/// <summary>
	/// Summary description for BuildMachineControl.
	/// </summary>
	public class BuildMachineControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label lblMachineName;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Label lblStats;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private BuildMachine machine;
		private Random rnd;

		public BuildMachineControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		public BuildMachineControl(BuildMachine machine) : this()
		{
			this.machine = machine;
			this.rnd = new Random();
			Refresh();
		}

		public void Refresh(long now)
		{

            Action<long> updateStuff = nowparam => {
                lblMachineName.Text = machine.Name;
                lblStatus.Text = machine.status;
                if (machine.status == "Idle")
                    lblStatus.BackColor = Color.Ivory;
                else
                    lblStatus.BackColor = Color.Aquamarine;
                if (machine.buildTime > 0)
                    progressBar.Value = (int)(100 * (nowparam - machine.startTime) / machine.buildTime);
                else
                    progressBar.Value = 0;
                lblStats.Text = String.Format("Builds: {0}, %Util: {1,4}", machine.buildCount, machine.utilization);
            };
            if (InvokeRequired)
                Invoke(updateStuff, now);
            else
                updateStuff(now);
			base.Invalidate();
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

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.lblMachineName = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // lblMachineName
            //
            this.lblMachineName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMachineName.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMachineName.Location = new System.Drawing.Point(0, 0);
            this.lblMachineName.Name = "lblMachineName";
            this.lblMachineName.Size = new System.Drawing.Size(200, 16);
            this.lblMachineName.TabIndex = 0;
            this.lblMachineName.Text = "BM-MOTHRA";
            this.lblMachineName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMachineName.Click += new System.EventHandler(this.lblMachineName_Click);
            //
            // progressBar
            //
            this.progressBar.Location = new System.Drawing.Point(260, 16);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(80, 16);
            this.progressBar.TabIndex = 4;
            //
            // lblStatus
            //
            this.lblStatus.Location = new System.Drawing.Point(0, 16);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(260, 16);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Idle";
            //
            // lblStats
            //
            this.lblStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStats.Location = new System.Drawing.Point(199, 0);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(140, 16);
            this.lblStats.TabIndex = 3;
            this.lblStats.Text = "builds:  0; %util:   0";
            this.lblStats.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // BuildMachineControl
            //
            this.BackColor = System.Drawing.SystemColors.Info;
            this.Controls.Add(this.lblStats);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblMachineName);
            this.Name = "BuildMachineControl";
            this.Size = new System.Drawing.Size(340, 32);
            this.ResumeLayout(false);

		}
		#endregion

        private void lblMachineName_Click(object sender, EventArgs e) {

        }

	}
}
