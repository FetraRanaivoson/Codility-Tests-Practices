﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> unsorted = new List<int>() { 0, 4, 3, -1, 3, 78, 5, 6, 2, -128, -44, 36, -4, -300 };

            //unsorted.Sort();
            //Console.WriteLine("Sorted array using built in tool : {0}", string.Join(", ",unsorted)); 

            //Console.WriteLine("Bubble sorted array : {0}", string.Join(", ", BubbleSort(unsorted)));
            //Console.WriteLine("Bubble sorted array inverse : {0}", string.Join(", ", BubbleSortInverse(unsorted)));

            //Console.WriteLine("Selection sorted array : {0}", string.Join(", ", SelectionSort(unsorted)));
            //Console.WriteLine("Selection sorted (clean) array : {0}", string.Join(", ", SelectionSortClean(unsorted)));

            //Console.WriteLine("Insertion sorted array : {0}", string.Join(", ", InsertionSort(unsorted)));
            
            Console.WriteLine("Merge sorted array: {0}", string.Join(", ", MergeSort(unsorted)));

        }



        /// <summary>
        ///  Merge Sort always have an O(n log n) time complexity in all scenarios (best to worst case)
        ///  as opposed to Quick sort which can have a O(n^2) time complexity if you are not using a good pivot.
        ///  Also, Merge Sort is stable in as sense that if you have two identical items, they will be sorted
        ///  in order as they were before.
        ///  In terms of space complexity, Merge sort takes a bit more memory at O(n)
        ///  as opposed to Quick Sort with a O(n log n) space complexity.
        /// </summary>
        private static List<int> MergeSort(List<int> unsorted)
        {
            if(unsorted.Count == 1)
            {
                return unsorted;
            }

            //  Split list into left and right
            List<int> tempLeft = new List<int>(unsorted);
            tempLeft.RemoveRange(unsorted.Count / 2, unsorted.Count - unsorted.Count / 2);
            List<int> left = tempLeft;

            List<int> tempRight= new List<int>(unsorted);
            tempRight.RemoveRange(0, unsorted.Count / 2);
            List<int> right = tempRight;

            //Console.WriteLine("Left: " + string.Join(",", left));
            //Console.WriteLine("Right: " + string.Join(",", right));
            //Console.WriteLine("--------------------");

            return Merge(MergeSort(left), MergeSort(right));
        }

        private static List<int> Merge(List<int> left, List<int> right)
        {
            List<int> mergeResult = new List<int>(); 
            int leftIndex = 0;
            int rightIndex = 0;

            while(leftIndex < left.Count && rightIndex < right.Count)
            {
                if(left[leftIndex]< right[rightIndex])
                {
                    mergeResult.Add(left[leftIndex]);
                    leftIndex++;
                }
                else
                {
                    mergeResult.Add(right[rightIndex]);
                    rightIndex++;
                }
            }
            //Remove unecessary items because they have already been added to the result
            right.RemoveRange(0, rightIndex);
            left.RemoveRange(0, leftIndex);

            mergeResult.AddRange(left);
            mergeResult.AddRange(right);

            return mergeResult;
        }


        /// <summary>
        /// Insertion sort is mostly used when you have a small amount of inputs to be sorted
        /// although it has O(n^2) time complexity.
        /// It is very performant in memory with O(1) space complexity
        /// It is easy to code
        /// </summary>
        private static List<int> InsertionSort(List<int> unsorted)
        {
            for (int i = 0; i < unsorted.Count; i++)
            {
                if (i > 0)
                {
                    for (int j = i; j > 0; j--) // Retro check starting from index j=i back to index 0
                    {
                        if (unsorted[j] < unsorted[j-1])
                        {
                            int temp = unsorted[j-1];
                            unsorted[j - 1] = unsorted[j];
                            unsorted[j] = temp;
                        }
                    }
                }
            }
            return unsorted;
        }

        /// <summary>
        /// With a O(n^2) space complexity, you mostly won't use Selection Sort in real life
        /// </summary>
        private static List<int> SelectionSort(List<int> unsorted)
        {
            //  RED_INDEX = THE INDEX OF THE "SMALLER" VALUE (We suppose that the first index is the smaller value)
            //  BLUE_INDEX = THE INDEX OF THE VALUE NEXT TO THE RED INDEX (RED INDEX++)
            //  The idea is to find the SMALLEST VALUE after the RED_INDEX and INSERT it back at the beginning or after all
            //  the smaller values at the beginning

            int insertionIndex = 0;

            for (int RED_INDEX = 0; RED_INDEX < unsorted.Count; RED_INDEX++)
            {
                //  Always check from RED_INDEX up
                for (int BLUE_INDEX = RED_INDEX + 1; BLUE_INDEX < unsorted.Count; BLUE_INDEX++)
                {
                    //  As soon as current value > next value, break this so that on the next outer iteration,
                    //  the RED_INDEX WILL BE THE BLUE INDEX
                    if (unsorted[RED_INDEX] > unsorted[BLUE_INDEX])
                    {
                        //Technically RED_INDEX = BLUE_INDEX but because the outer for loop will increment, we need to counter that!
                        RED_INDEX = BLUE_INDEX - 1;
                        break;
                    }

                    //  Else if current value < next, we need to BUBBLE UP OUR COMPARISON TILL THE END
                    else if (unsorted[RED_INDEX] < unsorted[BLUE_INDEX])
                    {
                        //  If we found out that the value on the RED_INDEX is SMALLER than all the others starting from [BLUE_INDEX +1]
                        if (BLUE_INDEX == unsorted.Count - 1)
                        {
                            //  INSERT that element at the beginning of the list
                            int toBubbleUp = unsorted[RED_INDEX];
                            unsorted.RemoveAt(RED_INDEX);

                            //  Insertion is after each smaller ones. We insert at 0 by default and increment the insertion index as we insert elements
                            unsorted.Insert(insertionIndex, toBubbleUp);

                            //  And the next RED_INDEX (default smaller value) will be at AT [INSERTION INDEX + 1]
                            RED_INDEX = insertionIndex; //Techincally [INSERTION INDEX + 1] but this will increment by 1 on the next outer loop!

                            //  Finally increment for the next INSERTION INDEX
                            insertionIndex++;
                        }
                    }
                }

                //  Particular case when we are comparing the last 2 values and the last element is less than the last element-1
                if (RED_INDEX == unsorted.Count - 2 && unsorted[RED_INDEX] > unsorted[RED_INDEX + 1])
                {
                    int toBubbleUp = unsorted[RED_INDEX + 1];
                    unsorted.RemoveAt(RED_INDEX + 1);
                    unsorted.Insert(insertionIndex, toBubbleUp);
                    insertionIndex++;
                    RED_INDEX = insertionIndex - 1;
                }
            }

            return unsorted;
        }
        private static List<int> SelectionSortClean(List<int> unsorted)
        {
            int listCount = unsorted.Count;
            for (int RED_INDEX = 0; RED_INDEX < listCount; RED_INDEX++)
            {
                //  Set the current index as minimum
                int minRedIndex = RED_INDEX;
                //  The element to swap for the minimum found
                int temp = unsorted[RED_INDEX];
                for (int BLUE_INDEX = RED_INDEX + 1; BLUE_INDEX < listCount; BLUE_INDEX++)
                {
                    //  Remember, we need to find the smallest value after the min(RED_INDEX: i) and
                    //  if we find it, the next min (RED_INDEX) will be the older (BLUE_INDEX)
                    if (unsorted[BLUE_INDEX] < unsorted[minRedIndex])
                    {
                        //  Update minimum if current is lower that what we had previously (Where indew are we? What index is the minimum?)
                        minRedIndex = BLUE_INDEX;
                    }
                }
                //  Now after finding the min INDEX between ALL the  elements, SWAP the min INDEX value and the element where to place it (temp)
                unsorted[RED_INDEX] = unsorted[minRedIndex];
                //  And the  value of where the min was before is the temp 
                unsorted[minRedIndex] = temp;
            }
            return unsorted;
        }


        /// <summary>
        /// With a O(n^2) space complexity, you mostly won't use Bubble Sort in real life
        /// </summary>
        private static List<int> BubbleSort(List<int> unsorted)
        {
            for (int i = 0; i < unsorted.Count; i++)
            {
                for (int k = 0; k < unsorted.Count; k++)
                {
                    if (k != unsorted.Count - 1)
                    {
                        int first = unsorted[k];  //this is the current item
                        int second = unsorted[k + 1];
                        if (first > second)
                        {
                            unsorted[k] = second;
                            unsorted[k + 1] = first;   //the current item is moving
                        }
                    }
                }//At the end of this loop, we should have resolve the case of 'first'which is always moving (bubbling)
            }//At the end of this should resolve all cases
            return unsorted;
        }
        /// <summary>
        /// With a O(n^2) space complexity, you mostly won't use Bubble Sort in real life
        /// </summary>
        private static List<int> BubbleSortInverse(List<int> unsorted)
        {
            for (int i = 0; i < unsorted.Count; i++)
            {
                for (int k = 0; k < unsorted.Count; k++)
                {
                    if (k != unsorted.Count - 1)
                    {
                        int first = unsorted[k];  //this is the current item
                        int second = unsorted[k + 1];
                        if (first < second)
                        {
                            unsorted[k] = second;
                            unsorted[k + 1] = first;   //the current item is moving because it becomes in k+1 index and so we always test for it in the k for loop
                        }
                    }
                }//At the end of this loop, we should have resolve the case of 'first'which is always moving (bubbling)
            }//At the end of this should resolve all cases
            return unsorted;
        }
    }
}
