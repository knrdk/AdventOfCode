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

        static int GetTreeValue(IEnumerator<int> treeDescriptionEnumerator)
        {
            treeDescriptionEnumerator.MoveNext();
            int numberOfSubtrees = treeDescriptionEnumerator.Current;
            treeDescriptionEnumerator.MoveNext();
            int numberOfMetadata = treeDescriptionEnumerator.Current;

            int[] childValues = new int[numberOfSubtrees];
            for (int i = 0; i < numberOfSubtrees; i++)
            {
                childValues[i] = GetTreeValue(treeDescriptionEnumerator);
            }

            int[] metadataValues = new int[numberOfMetadata];
            for (int i = 0; i < numberOfMetadata; i++)
            {
                treeDescriptionEnumerator.MoveNext();
                metadataValues[i] = treeDescriptionEnumerator.Current;
            }

            if (numberOfSubtrees == 0)
            {                
                return metadataValues.Sum();
            }
            
            int result = 0;
            foreach (var metadata in metadataValues)
            {                
                result += metadata-1 < numberOfSubtrees ? childValues[metadata-1] : 0;
            }
            return result;
        }
    }
}
