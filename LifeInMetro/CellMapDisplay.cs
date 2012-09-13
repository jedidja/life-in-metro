using System;
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

        private byte[] cells;
        private int bytesPerCellLine;

        private readonly DeterminesCellNeighbours determinesCellNeighbours;
        private readonly ColorPack colorPack;
        private byte[] cellColors;

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

            cellColors = new byte[numberCellsAcross * numberCellsDown];

            for (int c = 0; c < cellColors.Length; c++)
            {
                cellColors[c] = 0;
            }

            colorPack = new ColorPack(0x0000FFFF, 0x00FFFFFF);
            determinesCellNeighbours = new DeterminesCellNeighbours((int)numberCellsAcross, (int)numberCellsDown);
        }

        public void ClearCell(uint x, uint y)
        {
            DrawCell(x, y, 0, 0, 0);
        }

        public void SpawnCell(uint x, uint y)
        {
            int r = 0, g = 0, b = 0;

            foreach (var neighbour in determinesCellNeighbours.GetNeighbourIndexes(x, y))
            {
                // Only worried about the parents (i.e. three cells that are on)
                if (cellColors[neighbour] == 1)
                {
                    r += colorPack.Color1R;
                    g += colorPack.Color1G;
                    b += colorPack.Color1B;
                }
                else if (cellColors[neighbour] == 2)
                {
                    r += colorPack.Color2R;
                    g += colorPack.Color2G;
                    b += colorPack.Color2B;
                }
            }

            DrawCell(x, y, (byte)(r / 3), (byte)(g / 3), (byte)(b / 3));
        }

        public void InitCell(uint x, uint y, bool useFirstColor)
        {
            byte r = 0, g = 0, b = 0;

            if (useFirstColor)
            {
                r = colorPack.Color1R;
                g = colorPack.Color1G;
                b = colorPack.Color1B;

                cellColors[y * numberCellsAcross + x] = 1;
            }
            else
            {
                r = colorPack.Color2R;
                g = colorPack.Color2G;
                b = colorPack.Color2B;

                cellColors[y * numberCellsAcross + x] = 2;
            }

            DrawCell(x, y, r, g, b);
        }

        public async void UpdateScreen()
        {
            using (var stream = bitmap.PixelBuffer.AsStream())
            {
                await stream.WriteAsync(cells, 0, cells.Length);
            }

            bitmap.Invalidate();
        }

        private void DrawCell(uint x, uint y, byte r, byte g, byte b)
        {
            var lineLeft = bytesPerCellLine * y + (x * cellSize * 4);

            for (int celly = 0; celly < cellSize; celly++)
            {
                for (int cellx = 0; cellx < cellSize; cellx++)
                {
                    var pixel = lineLeft + (cellx * 4);

                    cells[pixel] = b;
                    cells[pixel + 1] = g;
                    cells[pixel + 2] = r;
                    cells[pixel + 3] = 0xFF;
                }

                lineLeft += (int)numberCellsAcross * cellSize * 4;
            }
        }
    }
}
