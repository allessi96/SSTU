using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Threading;

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        private List<Butterfly> butterflies = new  List<Butterfly>();
        private List<Cloud> clouds = new List<Cloud>();
        private List<Flower> flowers = new List<Flower>();
        private List<Sun> suns = new  List<Sun>();

        private const int bButton = 1;
        private const int cButton = 2;
        private const int sButton = 3;
        private const int fButton = 4;

        private int selButton = 0;

        private System.Timers.Timer pictureTimer;
        private System.Timers.Timer butterflyTimer;
        private System.Timers.Timer cloudTimer;
        private System.Timers.Timer cloudPicture;

        public static int mainX;
        public static int mainY;

        public MainWindow()
        {
            InitializeComponent();
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            mainX = (int) main.Width;
            mainY = (int) main.Height;
        }
        
        private void pictureTimer_Tick(object sender, EventArgs e)
        {
            foreach (Butterfly b in butterflies)
            {
                b.nextPicture(this);
            }
            foreach (Flower f in flowers)
            {
               
                f.nextPicture(this);
              
            }
            foreach (Sun s in suns)
            {
                s.nextPicture(this);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            pictureTimer = new System.Timers.Timer();
            pictureTimer.Elapsed += new ElapsedEventHandler(this.pictureTimer_Tick);
            pictureTimer.Interval = 20;

            butterflyTimer = new System.Timers.Timer();
            butterflyTimer.Elapsed += new ElapsedEventHandler(this.butterflyTimer_Tick);
            butterflyTimer.Interval = 20;

            cloudTimer = new System.Timers.Timer();
            cloudTimer.Elapsed += new ElapsedEventHandler(this.cloudTimer_Tick);
            cloudTimer.Interval = 1000;

            cloudPicture = new System.Timers.Timer();
            cloudPicture.Elapsed += new ElapsedEventHandler(this.cloudPicture_Tick);
            cloudPicture.Interval = 100;
        }

        private void butterflyTimer_Tick(object sender, EventArgs e)
        {
            foreach (Butterfly b in butterflies)
            {
                b.nextLocation(this);
            }
        }

        private void cloudPicture_Tick(object sender, EventArgs e)
        {
            foreach (Cloud c in clouds)
            {
                c.nextPicture(this);
            }
        }

        private void cloudTimer_Tick(object sender, EventArgs e)
        {Dispatcher.BeginInvoke((ThreadStart)delegate
            {
            foreach (Cloud c in clouds)
            {
                
                if (Canvas.GetLeft(c.picBox) == c.endX && Canvas.GetTop(c.picBox) == c.endY)
                {
                    clouds.Remove(c);
                }
                else
                {
                    c.nextLocation(this);
                }
            }

            },null);
        }

        private void bButton_Click(object sender, RoutedEventArgs e)
        {
            selButton = bButton;
        }

        private void cButton_Click(object sender, RoutedEventArgs e)
        {
            selButton = cButton;
        }

        private void sButton_Click(object sender, RoutedEventArgs e)
        {

            selButton = sButton;

        }

        private void fButton_Click(object sender, RoutedEventArgs e)
        {
            selButton = fButton;
        }

        private void main_Mouse(object sender, MouseButtonEventArgs e)
        {
            int x = (int)e.GetPosition(main).X;
            int y = (int)e.GetPosition(main).Y;
            switch (selButton)
            {
                case 1:
                    if (pictureTimer.Enabled == false) 
                        pictureTimer.Start();

                    if (butterflyTimer.Enabled == false)
                        butterflyTimer.Start();

                    butterflies.Add(new Butterfly(x,y));
                    main.Children.Add(butterflies[butterflies.Count - 1].picBox);
                    break;

                case 2: 
                    if (cloudTimer.Enabled == false)
                        cloudTimer.Start();

                    if (cloudPicture.Enabled == false)
                        cloudPicture.Start(); 

                    clouds.Add(new Cloud(x,y));
                    main.Children.Add(clouds[clouds.Count - 1].picBox);
                    break;

                case 3: 
                    if (pictureTimer.Enabled == false) 
                        pictureTimer.Start();

                    suns.Add(new Sun(x,y));
                    main.Children.Add(suns[suns.Count - 1].picBox);
                    break;

                case 4: 
                    if (pictureTimer.Enabled == false) 
                        pictureTimer.Start();
                    
                    flowers.Add(new Flower(x,y));
                    main.Children.Add(flowers[flowers.Count - 1].picBox);
                    break;


                default: break;
            }
            selButton = 0;
        }

    }
}
