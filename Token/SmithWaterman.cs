using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Token
{
    /// <summary>
    /// Class SmithWaterman to simulate Smith Waterman
    /// similarity score finding on two string.
    /// v. 0.1
    /// <author>Aditya Situmeang</author>
    /// <email>bananab9001@gmail.com</email>
    /// </summary>
    public class SmithWaterman
    {
        /**
         * Defines the Score.
         */
        private const double GAP = -2;
        private const double MISS = -1;
        private const double MATCH = 3;
        /**
         * Defines the direction. Useful when doing
         * trace back in matrix.
         */
        private const int LEFT = 0;
        private const int UP = 1;
        private const int DIAGONAL = 2;
        private const int D_MISS = 3;
        private const int ZERO = 4;
        /// <summary>
        /// Helds the maximum score in matrix
        /// </summary>
        public double MaxScore { get; set; }
        /// <summary>
        /// Helds the maximum score.
        /// </summary>
        public double MaxGain { get; set; }
        /// <summary>
        /// Matrix that do the scoring system.
        /// Using List to be more dynamic.
        /// </summary>
        private MatrixCell[,] matrix;
        /// <summary>
        /// Helds the first string.
        /// </summary>
        public string[] FirstStr { get; set; }
        /// <summary>
        /// Helds the second string.
        /// </summary>
        public string[] SecondStr { get; set; }
        /// <summary>
        /// Row position of Maximum Score.
        /// </summary>
        private int MaxRow { get; set; }
        /// <summary>
        /// Column position of Maximum Score.
        /// </summary>
        private int MaxCol { get; set; }
        /// <summary>
        /// Traced back string of first string.
        /// </summary>
        public List<string> Result1 { get; set; }
        /// <summary>
        /// Traced back string of second string.
        /// </summary>
        public List<string> Result2 { get; set; }
        /// <summary>
        /// Create a SmithWaterman object using default value
        /// that can calculate similarity.
        /// </summary>
        public SmithWaterman()
        {
            FirstStr = null;
            SecondStr = null;
            matrix = null;
            MaxScore = 0;
            Result1 = new List<string>();
            Result2 = new List<string>();
        }
        /// <summary>
        /// Create a SmithWaterman object using assigned value
        /// that can calculate similarity.
        /// </summary>
        /// <param name="first_string"></param>
        /// <param name="second_string"></param>
        public SmithWaterman(string[] first_string, string[] second_string)
        {
            FirstStr = first_string;
            SecondStr = second_string;
            matrix = new MatrixCell[first_string.Length + 1, second_string.Length + 1];
            MaxScore = 0;
            Result1 = new List<string>();
            Result2 = new List<string>();
        }
        /// <summary>
        /// mengemprint ke layar konsol
        /// </summary>
        private void PrintToConsole()
        {
            for (int i = 0; i <= FirstStr.Length; i++)
            {
                //  fill cell
                for (int j = 0; j <= SecondStr.Length; j++)
                {
                    Console.Out.Write(matrix[i, j] + "\t");
                }
                Console.Out.WriteLine();
            }
        }
        
        /// <summary>
        /// Initiate the matrix with zero value.
        /// </summary>
        private void InitiateMatrix()
        {
            //  fill row
            for (int i = 0; i <= FirstStr.Length; i++)
            {
                //  fill cell
                for (int j = 0; j <= SecondStr.Length; j++)
                {
                    matrix[i, j] = new MatrixCell(ZERO, 0);
                }
            }
        }
        /// <summary>
        /// Calculate the sameness between two
        /// chunk of string.
        /// </summary>
        /// <param name="i">Index of first string. 0 if up.</param>
        /// <param name="j">Index of second string. 0 if left.</param>
        /// <returns>the result from calculation</returns>
        private double CalculateSimilarity(int i, int j)
        {
            //  if gap, return GAP
            if (i == 0 || j == 0)
                return GAP;
            //  return if same or not
            return FirstStr[i - 1].Equals(SecondStr[j - 1]) ? MATCH : MISS;
        }
        /// <summary>
        /// Fill the matrix with values from
        /// comparison.
        /// </summary>
        public void CalculateScore()
        {
            // Vars
            int i = 0;
            int j = 0;
            //  Check if length = 0
            if (FirstStr.Length == 0 || SecondStr.Length == 0)
                return;
            //MaxGain = ((FirstStr.Length + SecondStr.Length) / 2) * MATCH;
            MaxGain = FirstStr.Length * MATCH;
            //  Create the matrix
            InitiateMatrix();
            //  fill row
            for (i = 1; i <= FirstStr.Length; i++)
            {
                //  fill cell
                for (j = 1; j <= SecondStr.Length; j++)
                {
                    double sim = CalculateSimilarity(i, j);
                    //  count score possible
                    double d = matrix[i - 1, j - 1].Value + sim;
                    //  fill with deliberately set GAP value
                    double u = matrix[i - 1, j].Value + CalculateSimilarity(0, j);
                    double l = matrix[i, j - 1].Value + CalculateSimilarity(i, 0);
                    //  select max
                    double used = Math.Max(d,
                        Math.Max(u,
                        Math.Max(l, 0)));
                    //  change the max score
                    if (MaxScore < used)
                    {
                        MaxScore = used;
                        MaxRow = i;
                        MaxCol = j;
                    }
                    matrix[i, j].Value = used;
                    //  fill the direction for trace back
                    if (used == d)
                        if (sim == MISS)
                            matrix[i, j].Direction = D_MISS;
                        else
                            matrix[i, j].Direction = DIAGONAL;
                    else if (used == u)
                        matrix[i, j].Direction = UP;
                    else if (used == l)
                        matrix[i, j].Direction = LEFT;
                    else
                        matrix[i, j].Direction = ZERO;
                }
            }
            //  do trace back
        }
        /// <summary>
        /// Trace back the matrix to get the similar sequence.
        /// </summary>
        public List<Koordinat> TraceBack()
        {
            List<Koordinat> letakSama = new List<Koordinat>();
            //  loop till 0
            do
            {
                //  fill the result array
                if (matrix[MaxRow, MaxCol].Direction.Equals(DIAGONAL))
                {
                    letakSama.Add(new Koordinat(MaxRow - 1, MaxCol - 1));
                    Result1.Add(FirstStr[MaxRow - 1]);
                    Result2.Add(SecondStr[MaxCol - 1]);
                    MaxRow--;
                    MaxCol--;
                    continue;
                }
                if (matrix[MaxRow, MaxCol].Direction.Equals(D_MISS))
                {
                    Result1.Add(FirstStr[MaxRow - 1]);
                    Result2.Add(SecondStr[MaxCol - 1]);
                    MaxRow--;
                    MaxCol--;
                    continue;
                }
                if (matrix[MaxRow, MaxCol].Direction.Equals(LEFT))
                {
                    Result1.Add("-");
                    Result2.Add(SecondStr[MaxCol - 1]);
                    MaxCol--;
                    continue;
                }
                if (matrix[MaxRow, MaxCol].Direction.Equals(UP))
                {
                    Result1.Add(FirstStr[MaxRow - 1]);
                    Result2.Add("-");
                    MaxRow--;
                    continue;
                }
                if (matrix[MaxRow, MaxCol].Direction.Equals(ZERO))
                {
                    break;
                }
            } while (MaxRow != 0 || MaxCol != 0);
            //  Reverse the table so it could look like normal :)
            Result1.Reverse();
            Result2.Reverse();
            letakSama.Reverse();
            return letakSama;
        }
    }
}
