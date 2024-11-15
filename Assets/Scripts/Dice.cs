using System;

namespace Minitamereville
{
    public class Dice
    {
        public int FaceCount { get; private set; }

        private Random random;

        public Dice(int faceCount)
        {
            FaceCount = faceCount;
            random = new Random();
        }

        public int Roll(int dieCount)
        {
            if (dieCount <= 0)
                return 0;

            int result = 0;

            for (int i = 0; i < dieCount; i++)
            {
                result += RollSingle();
            }

            return result;
        }

        private int RollSingle()
        {
            return random.Next(FaceCount);
        }
    } 
}
