using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.sorts
{
    interface ISorter<ArrayType> where ArrayType : IComparable<ArrayType>
    {
        void Sort(ArrayType[] unsortedArray);
    }
}
