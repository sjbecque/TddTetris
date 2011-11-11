using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public interface IField
    {
        int Width { get; }

        bool CanMoveLeft();
        void MoveBlockLeft();

        bool CanMoveRight();
        void MoveBlockRight();

        bool CanAdvance();
        void AdvanceBlock();

        void SetBlock(IBlock block, Vector2 position);
        void FixBlock();
    }
}
