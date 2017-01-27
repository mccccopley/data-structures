using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.sorts
{
    public class MergeSorter<ArrayType> : ISorter<ArrayType> where ArrayType : IComparable<ArrayType>
    {
        public void Sort(ArrayType[] unsortedArray)
        {
            ArrayType[] helperArray = new ArrayType[unsortedArray.Length];
            SortSubArray(unsortedArray, helperArray, 0, unsortedArray.Length - 1);
        }

        private void SortSubArray(ArrayType[] array, ArrayType[] helperStorage, int startIndex, int endIndex)
        {
            int middleIndex = (startIndex + endIndex + 1) / 2;

            if (startIndex < (middleIndex - 1))
            {
                SortSubArray(array, helperStorage, startIndex, middleIndex - 1);
            }

            if (middleIndex < endIndex)
            {
                SortSubArray(array, helperStorage, middleIndex, endIndex);
            }

            MergeSubArrays(array, helperStorage, startIndex, middleIndex, endIndex);
        }

        private void MergeSubArrays(ArrayType[] array, ArrayType[] helperStorage, int startIndex, int middleIndex, int endIndex)
        {
            int helperStorageIndex = 0;
            int index1 = startIndex;
            int index2 = middleIndex;

            while (index1 < middleIndex || index2 <= endIndex)
            {
                if (index1 == middleIndex)
                {
                    helperStorage[helperStorageIndex] = array[index2];
                    index2++;
                }
                else if (index2 > endIndex)
                {
                    helperStorage[helperStorageIndex] = array[index1];
                    index1++;
                }
                else
                {
                    int compareResult = array[index1].CompareTo(array[index2]);

                    if (compareResult < 0)
                    {
                        helperStorage[helperStorageIndex] = array[index1];
                        index1++;
                    }
                    else
                    {
                        helperStorage[helperStorageIndex] = array[index2];
                        index2++;
                    }
                }

                helperStorageIndex++;
            }

            for (int index = startIndex; index <= endIndex; index++)
            {
                array[index] = helperStorage[index - startIndex];
            }
        }
    }

    public class IntMergeSorter : MergeSorter<int> {}
}
