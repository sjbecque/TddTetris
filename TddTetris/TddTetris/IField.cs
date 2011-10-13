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

        void FixBlock();

        void SetBlock(Block block, Vector2 position);
    }
}
