using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TddTetris
{
    /**
     * Make sure that any keypresses are recorded only once
     */
    public class InputQueue
    {
        private List<Keys> lastKeys = new List<Keys>();
        public List<Keys> keyPress(List<Keys> keys)
        {
            var newlyPressedKeys = keys.Except(lastKeys);

            lastKeys = keys;

            return newlyPressedKeys.ToList<Keys>();
        }
    }
}
