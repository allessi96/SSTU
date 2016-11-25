using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Collections;

namespace Butter
{
    class Butterfly
    {
        private Random rand = new Random();
        public PictureBox picBox = new PictureBox();
        private LinkedList<Bitmap> images = new LinkedList<Bitmap>();
        private Point locationB;
        private int startX;
        private int startY;
        private Timer butterflyTimer;

        public Butterfly()
        {
            picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            picBox.Size = new Size(50,50);
            picBox.Visible = true;
            int x = rand.Next(0, 1);
            if (x == 1) x = Screen.PrimaryScreen.Bounds.Width;
            picBox.Location = new Point(x, rand.Next(Screen.PrimaryScreen.Bounds.Height));
            locationB = picBox.Location;
            startX = picBox.Location.X;
            startY = picBox.Location.Y;
            initImages();

        }

        public void start()
        {
            butterflyTimer = new Timer();
            butterflyTimer.Tick += new EventHandler(this.butterflyTimer_Tick);
            butterflyTimer.Interval = 20;
            butterflyTimer.Start();
        }


        private void butterflyTimer_Tick(object sender, EventArgs e)
        {
            nextPicture();
            nextLocation();
        }

        public void nextLocation()
        {
            if (picBox.Location == locationB)
            {
                startX = locationB.X;
                startY = locationB.Y;
                int endX = rand.Next(Screen.PrimaryScreen.Bounds.Width);
                if (Math.Abs(endX-startX)%2==0)
                locationB = new Point(endX, rand.Next(200, Screen.PrimaryScreen.Bounds.Height-50));
                else locationB = new Point(endX+1, rand.Next(200, Screen.PrimaryScreen.Bounds.Height-50));
                

            }
            else
            {
                if (locationB.X < picBox.Location.X)
                {
                    picBox.Location = new Point(picBox.Location.X - 2, picBox.Location.Y);
                }
                else 
                { 
                    picBox.Location = new Point(picBox.Location.X + 2, picBox.Location.Y); 
                }

                int y = ((locationB.Y - startY) * (picBox.Location.X - startX) / (locationB.X - startX) + startY);
                picBox.Location = new Point(picBox.Location.X, y);
            }
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
            files = System.IO.Directory.GetFiles(@"..\..\Images\Butterfly\", "*.png");


            for (int i = 0; i < files.Length; i++)
            {
                images.AddLast(new Bitmap(System.IO.Path.GetFullPath(@"..\..\Images\Butterfly\") + files[i]));
            }
            picBox.Image = images.First.Value;

        }
    }
}
