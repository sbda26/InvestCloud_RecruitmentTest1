using System.Text;

namespace InvestCloud_RecruitmentTest1.ConsoleApp
{
    internal class Program
    {
        private const string _urlRoot = "https://recruitment-test.investcloud.com/api/numbers";
        private const int _size = 1000;

        static async Task Main(string[] args)
        {
            int?[,] result = await MatrixClass.GetProductOfMatrices(_urlRoot, _size);
            
            await SendResultsClass.Go(_urlRoot, _size, result);
        }
    }
}
