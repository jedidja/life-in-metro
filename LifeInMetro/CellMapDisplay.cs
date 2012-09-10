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
        private readonly uint numberCellsAcross;
        private readonly int cellSize;

        private readonly Dictionary<uint, Rectangle> cellIndexDisplayMap;

        private readonly SolidColorBrush blackBrush;
        private readonly SolidColorBrush whiteBrush;

        public CellMapDisplay(Canvas canvas, uint numberCellsAcross, int cellSize)
        {
            this.canvas = canvas;
            this.numberCellsAcross = numberCellsAcross;
            this.cellSize = cellSize;

            cellIndexDisplayMap = new Dictionary<uint, Rectangle>();

            blackBrush = new SolidColorBrush(Colors.Black);
            whiteBrush = new SolidColorBrush(Colors.White);
        }

        public void AddCell(uint x, uint y, bool on)
        {
            var cell = new Rectangle();
            cell.Width = cellSize;
            cell.Height = cellSize;

            cell.Fill = on ? whiteBrush : blackBrush;

            Canvas.SetLeft(cell, x * cellSize);
            Canvas.SetTop(cell, y * cellSize);
            canvas.Children.Add(cell);

            cellIndexDisplayMap[y * numberCellsAcross + x] = cell;
        }

        public void DrawCell(uint x, uint y, bool on)
        {
            var cellRectangle = cellIndexDisplayMap[y * numberCellsAcross + x];

            cellRectangle.Fill = on ? whiteBrush : blackBrush;
        }
    }
}
