using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Threading;

namespace WpfApplication1
{
    class Butterfly
    {
        private Random rand = new Random();
        public Image picBox = new Image();
        private BitmapImage[] images;
        private int last;
        private int now;
        private int endX;
        private int endY;
        private int startX;
        private int startY;

        public Butterfly(int x, int y)
        {
            initImages();
            picBox.Width = images[0].Width / 10;
            picBox.Height = images[0].Height / 10;
            startX = x;
            startY = y;
            endX = startX;
            endY = startY;
            Canvas.SetLeft(picBox, startX);
            Canvas.SetTop(picBox, startY);

        }

        public void nextLocation(MainWindow w)
        {
            w.Dispatcher.BeginInvoke((ThreadStart)delegate
            {


                if (Canvas.GetLeft(picBox) == endX && Canvas.GetTop(picBox) == endY)
                {
                    startX = endX;
                    startY = endY;
                    int x = rand.Next(MainWindow.mainX);
                    if (Math.Abs(x - startX) % 2 == 0)
                    {
                        endX = x;
                    }
                    else endX = x + 1;

                    endY = rand.Next(MainWindow.mainY);

                }
                else
                {
                    if (endX < Canvas.GetLeft(picBox))
                    {
                        Canvas.SetLeft(picBox, Canvas.GetLeft(picBox) - 2);
                    }
                    else
                    {
                        Canvas.SetLeft(picBox, Canvas.GetLeft(picBox) + 2);
                    }

                    int y = (int)((endY - startY) * (Canvas.GetLeft(picBox) - startX) / (endX - startX) + startY);
                    Canvas.SetTop(picBox, y);
                }
            }
            , null);
        }

        public void nextPicture(MainWindow w)
        {
            w.Dispatcher.BeginInvoke((ThreadStart)delegate
            {

                if (now == last)
                {
                    picBox.Source = images[0];
                    now = 0;
                }
                else
                {
                    picBox.Source = images[now + 1];
                    now++;
                }
            }, null);
        }

        private void initImages()
        {
            string[] files;
            files = System.IO.Directory.GetFiles(@"..\..\Images\Butterfly\", "*.png");
            LinkedList<BitmapImage> imgList = new LinkedList<BitmapImage>();

            for (int i = 0; i < files.Length; i++)
            {
                BitmapImage bm = new BitmapImage();
                bm.BeginInit();
                bm.StreamSource = new System.IO.FileStream(files[i], System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                bm.CacheOption = BitmapCacheOption.OnLoad;
                imgList.AddLast(bm);
                bm.EndInit();
            }
            images = imgList.ToArray();
            last = images.Count() - 1;
            picBox.Source = images[0];

        }

    }
}
