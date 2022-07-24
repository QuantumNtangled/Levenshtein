using System;

namespace LeivenshteinDistance
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter in 2 values: /r");

            Console.WriteLine("First Word:  ");
            string firstvalue = Console.ReadLine().ToLower();

            Console.WriteLine("Second Word:  ");
            string secondvalue = Console.ReadLine().ToLower();

            //instantiate the Levenshtein algorithm
            LevenshteinDistance calculate = new LevenshteinDistance();
            //get the character differences from the levenshtein algorithm
            int results = calculate.Calculate(firstvalue, secondvalue);
            //divide the differences
            decimal difference = calculate.Percentage(results, secondvalue.Length);
            //get readable percentage
            Decimal finalpercentage = difference * 100;

            Console.WriteLine("Percentage confidence is: " + finalpercentage);
        }
        public class LevenshteinDistance
        {
            /// <summary>
            ///     Calculate the difference between 2 strings using the Levenshtein distance algorithm
            /// </summary>
            /// <param name="source1">First string</param>
            /// <param name="source2">Second string</param>
            /// <returns></returns>
            public int Calculate(string source1, string source2) //O(n*m)
            {
                var source1Length = source1.Length;
                var source2Length = source2.Length;

                var matrix = new int[source1Length + 1, source2Length + 1];

                // First calculation, if one entry is empty return full length
                if (source1Length == 0)
                    return source2Length;

                if (source2Length == 0)
                    return source1Length;

                // Initialization of matrix with row size source1Length and columns size source2Length
                for (var i = 0; i <= source1Length; matrix[i, 0] = i++) { }
                for (var j = 0; j <= source2Length; matrix[0, j] = j++) { }

                // Calculate rows and collumns distances
                for (var i = 1; i <= source1Length; i++)
                {
                    for (var j = 1; j <= source2Length; j++)
                    {
                        var cost = (source2[j - 1] == source1[i - 1]) ? 0 : 1;

                        matrix[i, j] = Math.Min(
                            Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                            matrix[i - 1, j - 1] + cost);
                    }
                }
                // return result
                return matrix[source1Length, source2Length];
            }
            public decimal Percentage(int distance, int secondwordlength)
            {
                int difference = secondwordlength - distance;
                decimal percentage = (decimal)difference / secondwordlength;
                return percentage;
            }
        }
       
    }
}
