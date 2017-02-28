using data_structures.src.minesweeper;
using InterviewPrep.searches;
using InterviewPrep.sorts;
using InterviewPrep.structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep
{
    class Program
    {
        static void Main(string[] args)
        {
            Random randomNumberGenerator = new Random(1234);

            int randomNumberCount = 10;
            int randomNumberSearchIndex = 8;

            // test out minesweeper

            TestMinesweeper();
            return;

            // make a test array of numbers

            int[] randomIntegers = new int[randomNumberCount];
            int[] randomIntegersAlt = new int[randomNumberCount];
            for (int randomNumberIndex = 0; randomNumberIndex < randomNumberCount; ++randomNumberIndex)
            {
                randomIntegers[randomNumberIndex] = randomNumberGenerator.Next() % 100;
                randomIntegersAlt[randomNumberIndex] = randomIntegers[randomNumberIndex];
            }

            // heap sorter test

            IntHeapSorter integerHeapSorter = new IntHeapSorter();
            integerHeapSorter.Sort(randomIntegers);

            //IntHeapSorterAlt integerHeapSorterAlt = new IntHeapSorterAlt();
            IntMergeSorter integerHeapSorterAlt = new IntMergeSorter();
            integerHeapSorterAlt.Sort(randomIntegersAlt);

            // binary searcher test

            IntBinarySearcher integerBinarySearcher = new IntBinarySearcher();
            int foundIndex = integerBinarySearcher.Search(randomIntegers, randomIntegers[randomNumberSearchIndex]);

            // hash table test

            IntPairHashTable integerHashTable = new IntPairHashTable();
            for (int randomNumberIndex = 0; randomNumberIndex < randomNumberCount; ++randomNumberIndex)
            {
                integerHashTable.Add(randomIntegers[randomNumberIndex], randomNumberIndex);
            }

            for (int randomNumberIndex = 0; randomNumberIndex < randomNumberCount; ++randomNumberIndex)
            {
                int randomNumberValue;
                integerHashTable.Get(randomIntegers[randomNumberIndex], out randomNumberValue);
            }

            randomNumberSearchIndex = randomNumberCount - 1;
            while (randomNumberSearchIndex >= 0)
            {
                integerHashTable.Remove(randomIntegers[randomNumberSearchIndex]);

                for (int randomNumberIndex = 0; randomNumberIndex < randomNumberSearchIndex; ++randomNumberIndex)
                {
                    int randomNumberValue;
                    integerHashTable.Get(randomIntegers[randomNumberIndex], out randomNumberValue);
                }

                randomNumberSearchIndex--;
            }

            // binary tree set test

            IntBinaryTreeSet integerBinaryTreeSet = new IntBinaryTreeSet();
            for (int randomNumberIndex = 0; randomNumberIndex < randomNumberCount; ++randomNumberIndex)
            {
                integerBinaryTreeSet.AddElement(randomIntegers[randomNumberIndex]);
            }

            integerBinaryTreeSet.Output();

            integerBinaryTreeSet.Serialize("test_binary_tree_set.txt");
            integerBinaryTreeSet.Deserialize("test_binary_tree_set.txt");

            integerBinaryTreeSet.Output();
        }

        static void TestMinesweeper()
        {
            BoardGenerator boardGenerator = new BoardGenerator();
            Board board = boardGenerator.Generate(10, 10, 10);
            List<String> boardDisplay = new List<String>();
            board.DisplayBoard(boardDisplay);
            ShowBoardDisplay(boardDisplay);
            throw new Exception("END");
        }

        static void ShowBoardDisplay(IList<String> cellRows)
        {
            foreach (String cellRow in cellRows)
            {
                Console.WriteLine(cellRow);
            }
        }
    }
}
