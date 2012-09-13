
namespace LifeInMetro
{
    internal class ColorPack
    {
        public ColorPack(int color1, int color2)
        {
            Color1 = color1;
            Color1R = (byte)(color1 >> 8 & 0xFF);
            Color1G = (byte)(color1 >> 16 & 0xFF);
            Color1B = (byte)(color1 >> 24 & 0xFF);

            Color2 = color2;
            Color2R = (byte)(color2 >> 8 & 0xFF);
            Color2G = (byte)(color2 >> 16 & 0xFF);
            Color2B = (byte)(color2 >> 24 & 0xFF);
        }

        public int Color1 { get; private set; }

        public byte Color1R { get; private set; }

        public byte Color1G { get; private set; }

        public byte Color1B { get; private set; }

        public int Color2 { get; private set; }

        public byte Color2R { get; private set; }

        public byte Color2G { get; private set; }

        public byte Color2B { get; private set; }
    }
}
