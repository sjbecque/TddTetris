using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public interface IBlock
    {
        void RotateLeft();
        void RotateRight();

        Color? ColorAt(int x, int y);
    }
}
