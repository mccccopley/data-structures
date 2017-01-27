using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.sorts
{
    public class HeapSorterAlt<ArrayType> : ISorter<ArrayType> where ArrayType : IComparable<ArrayType>
    {
        public void Sort(ArrayType[] unsortedArray)
        {
            MakeIntoHeap(unsortedArray);
            SortFromHeap(unsortedArray);
        }

        private void SwapElements(ArrayType[] array, int index1, int index2)
        {
            ArrayType tmp = array[index1];
            array[index1] = array[index2];
            array[index2] = tmp;
        }

        private void SortFromHeap(ArrayType[] array)
        {
            int topIndex = 0;
            int lastIndex = array.Length - 1;

            while (topIndex < lastIndex)
            {
                SwapElements(array, topIndex, lastIndex);
                lastIndex--;
                FilterTopElementToBottom(array, lastIndex);
            }
        }

        private void MakeIntoHeap(ArrayType[] array)
        {
            int topIndex = 1;
            int lastIndex = array.Length - 1;

            while (topIndex <= lastIndex)
            {
                BubbleBottomElementToTop(array, topIndex);
                topIndex++;
            }
        }

        private int GetLeftChildIndex(int parentIndex)
        {
            return (parentIndex * 2) + 1;
        }

        private int GetRightChildIndex(int parentIndex)
        {
            return (parentIndex * 2) + 2;
        }

        private int GetParentIndex(int childIndex)
        {
            return ((childIndex + 1) / 2) - 1;
        }

        private void BubbleBottomElementToTop(ArrayType[] array, int lastIndex)
        {
            ArrayType bottomValue = array[lastIndex];

            while (lastIndex > 0)
            {
                int parentIndex = GetParentIndex(lastIndex);
                ArrayType parentValue = array[parentIndex];
                int parentCompareResult = parentValue.CompareTo(bottomValue);

                if (parentCompareResult < 0)
                {
                    SwapElements(array, parentIndex, lastIndex);
                    lastIndex = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        private void FilterTopElementToBottom(ArrayType[] array, int lastIndex)
        {
            int topIndex = 0;

            while (topIndex < lastIndex)
            {
                int newIndex = topIndex;

                int leftChildIndex = GetLeftChildIndex(topIndex);
                if (leftChildIndex <= lastIndex)
                {
                    ArrayType leftChildValue = array[leftChildIndex];
                    int leftCompareResult = leftChildValue.CompareTo(array[newIndex]);

                    if (leftCompareResult > 0)
                    {
                        newIndex = leftChildIndex;
                    }
                }

                int rightChildIndex = GetRightChildIndex(topIndex);
                if (rightChildIndex <= lastIndex)
                {
                    ArrayType rightChildValue = array[rightChildIndex];
                    int rightCompareResult = rightChildValue.CompareTo(array[newIndex]);

                    if (rightCompareResult > 0)
                    {
                        newIndex = rightChildIndex;
                    }
                }

                if (topIndex != newIndex)
                {
                    SwapElements(array, newIndex, topIndex);
                    topIndex = newIndex;
                }
                else
                {
                    break;
                }
            }
        }
    }

    public class IntHeapSorterAlt : HeapSorterAlt<int> { }
}
