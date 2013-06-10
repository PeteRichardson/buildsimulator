using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Threading;

namespace WSim
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
        private IContainer components;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			menuItem2_Click(this, null);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null)
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
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            //
            // mainMenu1
            //
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem5});
            //
            // menuItem1
            //
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem6,
            this.menuItem3,
            this.menuItem4});
            this.menuItem1.Text = "&File";
            //
            // menuItem2
            //
            this.menuItem2.Index = 0;
            this.menuItem2.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.menuItem2.Text = "&New";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            //
            // menuItem6
            //
            this.menuItem6.Index = 1;
            this.menuItem6.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItem6.Text = "&Open...";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            //
            // menuItem3
            //
            this.menuItem3.Index = 2;
            this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
            this.menuItem3.Text = "&Close";
            //
            // menuItem4
            //
            this.menuItem4.Index = 3;
            this.menuItem4.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
            this.menuItem4.Text = "E&xit";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            //
            // menuItem5
            //
            this.menuItem5.Index = 1;
            this.menuItem5.MdiList = true;
            this.menuItem5.Text = "&Window";
            //
            // MainForm
            //
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(880, 409);
            this.IsMdiContainer = true;
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "Build Simulator";
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Run(new MainForm());
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			WSim newSimulatorForm = new WSim("nsconfig.xml");
			newSimulatorForm.MdiParent = this;
			newSimulatorForm.Show();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			// kill all threads?
			Process.GetCurrentProcess().CloseMainWindow();
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dlgOpenFile = new OpenFileDialog();

			if(dlgOpenFile.ShowDialog() == DialogResult.OK)
			{
				string configFilePath = dlgOpenFile.FileName;
				WSim newSimulatorForm = new WSim(configFilePath);
				newSimulatorForm.MdiParent = this;
				newSimulatorForm.Show();
			}

		}
	}
}
