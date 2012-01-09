using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public interface IBlock
    {
        List<Vector2> vectors { get; set; } //ToSelf: was private!
        Square squareType { get; set; } //ToSelf: was private!

        void RotateClockwise();
        void RotateCounterClockwise();

        //Color? ColorAt(Vector2 position);
    }
}
