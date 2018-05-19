using System;
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
        private const int CELL_SIZE = 50;
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


        private Graphics graphics;
        private PictureBox picBox;

        

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

        private void FigureNameMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var item in this.Controls)
            {
                if (item is PictureBox)
                {
                    if ((item as PictureBox).ContextMenuStrip.Equals(
                        (sender as ToolStripMenuItem).Owner))
                    {
                        MessageBox.Show((item as PictureBox).AccessibleName);
                    }
                }
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            this.graphics = e.Graphics;

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
            
            graphics.Dispose();
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
            if (this.IsUserField(i) == true)
            {
                
                if (this.IsFieldOfMainUserFigures(i) == true)
                {
                    this.CheckAndPutTheMainUserFigures(y, rect);
                }
                else if (this.IsUserPawnsField(i) == true)
                {
                    this.PlaceAFigure(rect, Resources.pawnW , "Пешка");
                }
            }
            else if (this.IsCompField(i) == true)
            {
                if (this.IsFielOfMainCompFigures(i) == true)
                {
                    this.CheckAndPutTheMainCompFigures(y, rect);
                }
                else if (this.IsCompPawnsField(i) == true)
                {
                    this.PlaceAFigure(rect, Resources.pawnB, "Пешка");
                }
            }
        }

        private void CheckAndPutTheMainCompFigures(int y, Rectangle rect)
        {
            if (y == 0 || y == 7)
            {
                this.PlaceAFigure(rect, Resources.rookB, "Ладья");
            }
            else if (y == 1 || y == 6)
            {
                this.PlaceAFigure(rect, Resources.knightB, "Конь");
            }
            else if (y == 2 || y == 5)
            {
                this.PlaceAFigure(rect, Resources.bishopB, "Слон");
            }
            else if (y == 3)
            {
                this.PlaceAFigure(rect, Resources.queenB, "Ферзь");
            }
            else if (y == 4)
            {
                this.PlaceAFigure(rect, Resources.kingB, "Король");
            }
        }

        private void CheckAndPutTheMainUserFigures(int y, Rectangle rect)
        {
            if (y == 0 || y == 7)
            {
                this.PlaceAFigure(rect, Resources.rookW, "Ладья");
            }
            else if (y == 1 || y  == 6)
            {
                this.PlaceAFigure(rect, Resources.knightW, "Конь");
            }
            else if (y == 2 || y == 5)
            {
                this.PlaceAFigure(rect, Resources.bishopW, "Слон");
            }
            else if (y == 3)
            {
                this.PlaceAFigure(rect, Resources.queenW, "Ферзь");
            }
            else if (y == 4)
            {
                this.PlaceAFigure(rect, Resources.kingW, "Король");
            }
        }

        private bool IsFielOfMainCompFigures(int i)
        {
            if (i == 0)
            {
                return true;
            }
            return false;
        }

        private bool IsFieldOfMainUserFigures(int i)
        {
            if (i == 7)
            {
                return true;
            }
            return false;
        }

        private bool IsCompPawnsField(int i)
        {
            if (i == 1)
            {
                return true;
            }
            return false;
        }

        private void PlaceAFigure(Rectangle rect, Bitmap figure, string nameFigure)
        {
            picBox = new PictureBox();
            picBox.ClientSize = new Size(CELL_SIZE, CELL_SIZE);
            picBox.Image = figure;
            picBox.SizeMode = PictureBoxSizeMode.Zoom;
            picBox.BackColor = Color.Transparent;

            picBox.Location = new Point(rect.X, rect.Y);
            picBox.AccessibleName = nameFigure;


            ContextMenuStrip contextMenuStripForCell = new ContextMenuStrip();
            ToolStripMenuItem figureNameMenuItem = new ToolStripMenuItem("Название фигуры");
            figureNameMenuItem.Click += FigureNameMenuItem_Click;
            contextMenuStripForCell.Items.Add(figureNameMenuItem);
            picBox.ContextMenuStrip = contextMenuStripForCell;

            this.Controls.Add(picBox);
        }

        private bool IsUserPawnsField(int i)
        {
            if (i == 6)
            {
                return true;
            }
            return false;
        }

        private bool IsCompField(int i)
        {
            if (i == 0 || i == 1)
            {
                return true;
            }
            return false;
        }

        private bool IsUserField(int i)
        {
            if (i == 6 || i == 7)
            {
                return true;
            }
            return false;
        }
    }
}
