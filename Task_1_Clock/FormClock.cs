using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_1_Clock
{
    public partial class FormClock : Form
    {
        private const int SECOND_PER_MINUTE = 60;

        private Timer timer;
        private Point centerPoint;
        private Graphics graphics;
        private Bitmap bmp;

        public FormClock()
        {
            InitializeComponent();
        }

        private void FormClock_Load(object sender, EventArgs e)
        {
            //this.ChangeTheShapeToRound();

            this.timer = new Timer();
            this.timer.Interval = 1000;
            this.timer.Tick += Timer_Tick;
            this.timer.Start();

            this.centerPoint = new Point(ClientSize.Width / 2, ClientSize.Height / 2);

            this.bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //this.graphics = Graphics;
            this.graphics = Graphics.FromImage(this.bmp);
            graphics.Clear(Color.LightSalmon);

            int ss = DateTime.Now.Second;
            int mm = DateTime.Now.Minute;
            int hh = DateTime.Now.Hour;

            // Секундная стрелка
            this.graphics.DrawLine(
                new Pen(Brushes.Blue, 2),
                this.centerPoint,
                this.ComputeArrow(this.centerPoint, ss, 80));

            // Минутная стрелка
            this.graphics.DrawLine(
                new Pen(Brushes.Green, 3),
                this.centerPoint,
                this.ComputeArrow(this.centerPoint, mm, 70));

            // Часовая стрелка
            this.graphics.DrawLine(
                new Pen(Brushes.Yellow, 4),
                this.centerPoint,
                this.ComputeArrow(this.centerPoint, ((hh - 1 / 2) * 5) + (mm / 12), 60));



            for (int i = 0; i < SECOND_PER_MINUTE; i++)
            {
                //this.graphics.DrawLine(
                //    new Pen(Brushes.Black, 2),
                //    this.ComputeArrow(this.centerPoint, i, 80),
                //    this.ComputeArrow(this.centerPoint, i, 83));
                this.graphics.DrawEllipse(new Pen(Brushes.Red, 2), new Rectangle(
                    ComputeArrow(centerPoint, i, 80), new Size(4, 4)
                    ));

                this.graphics.DrawEllipse(new Pen(Brushes.Black, 2), new Rectangle(
                    ComputeArrow(centerPoint, i * 5, 80), new Size(6, 6)
                    ));

                //this.graphics.FillEllipse(
                //    Brushes.Black,
                //    new Rectangle(ComputeArrow(centerPoint, i * 5, 83), new Size(10, 10))
                //    );
            }

            // test
            this.Text = $"{hh}:{mm}:{ss}";

            this.pictureBox.Image = this.bmp;

            this.graphics.Dispose();
        }

        private Point ComputeArrow(Point center, int seconds, int R)
        {
            int x = (int)(center.X + R * Math.Cos(
                    -Math.PI / 2 + Math.PI * seconds / 30));
            int y = (int)(center.Y + R * Math.Sin(
                -Math.PI / 2 + Math.PI * seconds / 30));

            return new Point(x, y);
        }

        private void ChangeTheShapeToRound()
        {
            this.FormBorderStyle = FormBorderStyle.None;

            GraphicsPath formPath = new GraphicsPath();
            formPath.AddEllipse(0, 0, this.Width, this.Height);
            Region = new Region(formPath);
        }
    }
}
