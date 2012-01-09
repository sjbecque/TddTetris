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
    class GridTest //ToSelf: Is toch nog afhankelijk van domain-klassen.. (namelijk Square..);
    {
        //ToSelf: eigenlijk moeten alle tests ook nog met een Grid gecreeert met de andere constructor?

        private static int gridWidth = 4; //random;
        private static int gridHeight = 3; //random;
        private static Square gridDefault = Square.Empty;
        private static Grid grid = new Grid(gridWidth, gridHeight, gridDefault);        
        private static Square x = Square.Bar; //for visual convenience;
        private static Square _ = Square.Empty; //for visual convenience;
        private static Grid gridMain = new Grid(
            new Square[,] {
                {_,_,_},
                {_,_,_},
                {x,_,_}
        });
        //=======================================================

        [Test]
        public void GridHasProperDimensions()
        {
            //Reason: it is easy to mix up the x- and y-dimensions;
            for (int posX = 0; posX < grid.Width; posX++)
            {
                var test = grid[0, posX];
            }
            for (int posY = 0; posY < grid.Height; posY++)
            {
                var test = grid[posY, 0];
            }

            //ToSelf: err..no assert?
        }

        [Test]
        public void GridCellsAreInitiatedWithDefaultValue()
        {
            foreach (var square in grid.getCells())
            {
                Assert.IsTrue(square == gridDefault);
            }            
        }

        [Test]
        public void iterate_returnsRightNumberOfGridCells()
        {
            bool numberOfGridCellsChecksOut = grid.getCells().Count() == gridWidth * gridHeight;

            Assert.IsTrue(numberOfGridCellsChecksOut, "Number of grid cells doesn't match grid-dimensions");
        }
        
        [Test]
        public void iterateHorizontal()
        {
            var grid = new Grid(
                new Square[,]{
                    {x, x},
                    {_, _}
            }); 

            var cells = grid.getCellsHorizontally();

            Assert.True(cells.Count() == 4); //ToSelf: zodat de test faalt, wanneer deze specifieke grid-constructor nog niet geimplementeerd is;
                        
            Assert.True(cells[0] == x);
            Assert.True(cells[1] == x);
            Assert.True(cells[2] == _);
            Assert.True(cells[3] == _);
        }

        [Test]
        public void Equals()
        {
            var gridDezelfde = new Grid(
                new Square[,] {
                    {_,_,_},
                    {_,_,_},
                    {x,_,_}
            });

            bool b = gridMain.Equals(gridDezelfde);
            Assert.IsTrue(b);

            //ToSelf: moet je dan ook meteen een test toevoegen waarbij 2 grids dan niet equal zijn, en soortgelijk voor de andere tests?
        }
        
        [Test]
        public void ShallowClone()
        {
            Assert.IsTrue( gridMain.Equals(gridMain.getShallowClone()) );
        }

        [Test]
        public void Superposition()
        {
            //fixedBlocks anders invullen, want property hoort private te zijn

            var field = new Field(3, 3);
            field.fixedBlocks = new Grid(
                new Square[,] {
                    {_,_,_},
                    {_,_,_},
                    {x,_,_}
            });
            field.Position = new Vector2(1, 1);

            var block = new Block(); //!!moet interface worden
            block.vectors = new List<Vector2>
            {
                new Vector2(0,1), //ToSelf: let op [x,y] , niet [y,x] overigens
                new Vector2(1,0)
            };
            block.squareType = x;
            field.Block = block;

            bool ClippingOccured = false;
            Grid gridSuperposition = field.getSuperposition(out ClippingOccured);

            var gridForCheck = new Grid(
                new Square[,] {
                    {_,_,_},
                    {_,_,x},
                    {x,x,_}
            });

            Assert.IsTrue( gridSuperposition.Equals(gridForCheck) );
            bool c = ClippingOccured;//nog meer tests nodig
        }
    }

    //ToSelf: Te gebruiken nog i.p.v. domainklasse Square?
    class TestContents
    { 
        public int testValue = 99;
    }
}
