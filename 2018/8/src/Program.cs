using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = args[0];
            List<int> treeDescription = File
                .ReadAllText(fileName)
                .Split(' ')
                .Select(x => int.Parse(x))
                .ToList();

            Console.WriteLine(GetSumOfTreeMetadata(treeDescription.GetEnumerator()));
            Console.WriteLine(GetTreeValue(treeDescription.GetEnumerator()));
        }

        static int GetSumOfTreeMetadata(IEnumerator<int> treeDescriptionEnumerator)
        {
            var calculator = new MetadataSumTreeValueCalculator();
            return calculator.CalculateValue(treeDescriptionEnumerator);
        }

        static int GetTreeValue(IEnumerator<int> treeDescriptionEnumerator)
        {
            var calculator = new ComplexTreeValueCalculator();
            return calculator.CalculateValue(treeDescriptionEnumerator);            
        }
    }
}
