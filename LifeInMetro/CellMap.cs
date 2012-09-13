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
        private readonly DeterminesCellNeighbours determinesCellNeighbours;


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

            determinesCellNeighbours = new DeterminesCellNeighbours((int)width, (int)height);

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
            cells[width * y + x] |= 0x01;

            foreach (var neighbour in determinesCellNeighbours.GetNeighbourIndexes(x, y))
            {
                cells[neighbour] += 2;
            }

            numberOfAliveCells += 1;
        }

        public void ClearCell(uint x, uint y)
        {
            cells[width * y + x] &= 0xFE;

            foreach (var neighbour in determinesCellNeighbours.GetNeighbourIndexes(x, y))
            {
                cells[neighbour] -= 2;
            }

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
                            display.ClearCell(x, y);
                        }
                    }
                    else
                    {
                        // Cell is off; turn it on on if it has exactly 3 neighbours
                        if (count == 3)
                        {
                            SetCell(x, y);
                            display.SpawnCell(x, y);
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

                    if (on)
                    {
                        display.InitCell(x, y, r.Next(100) < 51);
                        SetCell(x, y);
                    }
                    else
                    {
                        display.ClearCell(x, y);
                    }
                }
            }
        }
    }
}

