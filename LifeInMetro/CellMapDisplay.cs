using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace LifeInMetro
{
    public class CellMapDisplay
    {
        private readonly WriteableBitmap bitmap;
        private readonly uint numberCellsAcross;
        private readonly uint numberCellsDown;
        private readonly int cellSize;

        public byte[] cells;
        private int bytesPerCellLine;

        public CellMapDisplay(Image image, uint numberCellsAcross, uint numberCellsDown, int cellSize)
        {
            bitmap = new WriteableBitmap((int)numberCellsAcross * cellSize, (int)numberCellsDown * cellSize);
            image.Source = bitmap;

            this.numberCellsAcross = numberCellsAcross;
            this.numberCellsDown = numberCellsDown;
            this.cellSize = cellSize;

            bytesPerCellLine = (int)numberCellsAcross * cellSize * cellSize * 4;

            cells = new byte[bytesPerCellLine * numberCellsDown];

            for (int x = 0; x < cells.Length; x += 4)
            {
                cells[x] = 0;
                cells[x + 1] = 0;
                cells[x + 2] = 0;
                cells[x + 3] = 0xff;
            }
        }

        public void DrawCell(uint x, uint y, bool on)
        {
            byte value = (byte)(on ? 0xFF : 0);

            var lineLeft = bytesPerCellLine * y + (x * cellSize * 4);

            for (int celly = 0; celly < cellSize; celly++)
            {
                for (int cellx = 0; cellx < cellSize; cellx++)
                {
                    var pixel = lineLeft + (cellx * 4);

                    cells[pixel] = value;
                    cells[pixel + 1] = value;
                    cells[pixel + 2] = value;
                    cells[pixel + 3] = 0xFF;
                }

                lineLeft += (int)numberCellsAcross * cellSize * 4;
            }
        }

        public async void UpdateScreen()
        {
            using (var stream = bitmap.PixelBuffer.AsStream())
            {
                await stream.WriteAsync(cells, 0, cells.Length);
            }

            bitmap.Invalidate();
        }
    }
}
