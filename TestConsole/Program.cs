using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using NUnit.Framework;
using TddTetris;
using Microsoft.Xna.Framework;
//using Moq;

namespace TestConsole
{
    using Grid = GridBase<Square>;

    class Program
    {
        private static Square x = Square.Bar;
        private static Square _ = Square.Empty;

        private static Grid gridMain = new Grid(
            new Square[,] {
                {_,_,_},
                {_,_,_},
                {x,_,_}
        });







        static void Main(string[] args)
        {
            //fixedBlocks anders invullen, want property hoort private te zijn

            var field = new Field(3,3);
            field.fixedBlocks = new Grid(
                new Square[,] {
                    {_,_,_},
                    {_,_,_},
                    {x,_,_}
            });
            field.Position = new Vector2(1,1);

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

            bool r = gridSuperposition.Equals(gridForCheck);
            bool c = ClippingOccured;
        }



    }
}
