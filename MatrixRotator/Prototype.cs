using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixRotator
{
    public class PrototypeInterviewMatrixRotator : IMatrixRotator
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


            int[][] old = null;

            for (int loop = 0; loop < circleCount; loop++)
            {
                old = Clone(martix);

                for (int i = loop; i < m - loop - 1; i++)
                    martix[loop][i] = martix[loop][i + 1];

                Print(martix, old);
                old = Clone(martix);


                var rowStart = loop;
                var rowEnd = n - loop - 1;
                var col = m - loop - 1;

                for (int i = rowStart; i < rowEnd; i++)
                    martix[i][col] = martix[i + 1][col];

                Print(martix, old);
                old = Clone(martix);


                var colEnd = m - loop - 1;
                var colStart = loop;
                var row = n - loop - 1;
                for (int i = colEnd; i > colStart; i--)
                    martix[row][i] = martix[row][i - 1];

                Print(martix, old);
                old = Clone(martix);


                for (int i = n - loop - 1; i > loop; i--)
                    martix[i][loop] = martix[i - 1][loop];

                Print(martix, old);
            }

            for (int i = 0; i < circleCount; i++)
                martix[i + 1][i] = savedElements[i];

            Print(martix, old);


            return martix;
        }

        private static int[][] Clone(int[][] martix)
        {
            var old = new int[martix.Length][];

            for (int i = 0; i < martix.Length; i++)
            {
                old[i] = new int[martix[0].Length];
                for (int j = 0; j < martix[0].Length; j++)
                    old[i][j] = martix[i][j];
            }
            return old;
        }

        void Print(int[][] matrix, int[][] old)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    Console.ForegroundColor = old[i][j] != matrix[i][j]
                        ? ConsoleColor.Blue
                        : ConsoleColor.White;
                    Console.Write(old[i][j].ToString().PadLeft(3, ' '));
                }

                Console.Write("   ");

                for (int j = 0; j < matrix[0].Length; j++)
                {
                    Console.ForegroundColor = old[i][j] != matrix[i][j]
                        ? ConsoleColor.Red
                        : ConsoleColor.White;
                    Console.Write(matrix[i][j].ToString().PadLeft(3, ' '));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    class Test
    {
        public bool IsDebugMode;

        public Test(bool isDebugMode) {
            IsDebugMode = isDebugMode;
        }

        public IEnumerable<bool> Do()
        {
            var matrix = new int[0][];
            var resultMatrix = new int[0][];

            matrix = new[]
            {
                new[] {1, 2, 3, 4},
                new[] {5, 6, 7, 8},
                new[] {9, 10, 11, 12},
                new[] {13, 14, 15, 16},
            };

            resultMatrix = new[]
            {
                new[] {2, 3, 4, 8},
                new[] {1, 7, 11, 12},
                new[] {5, 6, 10, 16},
                new[] {9, 13, 14, 15},
            };

            yield return DoTest(matrix, resultMatrix, 1);

            resultMatrix = new[]
            {
                new[] {3, 4, 8, 12},
                new[] {2, 11, 10, 16},
                new[] {1, 7, 6, 15},
                new[] {5, 9, 13, 14},
            };

            yield return DoTest(matrix, resultMatrix, 1);

            matrix = new[]
            {
                new[] {1, 2, 3, 4},
                new[] {7, 8, 9, 10},
                new[] {13, 14, 15, 16},
                new[] {19, 20, 21, 22},
                new[] {25, 26, 27, 28},
            };

            resultMatrix = new[]
            {
                new[] {28, 27, 26, 25},
                new[] {22, 9, 15, 19},
                new[] {16, 8, 21, 13},
                new[] {10, 14, 20, 7},
                new[] {4, 3, 2, 1},
            };

            yield return DoTest(matrix, resultMatrix, 7);
        }

        private bool DoTest(int[][] matrix, int[][] resultMatrix, int count)
        {
            var result = IsDebugMode 
                ? new PrototypeInterviewMatrixRotator().Rotate(matrix, count)
                : new InterviewMatrixRotator().Rotate(matrix, count);
            

            var isEqual = true;

            if (result.Length != resultMatrix.Length || result[0].Length != resultMatrix[0].Length)
                return false;

            for (int i = 0; i < resultMatrix.Length; i++)
            {
                for (int j = 0; j < resultMatrix[0].Length; j++)
                {
                    if (result[i][j] != resultMatrix[i][j])
                    {
                        isEqual = false;
                        break;
                    }
                }
                if (!isEqual)
                    break;
            }

            return isEqual;
        }
    }
}
