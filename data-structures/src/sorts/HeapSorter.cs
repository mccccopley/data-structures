using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.sorts
{
    public class HeapSorter<ArrayType> : ISorter<ArrayType> where ArrayType : IComparable<ArrayType>
    {
        public void Sort(ArrayType[] unsortedArray)
        {
            MakeHeap(unsortedArray);

            int endIndex = unsortedArray.Length - 1;

            while (endIndex > 0)
            {
                Swap(unsortedArray, 0, endIndex);

                endIndex--;

                MaintainHeapWithinRange(unsortedArray, 0, endIndex);
            }
        }

        private void MakeHeap(ArrayType[] array)
        {
            int count = array.Length;
            int startIndex = (count - 2) / 2;

            while (startIndex >= 0)
            {
                MaintainHeapWithinRange(array, startIndex, count - 1);

                startIndex--;
            }
        }

        private void MaintainHeapWithinRange(ArrayType[] array, int startIndex, int endIndex)
        {
            int rootIndex = startIndex;
            int leftChildIndex = rootIndex * 2 + 1;

            while (leftChildIndex <= endIndex)
            {
                int indexToSwapWithRoot = rootIndex;

                if (array[indexToSwapWithRoot].CompareTo(array[leftChildIndex]) < 0)
                {
                    indexToSwapWithRoot = leftChildIndex;
                }

                int rightChildIndex = leftChildIndex + 1;

                if (rightChildIndex <= endIndex && array[indexToSwapWithRoot].CompareTo(array[rightChildIndex]) < 0)
                {
                    indexToSwapWithRoot = rightChildIndex;
                }

                if (indexToSwapWithRoot == rootIndex)
                {
                    return;
                }

                Swap(array, rootIndex, indexToSwapWithRoot);

                rootIndex = indexToSwapWithRoot;
                leftChildIndex = rootIndex * 2 + 1;
            }
        }

        private void Swap(ArrayType[] array, int firstIndex, int secondIndex)
        {
            ArrayType temporary = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = temporary;
        }
    }

    public class IntHeapSorter : HeapSorter<int> { }
}
