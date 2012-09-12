using System;
using Windows.UI.Xaml.Controls;

namespace LifeInMetro
{
    public class CellMap
    {
        private readonly byte[] cells;
        private readonly byte[] tempCells;

        private readonly uint width;
        private readonly uint height;

        private readonly int lengthInBytes;

        private int numberOfAliveCells;

        private readonly CellMapDisplay display;

        public CellMap(uint height, uint width, Image image, int cellSize)
        {
            this.width = width;
            this.height = height;

            lengthInBytes = (int)(width * height);

            cells = new byte[lengthInBytes];
            tempCells = new byte[lengthInBytes];

            for (int c = 0; c < width * height; c++)
            {
                cells[c] = 0;
            }

            display = new CellMapDisplay(image, width, height, cellSize);
            InitDisplay();
        }

        public int PercentageOfAliveCells
        {
            get
            {
                return (int)(numberOfAliveCells * 100 / width / height);
            }
        }

        public void SetCell(uint x, uint y)
        {
            int w = (int)width;
            int h = (int)height;

            int xoleft, xoright, yoabove, yobelow;

            if (x == 0)
            {
                xoleft = w - 1;
            }
            else
            {
                xoleft = -1;
            }

            if (y == 0)
            {
                yoabove = lengthInBytes - w;
            }
            else
            {
                yoabove = -w;
            }

            if (x == (w - 1))
            {
                xoright = -(w - 1);
            }
            else
            {
                xoright = 1;
            }

            if (y == (h - 1))
            {
                yobelow = -(lengthInBytes - w);
            }
            else
            {
                yobelow = w;
            }

            uint cellIndex = width * y + x;

            cells[cellIndex] |= 0x01;
            cells[cellIndex + yoabove + xoleft] += 2;
            cells[cellIndex + yoabove] += 2;
            cells[cellIndex + yoabove + xoright] += 2;
            cells[cellIndex + xoleft] += 2;
            cells[cellIndex + xoright] += 2;
            cells[cellIndex + yobelow + xoleft] += 2;
            cells[cellIndex + yobelow] += 2;
            cells[cellIndex + yobelow + xoright] += 2;

            numberOfAliveCells += 1;
        }

        public void ClearCell(uint x, uint y)
        {
            int w = (int)width;
            int h = (int)height;

            int xoleft, xoright, yoabove, yobelow;

            if (x == 0)
            {
                xoleft = w - 1;
            }
            else
            {
                xoleft = -1;
            }

            if (y == 0)
            {
                yoabove = lengthInBytes - w;
            }
            else
            {
                yoabove = -w;
            }

            if (x == (w - 1))
            {
                xoright = -(w - 1);
            }
            else
            {
                xoright = 1;
            }

            if (y == (h - 1))
            {
                yobelow = -(lengthInBytes - w);
            }
            else
            {
                yobelow = w;
            }

            uint cellIndex = width * y + x;

            cells[cellIndex] &= 0xFE;
            cells[cellIndex + yoabove + xoleft] -= 2;
            cells[cellIndex + yoabove] -= 2;
            cells[cellIndex + yoabove + xoright] -= 2;
            cells[cellIndex + xoleft] -= 2;
            cells[cellIndex + xoright] -= 2;
            cells[cellIndex + yobelow + xoleft] -= 2;
            cells[cellIndex + yobelow] -= 2;
            cells[cellIndex + yobelow + xoright] -= 2;

            numberOfAliveCells -= 1;
        }

        public int GetCellState(int x, int y)
        {
            return cells[y * width + x] & 0x01;
        }

        public void NextGeneration()
        {
            display.UpdateScreen();

            uint x, y;
            int count;
            uint h = height, w = width;
            uint cellIndex;

            Array.Copy(cells, tempCells, lengthInBytes);

            cellIndex = 0;

            for (y = 0; y < h; y++)
            {
                x = 0;
                do
                {
                    //  repeat  for  each  cell  in  row 
                    //  Zip  quickly  through  as  many  off-cells  with  no 
                    //  neighbors  as  possible 
                    while (tempCells[cellIndex] == 0)
                    {
                        cellIndex += 1;

                        if (++x >= w)
                        {
                            goto rowDone;
                        }
                    }

                    // Found  a cell  that's  either  on  or  has  on-neighbors, 
                    // so  see  if  its  state  needs  to be changed
                    count = tempCells[cellIndex] >> 1;

                    if ((tempCells[cellIndex] & 0x01) == 0x01)
                    {
                        // Cell is on; turn it off if it doesn't have 2 or 3 neighbours
                        if (count != 2 && count != 3)
                        {
                            ClearCell(x, y);
                            display.DrawCell(x, y, false);
                        }
                    }
                    else
                    {
                        // Cell is off; turn it on on if it has exactly 3 neighbours
                        if (count == 3)
                        {
                            SetCell(x, y);
                            display.DrawCell(x, y, true);
                        }
                    }

                    // Advance to the next cell
                    cellIndex += 1;
                } while (++x < w);
            rowDone: ;
            }
        }

        private void InitDisplay()
        {
            var r = new Random();

            for (uint y = 0; y < height; y++)
            {
                for (uint x = 0; x < width; x++)
                {
                    bool on = r.Next(100) < 52;
                    display.DrawCell(x, y, true);

                    if (on)
                    {
                        SetCell(x, y);
                    }
                }
            }
        }
    }
}

