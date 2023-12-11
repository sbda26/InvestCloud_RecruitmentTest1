using Flurl.Http;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InvestCloud_RecruitmentTest1.ConsoleApp
{
    internal class SendResultsClass
    {
        public static async Task Go(string urlRoot, int size, int?[,] result)
        {
            string matrixString = ConvertMatrixToString(size, result);
            string hashed = ConvertToHashString(matrixString);

            await PostToAPI(urlRoot, hashed);
        }

// =======================================================================================================================================================================================================

        private static string ConvertToHashString(string matrixString)
        {
            byte[] matrixBytes = Encoding.ASCII.GetBytes(matrixString);
            byte[] hashBytes = MD5.HashData(matrixBytes);
            string hashString = Convert.ToHexString(hashBytes);

            return hashString;
        }

        private static string ConvertMatrixToString(int size, int?[,] matrix)
        {
            StringBuilder output = new();

            for (int row = 0; row < size; row++)
                for (int col = 0; col < size; col++)
                    output.Append($"{matrix[row, col]}");

            return output.ToString();
        }

        private static async Task PostToAPI(string urlRoot, string hashedOutput)
        {
            var response = await $"{urlRoot}/Validate"
                .SendJsonAsync(HttpMethod.Post, hashedOutput)
                .ReceiveJson<InvestCloudApiClass<string>>();
            
            if (response.Success == false)
                throw new Exception($"Post to API failed. Cause: {response.Cause}");
        }
    }
}
