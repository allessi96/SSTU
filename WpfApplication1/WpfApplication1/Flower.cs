﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Threading;

namespace WpfApplication1
{
    class Flower
    {
        public Image picBox = new Image();
        private BitmapImage[] images;
        private int last;
        private int now;

        public Flower(int x, int y)
        {
            initImages();
            picBox.Width = images[0].Width / 2;
            picBox.Height = images[0].Height / 2;
            Canvas.SetLeft(picBox, x);
            Canvas.SetTop(picBox, y);
        }

        public void nextPicture(MainWindow w)
        {
            w.Dispatcher.BeginInvoke((ThreadStart)delegate
            {

                if (now==last)
                {
                    picBox.Source = images[0];
                    now = 0;
                }
                else
                {
                    picBox.Source = images[now+1];
                    now++;
                }
            }, null);
        }

        private void initImages()
        {
            string[] files;
            files = System.IO.Directory.GetFiles(@"..\..\Images\Flowers\", "*.png");
            LinkedList<BitmapImage> imgList = new LinkedList<BitmapImage>();

            for (int i = 0; i < files.Length; i++)
            {
                BitmapImage bm=new BitmapImage();
                bm.BeginInit();
                bm.StreamSource = new System.IO.FileStream(files[i], System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                bm.CacheOption=BitmapCacheOption.OnLoad;
                imgList.AddLast(bm);
                bm.EndInit();
            }
            images = imgList.ToArray();
            last = images.Count()-1;
            picBox.Source = images[0];

        }

    }
}
