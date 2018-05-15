using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_2_Chess
{
    public partial class MainForm : Form
    {
        private const int CELL_SIZE = 25;
        private const int NUMBER_OF_CELLS_IN_ROW = 8;

        Brush brushCell = Brushes.White;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Width
                = (CELL_SIZE * NUMBER_OF_CELLS_IN_ROW)
                + (this.Width - this.ClientSize.Width);
            this.Height
                = (CELL_SIZE * NUMBER_OF_CELLS_IN_ROW)
                + (this.Height - this.ClientSize.Height);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            Point point = new Point(0, 0);
            Size size = new Size(CELL_SIZE, CELL_SIZE);
            Rectangle rectCell = new Rectangle(point, size);

            for (int i = 0; i < NUMBER_OF_CELLS_IN_ROW; i++)
            {
                for (int y = 0; y < NUMBER_OF_CELLS_IN_ROW; y++)
                {
                    graphics.FillRectangle(this.brushCell, rectCell);

                    //this.ArrangementOfFigures(i, y);

                    this.ChangeColorBrush();
                    rectCell.X += CELL_SIZE;
                }

                this.ChangeColorBrush();
                rectCell.X = 0;
                rectCell.Y += CELL_SIZE;
            }
        }

        private void ChangeColorBrush()
        {
            if (brushCell == Brushes.White)
            {
                brushCell = Brushes.Black;
            }
            else
            {
                brushCell = Brushes.White;
            }
        }

        private void ArrangementOfFigures(int i, int y)
        {
            //throw new NotImplementedException();
        }
    }
}
