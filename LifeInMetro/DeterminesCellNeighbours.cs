namespace LifeInMetro
{
    public class DeterminesCellNeighbours
    {
        private readonly int width;
        private readonly int height;
        private readonly int lengthInBytes;
        
        public DeterminesCellNeighbours(int width, int height)
        {
            this.width = width;
            this.height = height;
            
            lengthInBytes = width * height;
        }

        public int[] GetNeighbourIndexes(uint x, uint y)
        {
            var result = new int[8];

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

            int cellIndex = width * (int)y + (int)x;

            result[0] = cellIndex + yoabove + xoleft;
            result[1] = cellIndex + yoabove;
            result[2] = cellIndex + yoabove + xoright;
            result[3] = cellIndex + xoleft;
            result[4] = cellIndex + xoright;
            result[5] = cellIndex + yobelow + xoleft;
            result[6] = cellIndex + yobelow;
            result[7] = cellIndex + yobelow + xoright;

            return result;
        }
    }
}
