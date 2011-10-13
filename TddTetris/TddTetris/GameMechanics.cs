using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class GameMechanics
    {
        private readonly IField field;

        public GameMechanics(IField field)
        {
            this.field = field;
        }

        public void HandleInput(List<Keys> input)
        {
            if (input.IndexOf(Keys.Left) > -1)
            {
                MoveLeftIfPossible();
            }

            if (input.IndexOf(Keys.Right) > -1)
            {
                MoveRightIfPossible();
            }
        }

        public void AdvanceIfPossible()
        {
            if (field.CanAdvance())
            {
                field.AdvanceBlock();
            }
            else
            {
                field.FixBlock();
                field.SetBlock( new Block(), new Vector2(field.Width / 2, 0) );
            }
        }

        public void MoveLeftIfPossible()
        {
            if (field.CanMoveLeft())
            {
                field.MoveBlockLeft();
            }
        }

        public void MoveRightIfPossible()
        {
            if (field.CanMoveRight())
            {
                field.MoveBlockRight();
            }
        }
    }
}
