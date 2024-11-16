using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minitamereville
{
    public class De
    {
        public string nbFaces;
        private Random random = new Random();
        private int lance;
        public int Face { get; private set; }

        public De(string nbFace)
        {
            this.nbFaces = nbFace;
        }

        public int Lancer()
        {
            lance = random.Next(int.Parse(nbFaces));
            return lance;
        }
        public override string ToString()
        {
            return lance.ToString();
        }

    }
}
