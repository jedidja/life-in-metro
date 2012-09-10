using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace LifeInMetro
{
    public class CellMapDisplay
    {
        private readonly Canvas canvas;
        private readonly int numberCellsAcross;
        private readonly int numberCellsDown;

        private readonly Dictionary<int, Rectangle> cellIndexDisplayMap;

        private readonly SolidColorBrush blackBrush;
        private readonly SolidColorBrush whiteBrush;

        public CellMapDisplay(Canvas canvas, int numberCellsAcross, int numberCellsDown, int cellSize)
        {
            this.canvas = canvas;
            this.numberCellsAcross = numberCellsAcross;
            this.numberCellsDown = numberCellsDown;

            cellIndexDisplayMap = new Dictionary<int, Rectangle>();
            
            blackBrush = new SolidColorBrush(Colors.Black);
            whiteBrush = new SolidColorBrush(Colors.White);

            for (int y = 0; y < numberCellsDown; y++)
            {
                for (int x = 0; x < numberCellsAcross; x++)
                {
                    var cell = new Rectangle();
                    cell.Width = cellSize;
                    cell.Height = cellSize;

                    cell.Fill = new SolidColorBrush(Colors.Black);

                    Canvas.SetLeft(cell, x * cellSize);
                    Canvas.SetTop(cell, y * cellSize);
                    canvas.Children.Add(cell);

                    cellIndexDisplayMap[y * numberCellsAcross + x] = cell;
                }
            }
        }

        public void DrawCell(int x, int y, bool on)
        {
            var cellRectangle = cellIndexDisplayMap[y * numberCellsAcross + x];

            cellRectangle.Fill = on ? whiteBrush : blackBrush;
        }
    }
}
