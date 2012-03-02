using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.Threading;

namespace Project {



    public partial class MainForm : Form
    {
        private RenderView renderView1;
        private TextBox timeBox;
        private Button button1;
        private int timeFromBox = 30;
        //private RenderView view;
        private System.Timers.Timer t = null;

        private void setupTimer (object sender, System.EventArgs e) {
                try{
                    timeFromBox = Convert.ToInt32(timeBox.Text);
                } catch (Exception er) {
                    timeFromBox = 30;
                }

                if (timeFromBox < 3)
                {
                    timeFromBox = 30;
                }
                startTimer(timeFromBox);
        }
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


        public void startTimer(int dueTime)  {

            if (t != null)
            {
                t.Stop();
            }
            //if (t == null) {
                t = new System.Timers.Timer(dueTime/100.0);
                //t = new Timer(new TimerCallback(TimerProc));
            //}
            //t.Interval = 1000.0;
            t.Elapsed += new ElapsedEventHandler(ProcessTimerEvent);
            
            // timer.Elapsed += new ElapsedEventHandler(ProcessTimerEvent);

            t.AutoReset = true;
            t.Start();
        }

        private void ProcessTimerEvent(Object obj, ElapsedEventArgs e)
        {
          //  Thread demoThread = new Thread(new ThreadStart(this.ThreadProcUnsafe));
         //   demoThread.Start();

           
            renderView1.Invoke((MethodInvoker)delegate(){
                   renderView1.Refresh();
             });
            
        }

        //private void ThreadProcUnsafe() {
        //   renderView1.Refresh();
       // }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.renderView1 = new Project.RenderView();
            this.timeBox = new System.Windows.Forms.TextBox();
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
            this.button1.Click += new System.EventHandler(this.setupTimer);
            // 
            // renderView1
            // 
            this.renderView1.Location = new System.Drawing.Point(12, 53);
            this.renderView1.Name = "renderView1";
            this.renderView1.Size = new System.Drawing.Size(460, 305);
            this.renderView1.TabIndex = 2;
            this.renderView1.Text = "renderView1";
            // 
            // timeBox
            // 
            this.timeBox.Location = new System.Drawing.Point(12, 14);
            this.timeBox.Name = "timeBox";
            this.timeBox.Size = new System.Drawing.Size(359, 20);
            this.timeBox.TabIndex = 3;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(484, 370);
            this.Controls.Add(this.timeBox);
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
