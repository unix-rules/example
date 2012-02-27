using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project {



    public partial class MainForm : Form
    {
        private RenderView renderView1;
        private TextBox textBox1;
        private Button button1;
        //private RenderView view;

		public MainForm()
		{
            InitializeComponent();
		//	this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		//	this.ClientSize = new System.Drawing.Size(640, 480);
		//	this.Name = "MainForm";
		//	this.Text = "project";
            //this.view = new Project.OpenGLForm();			
			//this.view.Parent = this;
			//this.view.Dock = DockStyle.Fill; // Will fill whole form
		//	this.Show();
		}

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.renderView1 = new Project.RenderView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(377, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Set parameter";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // renderView1
            // 
            this.renderView1.Location = new System.Drawing.Point(12, 53);
            this.renderView1.Name = "renderView1";
            this.renderView1.Size = new System.Drawing.Size(460, 305);
            this.renderView1.TabIndex = 2;
            this.renderView1.Text = "renderView1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(359, 20);
            this.textBox1.TabIndex = 3;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(484, 370);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.renderView1);
            this.Controls.Add(this.button1);
            this.Name = "MainForm";
            this.Text = "Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       // [STAThread]
       //// static void Main() 
		//{
            //new Project.OpenGLForm();


          //  MainForm form = new MainForm();


        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new MainForm());

            //while(true)
            //while ((!form.view.finished) && (!form.IsDisposed))		// refreshing the window, so it rotates
            //{
             //   form.view.glDraw();
            //    form.Refresh();
            //    Application.DoEvents();
            //}

            //form.Dispose();
		//}
	}
}
