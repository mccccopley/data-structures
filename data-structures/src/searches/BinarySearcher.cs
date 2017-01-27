using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.searches
{
    public class BinarySearcher<ArrayType> : ISearcher<ArrayType> where ArrayType : IComparable<ArrayType>
    {
        public int Search(ArrayType[] sortedArray, ArrayType element)
        {
            int count = sortedArray.Length;
            int startIndex = 0;
            int endIndex = count - 1;

            while (startIndex <= endIndex)
            {
                int middleIndex = (startIndex + endIndex) / 2;
                int comparisonResult = element.CompareTo(sortedArray[middleIndex]);

                if (comparisonResult == 0)
                {
                    return middleIndex;
                }
                else if (comparisonResult < 0)
                {
                    endIndex = middleIndex - 1;
                }
                else // comparisonResult > 0
                {
                    startIndex = middleIndex + 1;
                }

                middleIndex = (startIndex + endIndex) / 2;
            }

            return Constants.NotFound;
        }
    }

    public class IntBinarySearcher : BinarySearcher<int> { }
}
