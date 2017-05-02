using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixRotator
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Test(false);
            foreach (var result in test.Do())
                Console.WriteLine(result ? "Passed" : "Error");

            Console.ReadKey();
        }
    }
}
