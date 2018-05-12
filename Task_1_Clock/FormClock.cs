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

        private const int RADIUS_OF_THE_CLOCK = 83;
        private const int DIAMETER_OF_HOUR_MARK = 10;
        private const int DIAMETER_OF_MINUTE_MARK = 4;

        private const int ARROW_LENGTH_HOUR = 60;
        private const int ARROW_LENGTH_MINUTE = 70;
        private const int ARROW_LENGTH_SECONDS = 80;

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
            this.graphics = Graphics.FromImage(this.bmp);
            graphics.Clear(Color.LightSalmon);

            int ss = DateTime.Now.Second;
            int mm = DateTime.Now.Minute;
            int hh = DateTime.Now.Hour;

            // Секундная стрелка
            this.graphics.DrawLine(
                new Pen(Brushes.Blue, 2),
                this.centerPoint,
                this.ComputeArrow(this.centerPoint, ss, ARROW_LENGTH_SECONDS));

            // Минутная стрелка
            this.graphics.DrawLine(
                new Pen(Brushes.Green, 3),
                this.centerPoint,
                this.ComputeArrow(this.centerPoint, mm, ARROW_LENGTH_MINUTE));

            // Часовая стрелка
            this.graphics.DrawLine(
                new Pen(Brushes.Yellow, 4),
                this.centerPoint,
                this.ComputeArrow(
                    this.centerPoint,
                    ((hh - 1 / 2) * 5) + (mm / 12), ARROW_LENGTH_HOUR));



            for (int i = 0; i < SECOND_PER_MINUTE; i++)
            {
                // Ставим метки минут/секунд:
                Point pMmSs = ComputeArrow(centerPoint, i, RADIUS_OF_THE_CLOCK);
                // Центрируем метки.
                pMmSs = new Point(
                    pMmSs.X - (DIAMETER_OF_MINUTE_MARK / 2),
                    pMmSs.Y - (DIAMETER_OF_MINUTE_MARK / 2));
                // Ставим метку.
                this.graphics.DrawEllipse(
                    new Pen(Brushes.Red, 2),
                    new Rectangle(
                        pMmSs,
                        new Size(DIAMETER_OF_MINUTE_MARK, DIAMETER_OF_MINUTE_MARK)
                    ));

                // Ставим метки часов:
                Point pHour = ComputeArrow(centerPoint, i * 5, RADIUS_OF_THE_CLOCK);
                // умножаем на 5 - чтоб часовая стрелка показывала правильно.
                // Центрируем метки.
                pHour = new Point(
                    pHour.X - (DIAMETER_OF_HOUR_MARK / 2),
                    pHour.Y - (DIAMETER_OF_HOUR_MARK / 2));
                // Ставим метку.
                this.graphics.FillEllipse(
                    Brushes.Black,
                    new Rectangle(
                        pHour,
                        new Size(DIAMETER_OF_HOUR_MARK, DIAMETER_OF_HOUR_MARK))
                    );
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
