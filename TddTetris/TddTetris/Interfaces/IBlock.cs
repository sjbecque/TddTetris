using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public interface IBlock
    {
        List<Vector2> vectors { get; }
        Square squareType { get; }
        void RotateLeft();
        void RotateRight();

        void RotateClockwise();
        void RotateCounterClockwise();

        Color? ColorAt(Vector2 position);
    }
}
