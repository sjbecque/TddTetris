using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class Field : IField
    {
        #region Properties, Constructor
        public int Width { get; private set; }
        public int Height { get; private set; }

        public IBlock Block { get; private set; }

        private Vector2 _position;
        public Vector2 Position
        {
            get 
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public Square[,] fixedBlocks { get; private set; }

        public Field(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.fixedBlocks = General.getMatrix<Square>(Width, Height, Square.Empty);
        }
        #endregion

        public Square[,] getSuperposition()
        {            
            //vervang door createsquare
            var fixedBlocksCopy = new Square[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    fixedBlocksCopy[x, y] = fixedBlocks[x, y];
                }
            }

            foreach (var vector in Block.vectors)
            {
                var x_ = (int)Position.X + (int)vector.X;
                var y_ = (int)Position.Y + (int)vector.Y;

                if ((x_ >= 0) && (x_ < Width) && (y_ >= 0) && (y_ < Height))
                {
                    fixedBlocksCopy[x_, y_] = Block.squareType;
                }
            }

            return fixedBlocksCopy;          
  
            //methode nog verplaatsen..
        }

        public Color? ColorAt(Vector2 position)
        {
            float x = position.X;
            float y = position.Y;
                        
            if (x < 0 || x >= Width || y < 0 || y >= Height) //floats in compare..
            {
                throw new IndexOutOfRangeException();
            }

            var requestedSquare = getSuperposition()[(int)Width, (int)Height];
            return requestedSquare.color;
        }

        public void SetBlock(IBlock block, Vector2 position)
        {
            this.Block = block;
            this.Position = position;
        }

        public bool CanAdvance()
        {
            return Position.Y < Height - 1;
        }
        public void AdvanceBlock()
        {
            Position = new Vector2(Position.X, Position.Y + 1);
        }

        public bool CanMoveLeft()
        {
            return Position.X > 0;
        }
        public void MoveBlockLeft()
        {
            Position = new Vector2(Position.X - 1, Position.Y);
        }

        public bool CanMoveRight()
        {
            return Position.X < Width - 1;
        }
        public void MoveBlockRight()
        {
            Position = new Vector2(Position.X + 1, Position.Y);
        }

        public void FixBlock()
        {
            foreach (var vector in Block.vectors)
            {
                var resultanteX = (int)Position.X + (int)vector.X;
                var resultanteY = (int)Position.Y + (int)vector.Y;

                if ((resultanteX < Width) && (resultanteY < Height))
                {   
                    fixedBlocks[resultanteX, resultanteY] = Block.squareType;
                }
            }
        }

        public void RotateClockwise()
        {
            Block.RotateClockwise();
        }

        public void RotateCounterClockwise()
        {
            Block.RotateCounterClockwise();
        }
    }
}
