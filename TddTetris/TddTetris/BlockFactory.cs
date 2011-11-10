using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TddTetris
{
    public class BlockFactory : IBlockFactory
    {
        public IBlock MakeBlock()
        {
            return new Block();
        }
    }
}
