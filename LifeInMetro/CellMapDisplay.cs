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

        public CellMapDisplay(Canvas canvas, int numberCellsAcross, int numberCellsDown, int cellSize)
        {
            this.canvas = canvas;
            this.numberCellsAcross = numberCellsAcross;
            this.numberCellsDown = numberCellsDown;

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
                }
            }
        }

        public void DrawCell(int x, int y, bool on)
        {
            var cellRectangle = canvas.Children[y * numberCellsAcross + x] as Rectangle;

            cellRectangle.Fill = on ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black);
        }
    }
}
