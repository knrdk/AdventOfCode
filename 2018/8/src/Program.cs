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
        }

        static int GetSumOfTreeMetadata(IEnumerator<int> treeDescriptionEnumerator)
        {
            int result = 0;
            treeDescriptionEnumerator.MoveNext();
            int numberOfSubtrees = treeDescriptionEnumerator.Current;
            treeDescriptionEnumerator.MoveNext();
            int numberOfMetadata = treeDescriptionEnumerator.Current;

            for (int i = 0; i < numberOfSubtrees; i++)
            {
                result += GetSumOfTreeMetadata(treeDescriptionEnumerator);
            }

            for (int i = 0; i < numberOfMetadata; i++)
            {
                treeDescriptionEnumerator.MoveNext();
                result += treeDescriptionEnumerator.Current;
            }

            return result;
        }
    }
}
