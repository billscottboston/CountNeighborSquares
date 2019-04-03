using System;
using System.Collections.Generic;

namespace sandbox {
    class Program {
        static void Main (string[] args) {
            MatrixWalker mw = new MatrixWalker ();
            Console.WriteLine (mw.DetermineSquareCount ());
        }
    }
    public class MatrixWalker {
        public string[, ] InitMatrix () {
            // X X Y Y Y I I 
            // Y X X P P P P
            // U X U P R R P
            // U X A A A A B
            // U X A A A A P
            // I I I I A P E
            // U X U P R R P
            string[, ] theMatrix = new string[7, 7];

            string[] src = "X X Y Y Y I I Y X X P P P P U X U P R R P U X A A A A B U X A A A A P I I I I A P E U X U P R R P".Split (" ");
            int x = 0;
            int y = 0;
            foreach (string letter in src) {
                theMatrix[y, x] = letter;
                x++;
                if (x >= theMatrix.GetLength (1)) {
                    x = 0;
                    y++;
                }
            }
            return theMatrix;
        }

        public int GetSameSquareCount (string[, ] theMatrix, string previousValue, int prevY, int prevX, int y, int x, bool[, ] visited) {
            int rtn = 0;

            if (y < 0 || x < 0 || theMatrix.GetLength (0) <= y || theMatrix.GetLength (1) <= x)
                return 0;

            if (visited[y, x])
                return 0;

            string thisValue = theMatrix[y, x];
            visited[y, x] = true;

            if (previousValue != null && previousValue != thisValue)
                return 0;

            rtn += GetSameSquareCount (theMatrix, thisValue, y, x, y - 1, x, visited);
            rtn += GetSameSquareCount (theMatrix, thisValue, y, x, y + 1, x, visited);
            rtn += GetSameSquareCount (theMatrix, thisValue, y, x, y, x - 1, visited);
            rtn += GetSameSquareCount (theMatrix, thisValue, y, x, y, x + 1, visited);
            return rtn + 1;

        }
        public class Result {
            public string arrayPosition;
            public string arrayValue;
            public int count;
        }
        public string DetermineSquareCount () {

            int currentCount;
            List<Result> results = new List<Result> ();
            var theMatrix = InitMatrix ();

            for (int x = 0; x < theMatrix.GetLength (0); x += 1) {
                for (int y = 0; y < theMatrix.GetLength (1); y += 1) {
                    string arrayVal = theMatrix[x, y];
                    bool[, ] theMatrixVisited = new bool[7, 7];
                    currentCount = GetSameSquareCount (theMatrix, arrayVal, x, y, x, y, theMatrixVisited);
                    results.Add (new Result () { arrayPosition = string.Format ("{0},{1}", y, x), arrayValue = arrayVal, count = currentCount });
                }
            }
            results.Sort ((r1, r2) => r1.count.CompareTo (r2.count));
            string rtn = "";
            foreach (var res in results) {
                rtn += string.Format ("position={0} value={1} count={2}", res.arrayPosition, res.arrayValue, res.count) + Environment.NewLine;
            }
            return rtn;
        }
    }
}