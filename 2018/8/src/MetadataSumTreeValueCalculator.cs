using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace src
{
    public class MetadataSumTreeValueCalculator : AbstractTreeValueCalculator
    {
        protected override int GetResult(int[] childValues, int[] metadataValues)
        {
            return childValues.Sum() + metadataValues.Sum();
        }
    }
}