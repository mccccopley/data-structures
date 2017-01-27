using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.searches
{
    public class Constants
    {
        public const int NotFound = -1;
    }

    interface ISearcher<ArrayType> where ArrayType : IComparable<ArrayType>
    {
        int Search(ArrayType[] sortedArray, ArrayType element);
    }
}
