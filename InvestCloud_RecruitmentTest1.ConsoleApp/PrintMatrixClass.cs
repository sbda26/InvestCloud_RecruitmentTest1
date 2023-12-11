using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestCloud_RecruitmentTest1.ConsoleApp
{
    internal class PrintMatrixClass
    {
        public static void Go(int size, int?[,] matrix)
        {
            Console.WriteLine("---------------------------------------------");
            string output = ConvertToString2(size, matrix);
            Console.WriteLine(output);
        }

        private static string ConvertToString2(int size, int?[,] matrix)
        {
            StringBuilder output = new();
            int limit = size - 1;

            for (int row = 0; row <= limit; row++)
            {
                for (int col = 0; col <= limit; col++)
                {
                    output.Append($"{matrix[row, col]}");
                    if (col < limit)
                        output.Append(", ");
                    else
                        output.AppendLine();
                }
            }

            return output.ToString();
        }


    }
}
