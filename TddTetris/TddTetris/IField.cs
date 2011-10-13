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

        void MoveBlockLeft();

        void MoveBlockRight();

        void AdvanceBlock();

        void SetBlock(Block block, Vector2 position);
    }
}
