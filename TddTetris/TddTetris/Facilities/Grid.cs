using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    //De keuze voor een hele grid-class is misschien wat eigenaardig, maar ik streef er vaak naar domein-code en algemene faciliteiten zoveel mogelijk uit elkaar te trekken, vandaar; De domein-code wordt er lekker light en overzichtelijk van (= het idee tenminste);

    public class GridBase<T>
    {       
        private T[,] _grid;
        public int Width { get; private set; }
        public int Height { get; private set; }

        private T _InitValue;
        public T InitValue
        {
            get {
                return this._InitValue;
            }
            private set {
                if (this._InitValue != null) throw new Exception("initializationValue has already been set");
                this._InitValue = value;            
            }
        }
        
        //=======================================================

        #region Constructors
        public GridBase(T[,] grid)
        {
            this._grid = grid;            
            this.Height = this._grid.GetLength(0);
            this.Width = this._grid.GetLength(1);
        }

        public GridBase(int height, int width, T initializationValue)
        {            
            this.Height = height;
            this.Width = width;
            this.InitValue = initializationValue;

            this._grid = new T[height, width];
            resetCells();
        }
        #endregion

        //=======================================================

        /// <summary>
        /// Indexer to conveniently query the grid;
        /// </summary>
        /// <param name="positionX"></param>
        /// <param name="positionY"></param>
        /// <returns></returns>
        public T this[int positionY, int positionX]
        {
            get { return _grid[positionY, positionX]; }
            set { _grid[positionY, positionX] = value; }
        }

        public void resetCells()
        {
            iterateHorizontal(
                (y, x) => this[y,x] = InitValue
            ).ToList();
            // Here the inserted Func<> is effectively an Action<>, because the is no value returned;
            // The ToList() makes sure resulting IEnumemrable is enumerated, and consistent with getCellHorizontally();
        }

        public List<T> getCells()
        {
            return getCellsHorizontally();
        }
        public List<T> getCellsHorizontally()
        {
            return iterateHorizontal(
                (y, x) => this[y,x]
            ).ToList();
        }

        public GridBase<T> getShallowClone()
        {
            var ShallowClone = new GridBase<T>(Height, Width, InitValue);

            iterateHorizontal((y, x) => ShallowClone[y, x] = this[y, x]).ToList();

            return ShallowClone;
        }


        public bool Equals(GridBase<T> otherGrid) //ToSelf: weet niet of dit goed is, qua naam etc
        {
            //ToSelf: zou kunnen worden vervangen door native clone faciliteiten?

            if ((Width != otherGrid.Width ) || (Height != otherGrid.Height)) return false;
            if (!object.Equals(InitValue, otherGrid.InitValue)) return false;
            
            //Compare cell-pairs; //ToSelf: misschien beter te doen met LINQ.aggregate o.i.d; zodat de booleans ook helderder worden
            var CellComparisonResults = this.getCells().Zip(otherGrid.getCells(), (T1, T2) => object.Equals(T1, T2));
            bool allCellsEqual = !CellComparisonResults.Any(b => b == false);
            return allCellsEqual;
        }
        
        //==========================================================================================================
        
        /// <summary>
        /// Iterates all cells row by row, starting from the upper left corner, and allows a function to act on each
        /// cell and return a value;
        /// </summary>
        /// <returns>Collection of values from a custom function operating on each cell</returns>
        private IEnumerable<T> iterateHorizontal(Func<int, int, T> CellFunction)
        {
            for (int y = 0; y < _grid.GetLength(0); y++)
            {
                for (int x = 0; x < _grid.GetLength(1); x++)
                {
                    yield return CellFunction(y, x);
                }
            }
        }

        //ToSelf: IEnumerable nog implementeren in Grid<T>?
    }
}
