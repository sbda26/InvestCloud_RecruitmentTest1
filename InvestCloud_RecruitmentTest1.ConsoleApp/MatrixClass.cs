using Flurl.Http;
using System.Threading.Tasks;

namespace InvestCloud_RecruitmentTest1.ConsoleApp
{
    internal class MatrixClass
    {
        public static async Task<int?[,]> GetProductOfMatrices(string urlRoot, int size)
        {
            await Init(urlRoot, size);

            int?[,] matrixA = await GetMatrix(urlRoot, size, "A");
            int?[,] matrixB = await GetMatrix(urlRoot, size, "B");
            int?[,] result = ComputeMatrices(size, matrixA, matrixB);

            return result;
        }

// =======================================================================================================================================================================================================

        private static int?[,] ComputeMatrices(int size, int?[,] matrixA, int?[,] matrixB)
        {
            var result = new int?[size, size];

            for (int resultRow = 0; resultRow < size; resultRow++)
            {
                for (int resultCol = 0; resultCol < size; resultCol++)
                {
                    result[resultRow, resultCol] = 0;
                    for (int index = 0; index < size; index++)
                        result[resultRow, resultCol] += matrixA[resultRow, index] * matrixB[index, resultCol];
                }
            }

            return result;
        }

        private static async Task<int?[,]> GetMatrix(string urlRoot, int size, string matrixID)
        {
            var matrix = new int?[size, size];

            for (int row = 0; row < size; row++)
            {
                int[]? rowValues = await GetRowValues(urlRoot, matrixID, row);
                for (int col = 0; col < size; col++)
                    matrix[row, col] = rowValues?[col];
            }

            return matrix;
        }

        private static async Task<int[]?> GetRowValues(string urlRoot, string matrixID, int row)
        {
            string url = $"{urlRoot}/{matrixID}/row/{row}";
            var response = await url.GetJsonAsync<InvestCloudApiClass<int[]?>>();

            if (response.Success == false)
                throw new Exception($"Error on row #{row}. Cause: {response.Cause}");
            else
                return response.Value;
        }

        private static async Task Init(string urlRoot, int size)
        {
            var response = await $"{urlRoot}/init/{size}".GetJsonAsync<InvestCloudApiClass<int?>>();
            var success = (bool?)response.Success;

            if (success != true)
                throw new Exception($"Matrix initialization with size {size} failed. Cause: {response?.ToString()}");
        }
    }
}
