using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniville
{
    public class Dice
    {
        public int nbFaces;
        private Random random = new Random();
        private int lance;

        public Dice(int nbFace)
        {
            this.nbFaces = nbFace;
        }

        public int Lancer(int nbOfDice = 1)
        {
            if (nbOfDice < 1)
                return 1;

            lance = 0;
            for (int i = 0; i < nbOfDice; i++)
            {
                lance += random.Next(1, nbFaces + 1);
            }

            return lance;
        }
    }
}
