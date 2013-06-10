using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace changegrabber
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class ChangeGrabber : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblProject;
		private System.Windows.Forms.Label lblChangeStart;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox tbProjectName;
		private System.Windows.Forms.TextBox tbMaxChanges;
		private System.Windows.Forms.TextBox tbEndingChangeNumber;
		private System.Windows.Forms.TextBox tbStartingChangeNumber;
		private System.Windows.Forms.TextBox tbXmlFileName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbSqlStr;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ChangeGrabber()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			UpdateSqlString();
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
            this.lblProject = new System.Windows.Forms.Label();
            this.tbProjectName = new System.Windows.Forms.TextBox();
            this.tbMaxChanges = new System.Windows.Forms.TextBox();
            this.tbEndingChangeNumber = new System.Windows.Forms.TextBox();
            this.tbStartingChangeNumber = new System.Windows.Forms.TextBox();
            this.lblChangeStart = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tbXmlFileName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSqlStr = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblProject
            // 
            this.lblProject.Location = new System.Drawing.Point(8, 8);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(72, 16);
            this.lblProject.TabIndex = 20;
            this.lblProject.Text = "From project";
            // 
            // tbProjectName
            // 
            this.tbProjectName.Location = new System.Drawing.Point(112, 8);
            this.tbProjectName.Name = "tbProjectName";
            this.tbProjectName.Size = new System.Drawing.Size(208, 20);
            this.tbProjectName.TabIndex = 1;
            this.tbProjectName.Text = "loki";
            this.tbProjectName.TextChanged += new System.EventHandler(this.tbProjectName_TextChanged);
            // 
            // tbMaxChanges
            // 
            this.tbMaxChanges.Location = new System.Drawing.Point(112, 32);
            this.tbMaxChanges.Name = "tbMaxChanges";
            this.tbMaxChanges.Size = new System.Drawing.Size(100, 20);
            this.tbMaxChanges.TabIndex = 2;
            this.tbMaxChanges.Text = "100";
            this.tbMaxChanges.TextChanged += new System.EventHandler(this.tbMaxChanges_TextChanged);
            // 
            // tbEndingChangeNumber
            // 
            this.tbEndingChangeNumber.Location = new System.Drawing.Point(312, 56);
            this.tbEndingChangeNumber.Name = "tbEndingChangeNumber";
            this.tbEndingChangeNumber.Size = new System.Drawing.Size(100, 20);
            this.tbEndingChangeNumber.TabIndex = 4;
            this.tbEndingChangeNumber.TextChanged += new System.EventHandler(this.tbEndingChangeNumber_TextChanged);
            // 
            // tbStartingChangeNumber
            // 
            this.tbStartingChangeNumber.Location = new System.Drawing.Point(112, 56);
            this.tbStartingChangeNumber.Name = "tbStartingChangeNumber";
            this.tbStartingChangeNumber.Size = new System.Drawing.Size(100, 20);
            this.tbStartingChangeNumber.TabIndex = 3;
            this.tbStartingChangeNumber.TextChanged += new System.EventHandler(this.tbStartingChangeNumber_TextChanged);
            // 
            // lblChangeStart
            // 
            this.lblChangeStart.Location = new System.Drawing.Point(8, 56);
            this.lblChangeStart.Name = "lblChangeStart";
            this.lblChangeStart.Size = new System.Drawing.Size(96, 16);
            this.lblChangeStart.TabIndex = 25;
            this.lblChangeStart.Text = "starting at change";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 26;
            this.label1.Text = "grab at most";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(216, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 27;
            this.label2.Text = "changes";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(216, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 28;
            this.label3.Text = "ending at change";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(336, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Grab!";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbXmlFileName
            // 
            this.tbXmlFileName.Location = new System.Drawing.Point(112, 88);
            this.tbXmlFileName.Name = "tbXmlFileName";
            this.tbXmlFileName.Size = new System.Drawing.Size(304, 20);
            this.tbXmlFileName.TabIndex = 5;
            this.tbXmlFileName.Text = "changes.xml";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Save in Xml file";
            // 
            // tbSqlStr
            // 
            this.tbSqlStr.Location = new System.Drawing.Point(8, 152);
            this.tbSqlStr.Multiline = true;
            this.tbSqlStr.Name = "tbSqlStr";
            this.tbSqlStr.Size = new System.Drawing.Size(408, 224);
            this.tbSqlStr.TabIndex = 12;
            // 
            // ChangeGrabber
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(424, 382);
            this.Controls.Add(this.tbSqlStr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbXmlFileName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblChangeStart);
            this.Controls.Add(this.tbStartingChangeNumber);
            this.Controls.Add(this.tbEndingChangeNumber);
            this.Controls.Add(this.tbMaxChanges);
            this.Controls.Add(this.tbProjectName);
            this.Controls.Add(this.lblProject);
            this.Name = "ChangeGrabber";
            this.Text = "Change Grabber";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Run(new ChangeGrabber());
		}

		private void UpdateSqlString()
		{
			string maxChanges = "";
			if (tbMaxChanges.Text != "") maxChanges = "top " + tbMaxChanges.Text;

			string projectName = "loki";
			if (tbProjectName.Text != "") projectName = tbProjectName.Text;

			string startingChangeNumber = "";
			if (tbStartingChangeNumber.Text != "")
				startingChangeNumber = String.Format("(target_change_number >= {0}) AND\r\n\t", tbStartingChangeNumber.Text);
			string endingChangeNumber = "";
			if (tbEndingChangeNumber.Text != "")
				endingChangeNumber = String.Format("(target_change_number <= {0}) AND\r\n\t", tbEndingChangeNumber.Text);

			string sqlStr =
@"SELECT  {0} change_id, change_date, target_name,
target_time_requested, target_clean, target_successful, target_time_started,
target_time_finished, target_build_time
FROM target
INNER JOIN project ON target.target_project_id = project._id
INNER JOIN change on target_change_number = change_id
WHERE	(target.target_bmonkey = 1) AND
	(project.project_name = '{1}') AND
	(target.target_path_root = 1) AND
	{2}{3}(target.target_status = 20)
ORDER BY change_id
for XML AUTO";
			tbSqlStr.Text = string.Format(sqlStr, maxChanges, projectName, startingChangeNumber, endingChangeNumber);
		}


		private void tbProjectName_TextChanged(object sender, System.EventArgs e)
		{
			UpdateSqlString();
		}

		private void tbMaxChanges_TextChanged(object sender, System.EventArgs e)
		{
			UpdateSqlString();
		}

		private void tbStartingChangeNumber_TextChanged(object sender, System.EventArgs e)
		{
			UpdateSqlString();
		}

		private void tbEndingChangeNumber_TextChanged(object sender, System.EventArgs e)
		{
			UpdateSqlString();
		}


		private void button1_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				StreamWriter outputFile = new StreamWriter(tbXmlFileName.Text, false);
				outputFile.WriteLine("<changes project=\"" + tbProjectName.Text + "\">");
				outputFile.Flush();

                string connectionString = "Provider=SQLOLEDB;server=BUILD-SERVER;Database=Builds;UID=admin;PWD=password123;";
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();
                    SqlCommand command = new SqlCommand(tbSqlStr.Text, connection);
                    System.Xml.XmlReader reader = command.ExecuteXmlReader();
                    using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(outputFile)) {
                        writer.WriteNode(reader, true);
                    }

                }

                outputFile.WriteLine("</changes>");
				outputFile.Close();
				tbSqlStr.Text = "Completed!";
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

	}
}
