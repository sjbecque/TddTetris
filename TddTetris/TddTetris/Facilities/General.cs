using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TddTetris
{
    class General
    {
        //Oeps, ze hebben ook een Matrix class in het Xna-framework...
        public static T[,] getMatrix<T>(int width, int height, T initializationValue)
        {
            var matrix = new T[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    matrix[x, y] = initializationValue;
                }
            }
            return matrix;
        }
    }
}
