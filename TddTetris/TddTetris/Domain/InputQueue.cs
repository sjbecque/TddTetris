using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TddTetris
{
    /**
     * Make sure that any keypresses are recorded only once. XNA 'repeats' keypresses at every iteration.
     *
     * At some point, I'd like to add repetition.
     */
    public class InputQueue
    {
        private List<Keys> lastKeys = new List<Keys>();

        // Returns only the keys that were pressed keys since its last invocation.
        public List<Keys> keyPress(List<Keys> keys)
        {
            var newlyPressedKeys = keys.Except(lastKeys);

            lastKeys = keys;

            return newlyPressedKeys.ToList<Keys>();
        }
    }
}
