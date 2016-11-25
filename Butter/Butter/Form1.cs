using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Butter
{
    public partial class Form1 : Form
    {

        private Butterfly butterfly = new Butterfly();
        private List<Cloud> clouds = new List<Cloud>();
        private List<Flower> flowers = new List<Flower>();
        private Sun sun = new Sun();
        private System.Windows.Forms.Timer addCloudTimer;
        private System.Windows.Forms.Timer invalidateTimer;

        public Form1()
        {
            new Thread(sun.start).Start();
            this.Controls.Add(sun.picBox);

            addFlowers();

            new Thread(butterfly.start).Start();
            this.Controls.Add(butterfly.picBox);

            clouds.Add(new Cloud());
            new Thread(clouds[clouds.Count - 1].startThread).Start();
            this.Controls.Add(clouds[clouds.Count - 1].picBox);
            InitializeComponent();

        }

        private void addFlowers()
        {
            flowers.Add(new Flower(0));
            Thread flowersThread = new Thread(flowers[0].start);
            this.Controls.Add(flowers[flowers.Count - 1].picBox);

            int countFlowers = Screen.PrimaryScreen.Bounds.Width / flowers[flowers.Count - 1].picBox.Size.Width;

            for (int i = 0; i < countFlowers; i++)
            {
                flowers.Add(new Flower(flowers[flowers.Count - 1].picBox.Location.X + flowers[flowers.Count - 1].picBox.Size.Width));
                new Thread(flowers[flowers.Count - 1].start).Start();
                this.Controls.Add(flowers[flowers.Count - 1].picBox);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            addCloudTimer = new System.Windows.Forms.Timer();
            addCloudTimer.Tick += new EventHandler(this.addCloudTimer_Tick);
            addCloudTimer.Interval = 5000;
            addCloudTimer.Start();

            invalidateTimer = new System.Windows.Forms.Timer();
            invalidateTimer.Tick += new EventHandler(this.invalidateTimer_Tick);
            invalidateTimer.Interval = 20;
            invalidateTimer.Start();
        }

        private void addCloudTimer_Tick(object sender, EventArgs e)
        {
            clouds.Add(new Cloud());
            new Thread(clouds[clouds.Count - 1].startThread).Start();
            this.Controls.Add(clouds[clouds.Count - 1].picBox);
        }

        private void invalidateTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        

    }
}
