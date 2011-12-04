using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class Block : IBlock
    {
        public List<Vector2> vectors { get; private set; }
        public Square squareType { get; private set; }

        public Block(int width, int height)
        {
            //width, height??

            squareType = Square.Peg;
            
            //vectors moet nog naar Square class...
            vectors = new List<Vector2>();
            vectors.Add(new Vector2(0, -1));
            vectors.Add(new Vector2(0, 0));
            vectors.Add(new Vector2(1, 1));
            vectors.Add(new Vector2(0, 1));            
        }

        public Color? ColorAt(Vector2 position)
        {
            //nog nodig?
            throw new NotImplementedException();
        }

        public void RotateClockwise()
        {
            var newVectors = new List<Vector2>();
            foreach (var oldVector in vectors)
            { 
                newVectors.Add(new Vector2(oldVector.Y,-oldVector.X));
            }
            vectors = newVectors;
        }
        public void RotateCounterClockwise()
        {
            var newVectors = new List<Vector2>();
            foreach (var oldVector in vectors)
            {
                newVectors.Add(new Vector2(-oldVector.Y, oldVector.X));
            }
            vectors = newVectors;
        }
        //methodes kun je natuurlijk samenvoegen..

        //naamgeving ambigue?
        public void RotateLeft()
        {
            throw new NotImplementedException();
        }
        public void RotateRight()
        {
            throw new NotImplementedException();
        }
    }
}
