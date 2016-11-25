using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Butter
{
    class Sun
    {

        Random rand = new Random();
        public PictureBox picBox = new PictureBox();
        private LinkedList<Bitmap> images=new LinkedList<Bitmap>();
        private Timer pictureTimer;

        public Sun()
        {
            picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            picBox.Visible = true;
            initImages();
            picBox.Location = new Point(0,0);
            picBox.Size = new Size(picBox.Image.Size.Width/2, picBox.Image.Size.Height/2);
        }

        public void start()
        {
            pictureTimer = new Timer();
            pictureTimer.Tick += new EventHandler(this.pictureTimer_Tick);
            pictureTimer.Interval = 20;
            pictureTimer.Start();
        }

        private void pictureTimer_Tick(object sender, EventArgs e)
        {
            nextPicture();
        }

        public void nextPicture()
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

        private void initImages()
        {
            string[] files;
            files=System.IO.Directory.GetFiles(@"..\..\Images\Sun\", "*.png");


            for (int i = 0; i < files.Length; i++) 
            {
                images.AddLast(new Bitmap(System.IO.Path.GetFullPath(@"..\..\Images\Sun\") + files[i]));
            }
            picBox.Image = images.First.Value;

        }
    

    }
}
