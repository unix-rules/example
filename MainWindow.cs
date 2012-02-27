using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CsGL.OpenGL;
using CsGL;

using System.Drawing;

namespace Project {

    public class OpenGLForm : OpenGLControl {

		public float rtri;			// rtri is for rotating the pyramid
		public float rquad;			// rquad is for rotating the quad

		public bool finished;




        public bool light = true;				// Lighting ON/OFF ( NEW )
        public bool lp = false;					// L Pressed? ( NEW )
        public bool fp = false;					// F Pressed? ( NEW )

        public float xrot = 0.0f;				// X-axis rotation
        public float yrot = 0.0f;				// Y-axis rotation
        public float zrot = 0.0f;
        public float xspeed = 0.0f;				// X Rotation Speed
        public float yspeed = 0.0f;				// Y Rotation Speed
        public float z = -5.0f;					// Depth Into The Screen

        // Lighting components for the cube
        public float[] LightAmbient = { 0.5f, 0.5f, 0.5f, 1.0f };
        public float[] LightDiffuse = { 1.0f, 1.0f, 1.0f, 1.0f };
        public float[] LightPosition = { 0.0f, 0.0f, 2.0f, 1.0f };

        public int filter = 0;					// Which Filter To Use
        public uint[] texture = new uint[3];	// Texture array



        public OpenGLForm()
            : base()
		{
			this.KeyDown += new KeyEventHandler(OurView_OnKeyDown);
			finished = false;
		}
		
		protected void OurView_OnKeyDown(object Sender, KeyEventArgs kea)
		{
			//if escape was pressed exit the application
			if (kea.KeyCode == Keys.Escape) 
			{
				finished = true;
			}
		}

        protected bool LoadTextures()
        {
            Bitmap image = null;
            //string file = @"Data\Crate.bmp";
            string file = @"C:\Users\Artur\Desktop\lesson07\Data\Crate.bmp";
            //C:\Users\hp\Desktop\PhD 2nd Stage\c#\lesson07\lesson07\Data
            try
            {
                // If the file doesn't exist or can't be found, an ArgumentException is thrown instead of
                // just returning null
                image = new Bitmap(file);
            }
            catch (System.ArgumentException)
            {
                MessageBox.Show("Could not load " + file + ".  Please make sure that Data is a subfolder from where the application is running.", "Error", MessageBoxButtons.OK);
                this.finished = true;
            }
            if (image != null)
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                System.Drawing.Imaging.BitmapData bitmapdata;
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

                bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                GL.glGenTextures(3, this.texture);

                // Create Nearest Filtered Texture
                GL.glBindTexture(GL.GL_TEXTURE_2D, this.texture[0]);
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, GL.GL_NEAREST);
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, GL.GL_NEAREST);
                GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGB, image.Width, image.Height, 0, GL.GL_BGR_EXT, GL.GL_UNSIGNED_BYTE, bitmapdata.Scan0);

                // Create Linear Filtered Texture
                GL.glBindTexture(GL.GL_TEXTURE_2D, this.texture[1]);
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, GL.GL_LINEAR);
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, GL.GL_LINEAR);
                GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGB, image.Width, image.Height, 0, GL.GL_BGR_EXT, GL.GL_UNSIGNED_BYTE, bitmapdata.Scan0);

                // Create MipMapped Texture
                GL.glBindTexture(GL.GL_TEXTURE_2D, this.texture[2]);
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, GL.GL_LINEAR);
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, GL.GL_LINEAR_MIPMAP_NEAREST);
                GL.gluBuild2DMipmaps(GL.GL_TEXTURE_2D, (int)GL.GL_RGB, image.Width, image.Height, GL.GL_BGR_EXT, GL.GL_UNSIGNED_BYTE, bitmapdata.Scan0);

                image.UnlockBits(bitmapdata);
                image.Dispose();
                return true;
            }
            return false;
        }

		public override void glDraw()
		{
			GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);	// Clear the Screen and the Depth Buffer
			GL.glMatrixMode(GL.GL_MODELVIEW);		// Modelview Matrix
			GL.glLoadIdentity();					// reset the current modelview matrix
			GL.glTranslatef(-1.5f,0.0f,-6.0f);		// move 1.5 Units left and 6 Units into the screen
			GL.glRotatef(rtri,0.0f,1.0f,0.0f);		// rotate the Pyramid on it's Y-axis
			rtri+=0.2f;								// rotation angle

			GL.glBegin(GL.GL_TRIANGLES);			// start drawing a triangle, always counterclockside (top-left-right)
			GL.glColor3f(1.0f,0.0f,0.0f);			// Red
			GL.glVertex3f(0.0f,1.0f,0.0f);			// Top of Triangle (Front)
			GL.glColor3f(0.0f,1.0f,0.0f);			// green
			GL.glVertex3f(-1.0f,-1.0f,1.0f);		// left of Triangle (front)
			GL.glColor3f(0.0f,0.0f,1.0f);			// blue
			GL.glVertex3f(1.0f,-1.0f,1.0f);			// right of triangle (front)

			GL.glColor3f(1.0f,0.0f,0.0f);			// red
			GL.glVertex3f(0.0f,1.0f,0.0f);			// top of triangle (right)
			GL.glColor3f(0.0f,0.0f,1.0f);			// blue
			GL.glVertex3f(1.0f,-1.0f,1.0f);			// left of triangle (right)
			GL.glColor3f(0.0f,1.0f,0.0f);			// green
			GL.glVertex3f(1.0f,-1.0f,-1.0f);		// right of triangel (right)

			GL.glColor3f(1.0f,0.0f,0.0f);			// red
			GL.glVertex3f(0.0f,1.0f,0.0f);			// top of triangle (back)
			GL.glColor3f(0.0f,1.0f,0.0f);			// green
			GL.glVertex3f(1.0f,-1.0f,-1.0f);		// left of triangle (back)
			GL.glColor3f(0.0f,0.0f,1.0f);			// blue
			GL.glVertex3f(-1.0f,-1.0f,-1.0f);		// right of triangle (back)

			GL.glColor3f(1.0f,0.0f,0.0f);			// red
			GL.glVertex3f(0.0f,1.0f,0.0f);			// top of triangle (left)
			GL.glColor3f(0.0f,0.0f,1.0f);			// blue
			GL.glVertex3f(-1.0f,-1.0f,-1.0f);		// left of triangle (left)
			GL.glColor3f(0.0f,1.0f,0.0f);			// green
			GL.glVertex3f(-1.0f,-1.0f,1.0f);		// right of triangle (left)
			GL.glEnd();

			GL.glLoadIdentity();					// reset the current modelview matrix
            GL.glTranslatef(1.5f,0.0f,-7.0f);		// move 1.5 Units right and 7 into the screen
			GL.glRotatef(rquad,1.0f,1.0f,1.0f);		// rotate the quad on the X,Y and Z-axis
			rquad-=0.15f;							// rotation angle


            GL.glBindTexture(GL.GL_TEXTURE_2D, this.texture[filter]);

			GL.glBegin(GL.GL_QUADS);				// start drawing a quad
            // Front Face
            GL.glNormal3f(0.0f, 0.0f, 1.0f);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-1.0f, -1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(1.0f, -1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(1.0f, 1.0f, 1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-1.0f, 1.0f, 1.0f);
            // Back Face
            GL.glNormal3f(0.0f, 0.0f, -1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-1.0f, 1.0f, -1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(1.0f, 1.0f, -1.0f);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(1.0f, -1.0f, -1.0f);
            // Top Face
            GL.glNormal3f(0.0f, 1.0f, 0.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-1.0f, 1.0f, -1.0f);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-1.0f, 1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(1.0f, 1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(1.0f, 1.0f, -1.0f);
            // Bottom Face
            GL.glNormal3f(0.0f, -1.0f, 0.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(1.0f, -1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-1.0f, -1.0f, 1.0f);
            // Right face
            GL.glNormal3f(1.0f, 0.0f, 0.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(1.0f, 1.0f, -1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(1.0f, 1.0f, 1.0f);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(1.0f, -1.0f, 1.0f);
            // Left Face
            GL.glNormal3f(-1.0f, 0.0f, 0.0f);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-1.0f, -1.0f, -1.0f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-1.0f, -1.0f, 1.0f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-1.0f, 1.0f, 1.0f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-1.0f, 1.0f, -1.0f);
			GL.glEnd();

		}

		protected override void InitGLContext() 
		{
            LoadTextures();

            GL.glEnable(GL.GL_TEXTURE_2D);	

		//	GL.glShadeModel(GL.GL_SMOOTH);								// enable smooth shading
			GL.glClearColor(0.0f, 0.0f, 0.0f, 0.5f);					// black background
			GL.glClearDepth(1.0f);										// depth buffer setup
			GL.glEnable(GL.GL_DEPTH_TEST);								// enables depth testing
			GL.glDepthFunc(GL.GL_LEQUAL);								// type of depth test
			GL.glHint(GL.GL_PERSPECTIVE_CORRECTION_HINT, GL.GL_NICEST);	// nice perspective calculations
			//rtri = 30.0f;			define the rotation angle in the start position of the triangle
			//rquad = 30.0f;		define the rotation angle in the start position of the quad

            GL.glLightfv(GL.GL_LIGHT1, GL.GL_AMBIENT,  this.LightAmbient);	// Setup The Ambient Light
		//	GL.glLightfv(GL.GL_LIGHT1, GL.GL_DIFFUSE,  this.LightDiffuse);	// Setup The Diffuse Light
		//	GL.glLightfv(GL.GL_LIGHT1, GL.GL_POSITION, this.LightPosition);	// Position The Light
			GL.glEnable(GL.GL_LIGHT1);										// Enable Light One

		//	if (this.light)													// If lighting, enable it to start
          //      GL.glEnable(GL.GL_LIGHTING);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			Size s = Size;

			GL.glMatrixMode(GL.GL_PROJECTION);
			GL.glLoadIdentity();
			GL.gluPerspective(45.0f, (double)s.Width /(double) s.Height, 0.1f, 100.0f);	
			GL.glMatrixMode(GL.GL_MODELVIEW);
			GL.glLoadIdentity();
		}
	}
	
	public class MainForm : System.Windows.Forms.Form	
	{
        private Project.OpenGLForm view;

		public MainForm()
		{
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(640, 480);
			this.Name = "MainForm";
			this.Text = "project";
            this.view = new Project.OpenGLForm();			
			this.view.Parent = this;
			this.view.Dock = DockStyle.Fill; // Will fill whole form
			this.Show();
		}

        static void Main() 
		{
            new Project.OpenGLForm();


            MainForm form = new MainForm();

            while ((!form.view.finished) && (!form.IsDisposed))		// refreshing the window, so it rotates
            {
                form.view.glDraw();
                form.Refresh();
                Application.DoEvents();
            }

            form.Dispose();
		}
	}
}
