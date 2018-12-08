using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace src
{
    public abstract class AbstractTreeValueCalculator
    {
        public int CalculateValue(IEnumerator<int> treeEnumerator)
        {
            (int numberOfSubtrees, int numberOfMetadata) = ParseHeader(treeEnumerator);
            int[] childValues = GetChildValues(treeEnumerator, numberOfSubtrees);
            int[] metadataValues = GetMetadataValues(treeEnumerator, numberOfMetadata);

            return GetResult(childValues, metadataValues);
        }       

        private static (int numberOfSubtrees, int numberOfMetadata) ParseHeader(IEnumerator<int> treeEnumerator)
        {
            int numberOfSubtrees = GetEnumeratorNextValue(treeEnumerator);
            int numberOfMetadata = GetEnumeratorNextValue(treeEnumerator);

            return (numberOfSubtrees, numberOfMetadata);
        }

        private int[] GetChildValues(IEnumerator<int> treeEnumerator, int numberOfSubtrees)
        {
            return GetValues(treeEnumerator, numberOfSubtrees, CalculateValue);
        }

        private static int[] GetMetadataValues(IEnumerator<int> treeEnumerator, int numberOfMetadata)
        {
            return GetValues(treeEnumerator, numberOfMetadata, GetEnumeratorNextValue);
        }

        private static int[] GetValues(IEnumerator<int> treeEnumerator, int size, Func<IEnumerator<int>, int> ValueCalculator)
        {
            int[] values = new int[size];
            for (int i = 0; i < size; i++)
            {
                values[i] = ValueCalculator(treeEnumerator);
            }

            return values;
        }

        private static int GetEnumeratorNextValue(IEnumerator<int> enumerator){
            enumerator.MoveNext();
            return enumerator.Current;
        }

        protected abstract int GetResult(int[] childValues, int[] metadataValues);
    }
}