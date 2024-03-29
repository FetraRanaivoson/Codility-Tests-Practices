﻿using System;
using System.Collections.Generic;

namespace Binary_Tree
{
    /// <summary>
    /// A node prototype for binary trees
    /// </summary>
    public class Node
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
    public class BinarySearchTree
    {
        Node root;

        public BinarySearchTree()
        {
            this.root = null;
        }

        /// <summary>
        /// Insert this value to this the binary tree
        /// </summary>
        /// <param name="value"></param>
        public void Insert(int value)
        {
            //Traverse(ref this.root, value);
            TraverseRecursive(this.root, value);
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

        /// <summary>
        /// A more cleaner short recursive way to insert value to the binary tree
        /// </summary>
        private void TraverseRecursive(Node currentNode, int value)
        {
            //  Particular case: no root
            if (this.root == null)
            {
                this.root = new Node(value);
            }

            else if (value < currentNode.value)
            {
                //  Base case
                if (currentNode.left == null)
                {
                    currentNode.left = new Node(value);
                }
                //  Recursive case
                else
                {
                    TraverseRecursive(currentNode.left, value);
                }

            }
            else if (value > currentNode.value)
            {
                //  Base case
                if (currentNode.right == null)
                {
                    currentNode.right = new Node(value);
                }
                //  Recursive case
                else
                {
                    TraverseRecursive(currentNode.right, value);
                }
            }
        }

        /// <summary>
        /// Remove a value from the tree
        /// </summary>
        public void Remove(int value)
        {
            if (this.root == null)
            {
                Console.WriteLine("Failed to remove {0}, no root found", value);
            }
            else
            {
                Node currentNode = this.root;
                Node parentNode = null;
                while (currentNode != null)
                {
                    if (value < currentNode.value)
                    {
                        parentNode = currentNode;
                        currentNode = currentNode.left;
                    }
                    else if (value > currentNode.value)
                    {
                        parentNode = currentNode;
                        currentNode = currentNode.right;
                    }
                    else if (currentNode.value == value) //Match
                    {
                        //Case 1: No right child
                        if (currentNode.right == null)
                        {
                            if (parentNode == null) // case: deleting the root
                            {
                                this.root = currentNode.left;
                            }
                            else //Is the value to delete is less/greater than the parent value?
                            {
                                //  If parent value greater than current node value, then
                                //  make the current node left child a child of the parent
                                if (currentNode.value < parentNode.value)
                                {
                                    //parentNode.left = currentNode.left; //act of nullifying the current node
                                    parentNode.left = null;
                                    currentNode = parentNode.left;
                                }
                                //  Else if parent value less than current value, then
                                //  make the current node left child  a child of the right parent
                                else if (currentNode.value > parentNode.value)
                                {
                                    //parentNode.right = currentNode.left;
                                    parentNode.right = null;
                                    currentNode = parentNode.left;
                                }
                            }
                        }
                        //Case 2: Right child doesnt have a left child
                        else if (currentNode.right.left == null)
                        {
                            currentNode.right.left = currentNode.left;
                            if (parentNode == null)
                            {
                                this.root = currentNode.right;
                            }
                            else
                            {
                                //if parent > current, make right child of the left the parent
                                if (currentNode.value < parentNode.value)
                                {
                                    parentNode.left = currentNode.right;
                                    //parentNode.left = null;
                                    //currentNode = parentNode.right;
                                }
                                //if parent < current, make right child a right child of the parent
                                else if (currentNode.value > parentNode.value)
                                {
                                    parentNode.right = currentNode.right;
                                }
                            }
                        }
                        //Option 3: Right child that has a left child
                        else
                        {

                            //find the Right child's left most child
                            Node leftmost = currentNode.right.left;
                            Node leftmostParent = currentNode.right;
                            while (leftmost.left != null)
                            {
                                leftmostParent = leftmost;
                                leftmost = leftmost.left;
                            }

                            //Parent's left subtree is now leftmost's right subtree
                            leftmostParent.left = leftmost.right;
                            leftmost.left = currentNode.left;
                            leftmost.right = currentNode.right;

                            if (parentNode == null)
                            {
                                this.root = leftmost;
                            }
                            else
                            {
                                if (currentNode.value < parentNode.value)
                                {
                                    parentNode.left = leftmost;
                                }
                                else if (currentNode.value > parentNode.value)
                                {
                                    parentNode.right = leftmost;
                                }
                            }
                        }
                    }
                }
            }
            //Console.WriteLine("==============================");
        }


        /// <summary>
        /// Breadth First Search Algorithm.
        /// Searching broader first (left to right) then deeper (down)
        /// NB: the memory consumption for the queue can be very large if the tree is very wide
        /// </summary>
        public List<int> BreadthFirstSearch()
        {
            Node currentNode = this.root;
            List<int> list = new List<int>();
            Queue<Node> queue = new Queue<Node>(); //FIFO: we'll put in the left node first then the right node so we'll always go from left to right first
            queue.Enqueue(currentNode);

            //Console.WriteLine("BFS visit order: ");
            while (queue.Count > 0)
            {
                currentNode = queue.Dequeue(); //Overwrite current node. At the 1st iteration, the current node will be the root. Later it will be the 'First In' on the queue etc
                list.Add(currentNode.value);
                //Console.WriteLine("-> {0}", currentNode.value);
                if (currentNode.left != null)
                {
                    queue.Enqueue(currentNode.left);
                }
                if (currentNode.right != null)
                {
                    queue.Enqueue(currentNode.right);
                }
            }
            Console.Write("BFS visit order Normal method: ");
            Console.WriteLine(string.Join(", ", list));
            return list;
        }

        /// <summary>
        /// Breadth First Search Algorithm (Recursive method)
        /// </summary>
        public List<int> BreadthFirstSearchR()
        {
            Queue<Node> queue = new Queue<Node>();
            List<int> list = new List<int>();
            queue.Enqueue(this.root);
            return BreadthFirstSearchRecursive(queue, list);
        }

        private List<int> BreadthFirstSearchRecursive(Queue<Node> queue, List<int> list)
        {
            if (queue.Count == 0)
            {
                Console.Write("BFS visit order Recursive method: ");
                Console.WriteLine(string.Join(", ", list));
                return list;
            }
            Node currentNode = queue.Dequeue();
            list.Add(currentNode.value);
            if (currentNode.left != null)
            {
                queue.Enqueue(currentNode.left);
            }
            if (currentNode.right != null)
            {
                queue.Enqueue(currentNode.right);
            }
            return BreadthFirstSearchRecursive(queue, list);
        }

        /// <summary>
        /// In order Depth first search algorithm (Recursive approach).
        /// Going as deep as possible to the left first then wider to the right
        /// </summary>
        ///<returns>Returns the ordered list of visits in a DFS InOrder method</returns>
        public List<int> DFSInOrder()
        {
            Node currentNode = this.root;
            List<int> list = new List<int>();
            list = DFSInOrderRecursive(currentNode, list);

            Console.Write("DFS InOrder visit: ");
            Console.WriteLine(string.Join(", ", list));
            return list;
        }
        private List<int> DFSInOrderRecursive(Node currentNode, List<int> list)
        {
            if (currentNode.left != null)
            {
                DFSInOrderRecursive(currentNode.left, list); //Going deeper to the left until there is no more children
            }
            // No more node?
            list.Add(currentNode.value);

            if (currentNode.right != null) //Going deeper to the right until there is no more children
            {
                DFSInOrderRecursive(currentNode.right, list);
            }
            return list; //If there is no more depth to go, return the list but also return the last common ancestor 
        }

        /// <summary>
        /// Pre order Depth first search algorithm (Recursive approach).
        /// The order is we start FROM the PARENT FIRST. Useful when you want to recreate a tree because the tree is ordered. 
        /// </summary>
        /// <returns>Returns the ordered list of visits in a DFS PreOrder method ie STARTING FROM A PARENT FIRST.</returns>
        public List<int> DFSPreOrder()
        {
            Node currentNode = this.root;
            List<int> list = new List<int>();
            list = DFSPreOrderRecursive(currentNode, list);

            Console.Write("DFS PreOrder visit: ");
            Console.WriteLine(string.Join(", ", list));
            return list;
        }
        private List<int> DFSPreOrderRecursive(Node currentNode, List<int> list)
        {
            //The only difference from InOrder is you want to push at the very beginning before we get to the left node
            list.Add(currentNode.value);

            if (currentNode.left != null)
            {
                DFSPreOrderRecursive(currentNode.left, list); //Going deeper to the left until there is no more children
            }
            // No more node?
            //list.Add(currentNode.value);

            if (currentNode.right != null) //Going deeper to the right until there is no more children
            {
                DFSPreOrderRecursive(currentNode.right, list);
            }
            return list; //If there is no more depth to go, return the list but also return the last common ancestor 
        }

        /// <summary>
        /// Post order Depth first search algorithm (Recursive approach).
        /// The order is we start from the LEAF NODES (deep) from left to right then GO UP to a parent
        /// </summary>
        /// <returns>Returns the ordered list of visits in a DFS PostOrder method ie LEAFS FIRST THEN PARENT</returns>
        public List<int> DFSPostOrder()
        {
            Node currentNode = this.root;
            List<int> list = new List<int>();
            list = DFSPostOrderRecursive(currentNode, list);

            Console.Write("DFS PostOrder visit: ");
            Console.WriteLine(string.Join(", ", list));
            return list;
        }
        private List<int> DFSPostOrderRecursive(Node currentNode, List<int> list)
        {
            if (currentNode.left != null)
            {
                DFSPostOrderRecursive(currentNode.left, list); //Going deeper to the left until there is no more children
            }

            if (currentNode.right != null) //Going deeper to the right until there is no more children
            {
                DFSPostOrderRecursive(currentNode.right, list);
            }

            //Push when there is no left and right child anymore ie push the parent lastly
            list.Add(currentNode.value);

            return list; //If there is no more depth to go, return the list but also return the last common ancestor 
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"> value to search</param>
        /// <returns>returns the Node /*value*/ if found, else returns null/*-1*/</returns>
        public Node Lookup(int value)
        {
            if (this.root == null)
            {
                Console.WriteLine("Cannot find {0}, tree has not root.", value);
                //return -1;
                return null;
            }
            Node currentNode = this.root;
            while (true)
            {
                if (currentNode == null)//At the end of a node (left/right are null)
                {
                    Console.WriteLine("Entry {0} not found!", value);
                    //return -1;
                    return null;
                }
                if (value == currentNode.value)
                {
                    Console.WriteLine("Entry {0} found!", value);
                    //return currentNode.value;
                    return currentNode;
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

        /// <summary>
        /// Print every node and their connections (if any)
        /// </summary>
        public void Print()
        {
            Console.WriteLine("==============BINARY TREE===============");
            PrintRecursive(this.root);
            Console.WriteLine("========================================");
        }
        private void PrintRecursive(Node currentNode)
        {
            //  Base case
            //  if(currentNode == null){ do nothing because nothing to print)

            //  Recursive case
            if (currentNode != null)
            {
                if (currentNode.left != null && currentNode.right != null)
                {
                    Console.WriteLine("{0}--> L:{1}, R:{2}", currentNode.value, currentNode.left.value, currentNode.right.value);
                    PrintRecursive(currentNode.left);
                    PrintRecursive(currentNode.right);
                }
                else if (currentNode.left != null && currentNode.right == null)
                {
                    Console.WriteLine("{0}--> L:{1}, R:{2}", currentNode.value, currentNode.left.value, "Empty");
                    PrintRecursive(currentNode.left);
                }
                else if (currentNode.left == null && currentNode.right != null)
                {
                    Console.WriteLine("{0}--> L:{1}, R:{2}", currentNode.value, "Empty", currentNode.right.value);
                    PrintRecursive(currentNode.right);
                }
                else
                {
                    //Console.WriteLine("{0}--> L:{1}, R:{2}", currentNode.value, "Empty", "Empty");
                    Console.WriteLine("{0}--> Leaf", currentNode.value);
                }
            }

        }
    }

    class Program
    {
        //      9
        //    /   \
        //   4     20
        //  / \   /   \
        // 1   6 15   170
        //       /\   /  \
        //      13 19 21  1500
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

            //tree.Lookup(1500);  //1500 not found
            tree.Insert(1500);  //Added 1500
            //tree.Lookup(1500);  //1500 found

            tree.Insert(21);
            tree.Insert(19);

            //tree.Print();

            //tree.Remove(21);
            //tree.Remove(1500);

            tree.Print();

            tree.BreadthFirstSearch();
            tree.BreadthFirstSearchR();

            tree.DFSInOrder();
            tree.DFSPreOrder();
            tree.DFSPostOrder();

        }
    }
}
