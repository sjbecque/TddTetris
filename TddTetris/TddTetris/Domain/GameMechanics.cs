using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class GameMechanics //in field zit ook mechanics..?
    {
        private readonly IField field;
        private readonly IBlockFactory blockFactory;

        public GameMechanics(IField field, IBlockFactory blockFactory)
        {
            this.field = field;
            this.blockFactory = blockFactory;
            StartNewBlock();
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

            if (input.IndexOf(Keys.Space) > -1)
            {
                field.FixBlock(); //moet nog via advanceIfPossible?
            }

            if (input.IndexOf(Keys.PageDown) > -1)
            {
                field.RotateClockwise();
            }

            if (input.IndexOf(Keys.Delete) > -1)
            {
                field.RotateCounterClockwise();
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
                StartNewBlock();
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

        private void StartNewBlock()
        {
            field.SetBlock(blockFactory.MakeBlock(), new Vector2(field.Width / 2, 0));
        }
    }
}
