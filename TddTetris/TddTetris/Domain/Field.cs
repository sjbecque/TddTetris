using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    using Grid = GridBase<Square>; //ToSelf: kunnen beide Grid worden genoemd, via namespaces of zo?

    public class Field : IField
    {
        #region Properties, Constructor
        public int Width { get; private set; }
        public int Height { get; private set; }

        public IBlock Block { get; set; } //was private

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

        public Grid fixedBlocks { get; set; } //ToSelf: was private

        //===================================================

        public Field(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.fixedBlocks = new Grid( this.Height, this.Width, Square.Empty);
        }
        #endregion

        #region Superposition

        public Grid getSuperposition()
        {
            bool ClippingOccured;
            return getSuperposition(out ClippingOccured);
        }

        public Grid getSuperposition(out bool ClippingOccured)
        {
            var gridShallowClone = (Grid)fixedBlocks.getShallowClone();
            ClippingOccured = false;

            foreach (Vector2 vector in Block.vectors)
            {
                var resultant = Position + vector;

                if (canGetCell(resultant.y(), resultant.x()))
                {
                    if (fixedBlocks[resultant.y(), resultant.x()] != Square.Empty) ClippingOccured = true; //ToSelf: nog methodes met vector-parameters maken?

                    gridShallowClone[resultant.y(), resultant.x()] = Block.squareType;
                }
                else
                {
                    ClippingOccured = true;
                }
            }

            return gridShallowClone;
        }

        public bool canGetCell(int y, int x)  //ToSelf: hoe maak ik dit ook al weer private terwijl tests erbij kunnen?
        {
            return ((y >= 0) && (y < Height) && (x >= 0) && (x < Width));
        }

        #endregion

        #region Dynamics

        public void SetBlock(IBlock block, Vector2 position)
        {
            this.Block = block;
            this.Position = position;
        }

        public bool Move(Movement movement)
        {
            bool ClippingOccured = false;
            bool newBlockShouldBeInserted = false;
            var oldPosition = Position;
            var oldSuperposition = getSuperposition(); //out-constructie moet eruit

            DoMovement(movement);

            var newSuperposition = getSuperposition(out ClippingOccured); //the naming of newSuperposition is only for clearity but the variable is not used;

            if (ClippingOccured)
            {
                RestorePrevious(oldPosition, movement);

                if (movement == Movement.Down)
                {                 
                    fixedBlocks = oldSuperposition;

                    newBlockShouldBeInserted = true;
                }
            }

            return newBlockShouldBeInserted;
        }

        private void DoMovement(Movement movement)
        {
            if (movement == Movement.Down)
            {
                Position = new Vector2(Position.X, Position.Y + 1);
            }
            else if (movement == Movement.Left)
            {
                Position = new Vector2(Position.X - 1, Position.Y);
            }
            else if (movement == Movement.Right)
            {
                Position = new Vector2(Position.X + 1, Position.Y);
            }
            else if (movement == Movement.RotateClockwise)
            {
                Block.RotateClockwise();
            }
            else if (movement == Movement.RotateCounterClockwise)
            {
                Block.RotateCounterClockwise();
            }
            else throw new Exception("Unknown movement requested");
        }
        private void RestorePrevious(Vector2 oldPosition, Movement movement)
        {
            Position = oldPosition;
            if (movement == Movement.RotateClockwise) Block.RotateCounterClockwise();
            if (movement == Movement.RotateCounterClockwise) Block.RotateClockwise();
        }

        #endregion
    }
}
