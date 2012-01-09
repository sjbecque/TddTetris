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
                field.Move(Movement.Left);
            }

            if (input.IndexOf(Keys.Right) > -1)
            {
                field.Move(Movement.Right);
            }

            if (input.IndexOf(Keys.Space) > -1)
            {
                //field.FixBlock();
            }

            if (input.IndexOf(Keys.PageDown) > -1)
            {
                field.Move(Movement.RotateClockwise);
            }

            if (input.IndexOf(Keys.Delete) > -1)
            {
                field.Move(Movement.RotateCounterClockwise);
            }
        }

        public void AdvanceIfPossible()
        {
            bool newBlockShouldBeInserted = field.Move(Movement.Down);

            if (newBlockShouldBeInserted)
            {
                StartNewBlock();
            }
        }
        
        private void StartNewBlock()
        {
            field.SetBlock(blockFactory.MakeBlock(), new Vector2(field.Width / 2, 3));
        }
    }
}
