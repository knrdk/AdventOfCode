using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace src
{
    public class ComplexTreeValueCalculator : AbstractTreeValueCalculator
    {
        protected override int GetResult(int[] childValues, int[] metadataValues)
        {
            IEnumerable<int> resolvedMetadata = childValues.Any()
                ? metadataValues
                    .Select(x => x - 1)
                    .Select(x => x < childValues.Length ? childValues[x] : 0)
                : metadataValues;

            return resolvedMetadata.Sum();
        }
    }
}