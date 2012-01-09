using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TddTetris;
using Moq;
using Microsoft.Xna.Framework;

namespace Tests
{
    using Grid = GridBase<Square>;

    [TestFixture]
    class FieldTest
    {
        private static Square x = Square.Bar;
        private static Square _ = Square.Empty;

        private Field getTestField()
        {
            //fixedBlocks anders invullen, want property hoort private te zijn

            var field = new Field(3, 3);
            field.fixedBlocks = new Grid(
                new Square[,] {
                    {_,_,_},
                    {_,_,_},
                    {x,_,_}
            });

            field.Block = new Block(); //!!moet interface worden
            field.Block.vectors = new List<Vector2>
            {
                new Vector2(0,1), //ToSelf: let op [x,y] , niet [y,x] overigens
                new Vector2(1,0)/*,
                new Vector2(99,99)*/
            };
            field.Block.squareType = x;
            
            return field;
        }

        private Field getTestField2()
        {
            //fixedBlocks anders invullen, want property hoort private te zijn

            var field = new Field(3, 3);
            field.fixedBlocks = new Grid(
                new Square[,] {
                    {_,_,_},
                    {_,_,_},
                    {_,_,_}
            });

            field.Block = new Block(); //!!moet interface worden
            field.Block.squareType = x;

            return field;
        }

        //==========================================================================

        [Test]
        public void getSuperposition_shouldMatchTheCheckGrid()
        {
            var field = getTestField();
            field.Position = new Vector2(1, 1);

            bool ClippingOccured = false;
            Grid gridSuperposition = field.getSuperposition(out ClippingOccured);

            var gridForCheck = new Grid(
                new Square[,] {
                    {_,_,_},
                    {_,_,x},
                    {x,x,_}
            });

            Assert.IsTrue(gridSuperposition.Equals(gridForCheck));
            //Assert.IsTrue(ClippingOccured);
        }

        [Test]
        public void getSuperposition_shouldNotMatchTheCheckGrid()
        {
            //set up
            var field = getTestField();
            field.Position = new Vector2(1, 1);
            bool ClippingOccured = false;      
      
            var gridForCheck = new Grid(
                new Square[,] {
                    {_,_,_},
                    {_,x,x},
                    {x,x,_}
            });

            //act            
            Grid gridSuperposition = field.getSuperposition(out ClippingOccured);

            //assert
            Assert.IsFalse(gridSuperposition.Equals(gridForCheck));
            //Assert.IsTrue(ClippingOccured); //ToSelf: deze nog omdraaien, dus vector aanpassen; nee ClippingOccured eruit halen
        }

        [Test]
        public void canGetCell()
        {
            var field = getTestField();
            Assert.IsTrue( field.canGetCell(1, 1) );
            Assert.IsTrue( !field.canGetCell(3, 0) );
            Assert.IsTrue( !field.canGetCell(0, 3) );
            Assert.IsTrue( !field.canGetCell(-1, 0) );
            Assert.IsTrue( !field.canGetCell(0, -1) );
        }

        [Test]
        public void move_ShouldMoveDown()
        {
            var field = getTestField();
            field.Position = new Vector2(1, 1);
            field.Move(Movement.Down);

            var gridForCheck = new Grid(
                new Square[,] {
                    {_,_,_},
                    {_,_,x},
                    {x,x,_}
            });

            Assert.IsTrue( field.getSuperposition().Equals(gridForCheck) );
        }

        [Test]
        public void move_ShouldNotMoveDown()
        {
            var field = getTestField();
            field.Position = new Vector2(0, 0);
            field.Move(Movement.Down);

            var gridForCheck = new Grid(
                new Square[,] {
                    {_,x,_},
                    {x,_,_},
                    {x,_,_}
            });

            Assert.IsTrue(field.getSuperposition().Equals(gridForCheck));
        }
        
        [Test]
        public void move_ShouldMoveLeft()
        {
            var field = getTestField();
            field.Position = new Vector2(1, 0);
            field.Move(Movement.Left);

            var gridForCheck = new Grid(
                new Square[,] {
                    {_,x,_},
                    {x,_,_},
                    {x,_,_}
            });

            Assert.IsTrue(field.getSuperposition().Equals(gridForCheck));
        }

        [Test]
        public void move_ShouldNotMoveLeft()
        {
            var field = getTestField();
            field.Position = new Vector2(1, 1);
            field.Move(Movement.Left);

            var gridForCheck = new Grid(
                new Square[,] {
                    {_,_,_},
                    {_,_,x},
                    {x,x,_}
            });

            Assert.IsTrue(field.getSuperposition().Equals(gridForCheck));
        }

        [Test]
        public void move_ShouldMoveRight()
        {
            var field = getTestField();
            field.Position = new Vector2(0, 0);
            field.Move(Movement.Right);

            var gridForCheck = new Grid(
                new Square[,] {
                    {_,_,x},
                    {_,x,_},
                    {x,_,_}
            });

            Assert.IsTrue(field.getSuperposition().Equals(gridForCheck));
        }

        [Test]
        public void move_ShouldNotMoveRight()
        {
            var field = getTestField();
            field.Position = new Vector2(1, 1);
            field.Move(Movement.Right);

            var gridForCheck = new Grid(
                new Square[,] {
                    {_,_,_},
                    {_,_,x},
                    {x,x,_}
            });

            Assert.IsTrue(field.getSuperposition().Equals(gridForCheck));
        }

        [Test]
        public void move_RotateClockwise()
        {
            //ToSelf: draait alleen nog om origin, dus moeten [0,0] bevatten
            //Let op, als hij clipt, roteert hij ook niet. Niet handig voor tests dus?          

            var field = getTestField2();
            field.Position = new Vector2(1, 1);
            field.Block.vectors = new List<Vector2>
            {
                new Vector2(0,0), //ToSelf: let op [x,y] , niet [y,x] overigens
                new Vector2(1,0),
                new Vector2(0,1)
            };

            field.Move(Movement.RotateClockwise);

            var gridForCheck = new Grid(
                new Square[,] {
                    {_,_,_},
                    {x,x,_},
                    {_,x,_}
            });

            Assert.IsTrue(field.getSuperposition().Equals(gridForCheck));
        }

        [Test]
        public void move_RotateCounterClockwise()
        {
            var field = getTestField2();
            field.Position = new Vector2(1, 1);
            field.Block.vectors = new List<Vector2>
            {
                new Vector2(0,0), //ToSelf: let op [x,y] , niet [y,x] overigens
                new Vector2(1,0),
                new Vector2(0,1)
            };

            field.Move(Movement.RotateCounterClockwise);

            var gridForCheck = new Grid(
                new Square[,] {
                    {_,x,_},
                    {_,x,x},
                    {_,_,_}
            });

            Assert.IsTrue(field.getSuperposition().Equals(gridForCheck));
        }


    }
}
