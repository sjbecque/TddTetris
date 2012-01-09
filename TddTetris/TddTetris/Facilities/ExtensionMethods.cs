using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public static class ExtensionMethods
    {
        public static int x(this Vector2 vector2)
        {
            return (int)vector2.X;
        }
        public static int y(this Vector2 vector2)
        {
            return (int)vector2.Y;
        }
    }
}
