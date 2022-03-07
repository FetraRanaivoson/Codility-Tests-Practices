﻿using System;

namespace Binary_Tree
{
    /// <summary>
    /// A node prototype for binary trees
    /// </summary>
    class Node
    {
        public int value;
        public Node left;
        public Node right;
        public bool HasNoChild => this.left == null && this.right == null;
        public bool Full => this.left != null && this.right != null;
        public bool IsLeftNull => this.left == null;
        public bool isRightNull => this.right == null;
        public Node(int value)
        {
            this.value = value;
            this.left = null;
            this.right = null;
        }

    }

    /// <summary>
    /// Binary Search Tree class
    /// </summary>
    class BinarySearchTree
    {
        Node root;

        public BinarySearchTree()
        {
            this.root = null;
        }

        public void Insert(int value)
        {
            Traverse(ref this.root, value);
        }

        private static void Traverse(ref Node currentNode, int value)
        {
            //  Traversing current node
            if (currentNode == null)
            {
                currentNode = new Node(value);
            }
            else // Can be done with a while true loop
            {
                if (value < currentNode.value && currentNode.IsLeftNull)
                {
                    //assign
                    currentNode.left = new Node(value);
                }
                else if (value > currentNode.value && currentNode.isRightNull)
                {
                    //assign
                    currentNode.right = new Node(value);
                }

                else
                {
                    //  NODE HAS 2 CHILDREN === WE NEED TO DIVE IN
                    if (currentNode.Full)
                    {
                        if (value < currentNode.left.value)
                        {
                            Traverse(ref currentNode.left, value);
                        }
                        else
                        {
                            Traverse(ref currentNode.right, value);
                        }
                    }
                    //  EITHER ONE HAS 1 CHILD
                    else
                    {
                        if (currentNode.left != null)
                        {
                            if (value < currentNode.left.value)
                                Traverse(ref currentNode.left.left, value);
                            else
                            {
                                Traverse(ref currentNode.left.right, value);
                            }
                        }
                        else if (currentNode.right != null)
                        {
                            if (value < currentNode.right.value)
                                Traverse(ref currentNode.right.right, value);
                            else
                            {
                                Traverse(ref currentNode.right.left, value);
                            }
                        }
                    }
                }
            }
        }

        public int Lookup(int value)
        {
            if(this.root == null)
            {
                Console.WriteLine("Cannot find {0}, tree has not root.", value);
                return -1;
            }
            Node currentNode = this.root;
            while (true)
            {
                if(currentNode == null)//At the end of a node (left/right are null)
                {
                    Console.WriteLine("Entry {0} not found!", value);
                    return -1;
                }
                if (value == currentNode.value)
                {
                    Console.WriteLine("Entry {0} found!", value);
                    return currentNode.value;
                }
                else
                {
                    if (value < currentNode.value)
                    {
                        currentNode = currentNode.left;
                    }
                    else if (value > currentNode.value)
                    {
                        currentNode = currentNode.right;
                    }
                    //else
                    //{
                    //    Console.WriteLine("Entry {0} not found", value);
                    //    return -1;
                    //}
                }
            }
        }

        public void Print()
        {
            if (this.root == null)
            {
                Console.WriteLine("No tree to print!");
                return;
            }
            Node currentNode = this.root;
            while (currentNode.Full)
            {
                DisplayNodeAndChildren(currentNode);
                currentNode = currentNode.left;
            }
        }

        private static void DisplayNodeAndChildren(Node currentNode)
        {
            Console.WriteLine("...{0}...", currentNode.value);
            Console.WriteLine("...{0}...{1}...", currentNode.left.value, currentNode.right.value);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree tree = new BinarySearchTree();
            tree.Insert(9);
            tree.Insert(4);
            tree.Insert(6);
            tree.Insert(20);
            tree.Insert(170);
            tree.Insert(15);
            tree.Insert(1);
            tree.Insert(13);

            tree.Lookup(1500);  //1500 not found
            tree.Insert(1500);  //Added 1500
            tree.Lookup(1500);  //1500 found

            tree.Print();//Not working yet
        }
    }
}
