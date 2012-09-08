using System;

namespace LifeInMetro
{
    public class CellMap
    {
        private byte[] cells;
        private int width;
        private int height;

        public CellMap(int height, int width)
        {
            this.width = width;
            this.height = height;

            cells = new byte[width * height];
        }

        public void CopyCells(CellMap sourcemap)
        {
            Array.Copy(sourcemap.cells, cells, sourcemap.cells.Length);
        }

        public void SetCell(int x, int y)
        {
            cells[width * x + y] = 1;
        }

        public void ClearCell(int x, int y)
        {
            cells[width * x + y] = 0;
        }

        public int GetCellState(int x, int y)
        {
            while (x < 0) x += width;
            while (x >= width) x -= width;
            while (y < 0) y += height;
            while (y >= height) y -= height;

            return cells[width * x + y];
        }

        public void NextGeneration(CellMap destination, CellMapDisplay display)
        {
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < height; x++)
                {
                    // Figure out how many neighbours this cell has
                    var neighbourCount = GetCellState(x - 1, y - 1) + GetCellState(x, y - 1) +
                                         GetCellState(x + 1, y - 1) + GetCellState(x - 1, y) +
                                         GetCellState(x + 1, y) + GetCellState(x - 1, y + 1) +
                                         GetCellState(x, y + 1) + GetCellState(x + 1, y + 1);

                    if (GetCellState(x, y) == 1)
                    {
                        // The cell is on; does it stay on?
                        if (neighbourCount != 2 && neighbourCount != 3)
                        {
                            destination.ClearCell(x, y);
                            display.DrawCell(x, y, false);
                        }
                    }
                    else
                    {
                        // The cell is off; does it turn on?
                        if (neighbourCount == 3)
                        {
                            destination.SetCell(x, y);
                            display.DrawCell(x, y, true);
                        }
                    }
                }
            }
        }
    }
}

