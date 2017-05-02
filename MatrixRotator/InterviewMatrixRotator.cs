using System;

namespace MatrixRotator
{
    public interface IMatrixRotator
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="martix">Initial matrix</param>
        /// <param name="r">Number of rotations</param>
        /// <returns>Rotated matrix</returns>
        int[][] Rotate(int[][] martix, int r);
    }

    public class InterviewMatrixRotator : IMatrixRotator
    {
        public int[][] Rotate(int[][] martix, int r)
        {
            if (r < 1 || r > 109)
                throw new Exception("Rotation count should be in range: 1 - 109");

            for (int i = 0; i < r; i++)
                martix = _Rotate(martix);

            return martix;
        }

        private int[][] _Rotate(int[][] martix)
        {
            var circleCount = martix.Length / 2;

            var n = martix.Length;
            var m = martix[0].Length;

            if (m < 2 || n < 2 || m > 300 || n > 300)
                throw new Exception("M and N should be in range: 2 - 300");

            var savedElements = new int[circleCount];

            for (int i = 0; i < circleCount; i++)
                savedElements[i] = martix[i][i];

            for (int loop = 0; loop < circleCount; loop++)
            {
                for (int i = loop; i < m - loop - 1; i++)
                    martix[loop][i] = martix[loop][i + 1];

                var rowStart = loop;
                var rowEnd = n - loop - 1;
                var col = m - loop - 1;

                for (int i = rowStart; i < rowEnd; i++)
                    martix[i][col] = martix[i + 1][col];

                var colEnd = m - loop - 1;
                var colStart = loop;
                var row = n - loop - 1;
                for (int i = colEnd; i > colStart; i--)
                    martix[row][i] = martix[row][i - 1];

                for (int i = n - loop - 1; i > loop; i--)
                    martix[i][loop] = martix[i - 1][loop];
            }

            for (int i = 0; i < circleCount; i++)
                martix[i + 1][i] = savedElements[i];

            return martix;
        }
    }
}
