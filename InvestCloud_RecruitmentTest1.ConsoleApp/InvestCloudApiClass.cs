using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestCloud_RecruitmentTest1.ConsoleApp
{
    internal class InvestCloudApiClass<T>
    {
        public T? Value { get; set; }
        public string? Cause { get; set; }
        public bool? Success { get; set; }
    }
}
