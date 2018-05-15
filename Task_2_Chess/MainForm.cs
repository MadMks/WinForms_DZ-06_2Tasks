﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task_2_Chess.Properties;

namespace Task_2_Chess
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Размер одной клетки/ячейки.
        /// </summary>
        private const int CELL_SIZE = 25;
        /// <summary>
        /// Кол-во клеток в строке/столбце.
        /// </summary>
        private const int NUMBER_OF_CELLS_IN_ROW = 8;

        /// <summary>
        /// Цвет светлой клетки.
        /// </summary>
        private readonly Brush lightCell;
        /// <summary>
        /// Цвет темной клетки.
        /// </summary>
        private readonly Brush darkCell;

        /// <summary>
        /// Цвет клетки (закрашиваемой).
        /// </summary>
        private Brush brushCell;

        PictureBox picBox;  // TODO private

        public MainForm()
        {
            InitializeComponent();

            // Здесь выставляем цвета для клеток.
            this.lightCell = Brushes.SandyBrown;
            this.darkCell = Brushes.Chocolate;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.brushCell = this.lightCell;
            

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

                    this.ArrangementOfFigures(i, y, rectCell);

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
            if (brushCell == this.lightCell)
            {
                brushCell = this.darkCell;
            }
            else
            {
                brushCell = this.lightCell;
            }
        }

        private void ArrangementOfFigures(int i, int y, Rectangle rect)
        {
            if (this.UserPlayer(i) == true)
            {
                //picBox = new PictureBox();
                //picBox.ClientSize = new Size(CELL_SIZE, CELL_SIZE);
                //picBox.Image = Resources.bishopB;
                //picBox.SizeMode = PictureBoxSizeMode.Zoom;
                //picBox.BackColor = Color.Transparent;

                //picBox.Location = new Point(rect.X, rect.Y);
                if (this.UserPawns(i) == true)
                {
                    this.PlaceAFigure(rect, Resources.pawnW);
                }
            }
            else if (this.CompPlayer(i) == true)
            {
                if (this.CompPawns(i) == true)
                {
                    this.PlaceAFigure(rect, Resources.pawnB);
                }
            }

            this.Controls.Add(picBox);
        }

        private bool CompPawns(int i)
        {
            if (i == 1)
            {
                return true;
            }
            return false;
        }

        private void PlaceAFigure(Rectangle rect, Bitmap figure)
        {
            picBox = new PictureBox();
            picBox.ClientSize = new Size(CELL_SIZE, CELL_SIZE);
            picBox.Image = figure;
            picBox.SizeMode = PictureBoxSizeMode.Zoom;
            picBox.BackColor = Color.Transparent;

            picBox.Location = new Point(rect.X, rect.Y);
        }

        private bool UserPawns(int i)
        {
            if (i == 6)
            {
                return true;
            }
            return false;
        }

        private bool CompPlayer(int i)
        {
            if (i == 0 || i == 1)
            {
                return true;
            }
            return false;
        }

        private bool UserPlayer(int i)
        {
            if (i == 6 || i == 7)
            {
                return true;
            }
            return false;
        }
    }
}
