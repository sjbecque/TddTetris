using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class Square //Extended enum emulation; handiger om enum met attributes te gebruiken?;
    {
        public static readonly Square Empty = new Square(Color.Black, 0);
        public static readonly Square Peg = new Square(Color.LimeGreen, 10);
        public static readonly Square Triangle = new Square(Color.Red, 20);
        public static readonly Square Bar = new Square(Color.Blue, 30);

        public Color color;
        public int points; //etc..

        private Square(Color color, int points)
        {
            this.color = color;
            this.points = points;
        }
    }
}
