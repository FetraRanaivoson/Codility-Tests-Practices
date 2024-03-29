﻿using System;
using System.Collections.Generic;

namespace Reverse_String
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Hello, my name is Fetra. I am a game developer";
            Console.WriteLine(ReverseLoop(text));
            Console.WriteLine(ReverseSimplified(text));
            Console.WriteLine(ReverseRecursion(text));

        }

        private static string ReverseRecursion(string text)
        {
            //  Base case: stop
            if (text.Length <= 1)
            {
                return text;
            }

            //  Recursive case: recall => if (text.Length != 0)
            char[] chrs = text.ToCharArray();
            LinkedList<char> charactersList = new LinkedList<char>(chrs);
            LinkedListNode<char> lastChar = charactersList.Last;
            LinkedListNode<char> firstChar = charactersList.First;
            if (charactersList.Count > 1)
            {
                charactersList.RemoveLast();
                charactersList.RemoveFirst();

            }
            return lastChar.Value + ReverseRecursion(string.Join("", charactersList)) + firstChar.Value;

            //            "H +   eli    + o    "
            //            "o + e +l + i + H    "
            //            "o + i +l + e + H    "
        }

        static string ReverseSimplified(string text)
        {
            char[] reversed = text.ToCharArray();
            Array.Reverse(reversed);
            return string.Join("", reversed);
            //return string.Join("", text.Split(""));
        }

        static string ReverseLoop(string text)
        {
            //  I do not need the extra loop to create a separate array to hold the characters.
            //  string is already an array but I just need to convert it into string when assigning to the new array
            //  It is just a minor optimization because O(n+n) <====> 0(n)

            //  Transform the text to an array
            //string[] oldArray = new string[text.Length];
            //for (int i = 0; i < oldArray.Length; i++) // O(n)
            //{
            //    oldArray[i] = text[i].ToString();
            //    //Console.WriteLine(oldArray[i]);
            //}

            //  text = "abc"
            //  1-  You create a new array with the same length as the array we are working on
            string[] newArray = new string[text.Length];

            //  2-  Assign new values for the new array starting from old array index going backward from the end
            //  int currentOldArrayIndex = oldArray.Length -1
            //      For each item in the new array (increment loop)
            //          item[indexForward] = oldArray[currentOldArrayIndex]
            //          if(currentOldArrayIndex) >= 0)
            //              currentOldArrayIndex -- 
            //          
            //  OldArr = "a", "b", "c"
            //  NArr = "c", "b", "a"

            //int currentOldArrayIndex = oldArray.Length - 1;
            int currentOldArrayIndex = text.Length - 1;

            for (int i = 0; i < text.Length; i++) // O(n)
            {
                //newArray[i] = oldArray[currentOldArrayIndex];
                newArray[i] = text[currentOldArrayIndex].ToString();
                if (currentOldArrayIndex >= 0)
                {
                    currentOldArrayIndex--;
                }
            }

            //for (int i = 0; i < newArray.Length; i++)
            //{
            //    Console.WriteLine(newArray[i]);
            //}

            //  3-  Transform the array to a text
            return string.Join("", newArray);
        }


    }
}
