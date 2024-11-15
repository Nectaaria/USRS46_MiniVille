using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minitamereville
{
    public class Cards
    {
        public CardsInfo info = new CardsInfo();
        public Cards()
        {

        }
    }
    public struct CardsInfo
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int Cost { get; set; }
        public string Name { get; set; }
        public string Effect { get; set; }
        public int Dice { get; set; }
        public int Gain { get; set; }
        public string Type { get; set; }
        public string targetType { get; set; }
    }
    public class Bleue  : Cards
    {
         
    }
    

}
