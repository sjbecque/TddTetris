using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework.Input;

namespace TddTetris
{
    public class GameMechanics
    {
        private readonly Field field;

        public GameMechanics(Field field)
        {
            this.field = field;
        }

        public void HandleInput(List<Keys> input)
        {
            if (input.IndexOf(Keys.Left) > -1)
            {
                field.MoveBlockLeft();
            }

            if (input.IndexOf(Keys.Right) > -1)
            {
                field.MoveBlockRight();
            }
        }
    }
}
