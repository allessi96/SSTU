using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Butter
{
    public class Cloud
    {
        Random rand = new Random();
        public PictureBox picBox = new PictureBox();
        private LinkedList<Bitmap> images=new LinkedList<Bitmap>();
        public Point locationB;
        private System.Windows.Forms.Timer cloudTimer;
        private System.Windows.Forms.Timer pictureTimer;

        public Cloud()
        {
            picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            picBox.Visible = true;
            picBox.Location = new Point(500, 0);
            locationB = new Point(-200, 0);
            initImages();
            picBox.Size = new Size(picBox.Image.Size.Width/2, picBox.Image.Size.Height/2);
        }
        
        public void startThread()
        {
            cloudTimer = new System.Windows.Forms.Timer();
            cloudTimer.Tick += new EventHandler(this.cloudTimer_Tick);
            cloudTimer.Interval = 20;
            cloudTimer.Start();

            pictureTimer = new System.Windows.Forms.Timer();
            pictureTimer.Tick += new EventHandler(this.pictureTimer_Tick);
            pictureTimer.Interval = 20;
            pictureTimer.Start();

        }
    
        private void cloudTimer_Tick(object sender, EventArgs e)
        {
                bool cloud = nextLocation();
                if (cloud == false)
                {
                    Thread.CurrentThread.Abort();
                }

        }

        private void pictureTimer_Tick(object sender, EventArgs e)
        {
            if (picBox.Image.Equals(images.Last.Value))
            {
                picBox.Image = images.First.Value;
            }
            else
            {
                picBox.Image = images.Find((Bitmap)picBox.Image).Next.Value;
            }
        }
        
        public Boolean nextLocation()
        {
            if (picBox.Location == locationB)
            {
                return false;
            }
            else
            {
                picBox.Location = new Point(picBox.Location.X - 2, 0);
                return true;
            }
        }

        private void initImages()
        {
            string[] files;
            files=System.IO.Directory.GetFiles(@"..\..\Images\Cloud\", "*.png");


            for (int i = 0; i < files.Length; i++) 
            {
                images.AddLast(new Bitmap(System.IO.Path.GetFullPath(@"..\..\Images\Cloud\") + files[i]));
            }
            picBox.Image = images.First.Value;

        }
    
    }
}
