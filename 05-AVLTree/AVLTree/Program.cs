using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AVLTree
{
    class Node //Node class to hold data
    {
        public int data;
        public int height;
        public Node leftChild;
        public Node rightChild;

        public Node(int data)
        {
            this.data = data;
            this.height = 1;
            this.leftChild = null;
            this.rightChild = null;
        }

    }

    class AVLTree //AVL Tree class to balance the tree when inserting
    {
        Node root;
        public AVLTree() 
        {
            root=null;
        }
        public Node GetRoot()
        {
            return root;
        }

        public int GetHeight(Node node)
        {
            if (node == null) return 0;

            return node.height;
        }

        public int BalanceDiff(Node node) //Getting the height difference of subtree from given root.
        {
            if(node == null) return 0;

            return GetHeight(node.leftChild) - GetHeight(node.rightChild);
        }

        public Node Insert(int newData,Node localRoot) //Inserting and balancing with rotate methods
        {
            if (localRoot == null)
            {
                return new Node(newData);
            }

            if (newData < localRoot.data)
            {
                localRoot.leftChild=Insert(newData,localRoot.leftChild);
            }
            else if(newData > localRoot.data)
            {
                localRoot.rightChild=Insert(newData,localRoot.rightChild);
            }
            else { return localRoot; }

            localRoot.height = Math.Max(GetHeight(localRoot.leftChild), GetHeight(localRoot.rightChild)) + 1;

            int diff =BalanceDiff(localRoot);

            if (diff>1 && newData<localRoot.leftChild.data) 
            {
                return RightRotate(localRoot);
            }
            if (diff<-1 && newData > localRoot.rightChild.data)
            {
                return LeftRotate(localRoot);
            }
            if (diff>1 && newData > localRoot.leftChild.data)
            {
                localRoot.leftChild=LeftRotate(localRoot.leftChild);
                return RightRotate(localRoot);
            }
            if (diff<-1 && newData < localRoot.rightChild.data)
            {
                localRoot.rightChild= RightRotate(localRoot.rightChild);
                return LeftRotate(localRoot);
            }

            return localRoot;
        }

        public void Insert(int newData) //Overload to update root
        {
            root = Insert(newData,root);
        }

        public Node LeftRotate(Node oldRoot) //Left rotation on the given subtree root
        {
            Node newRoot = oldRoot.rightChild;
            Node tempChild=newRoot.leftChild;

            newRoot.leftChild = oldRoot;
            oldRoot.rightChild= tempChild;

            oldRoot.height = Math.Max(GetHeight(oldRoot.leftChild),GetHeight(oldRoot.rightChild)) + 1;
            newRoot.height = Math.Max(GetHeight(newRoot.leftChild), GetHeight(newRoot.rightChild)) + 1;

            return newRoot;
        }

        public Node RightRotate(Node oldRoot) //Right rotation on the given subtree root
        {
            Node newRoot = oldRoot.leftChild;
            Node tempChild=newRoot.rightChild;

            newRoot.rightChild= oldRoot;
            oldRoot.leftChild= tempChild;

            oldRoot.height = Math.Max(GetHeight(oldRoot.leftChild), GetHeight(oldRoot.rightChild)) + 1;
            newRoot.height = Math.Max(GetHeight(newRoot.leftChild), GetHeight(newRoot.rightChild)) + 1;

            return newRoot;
        }    
    }

    internal class Program
    {
        public static void PrintTree(Node node)
        {
            if (node != null)
            {
                PrintTree(node.leftChild);
                Console.Write(node.data + " ");
                PrintTree(node.rightChild);
            }
            
        }
        static void Main(string[] args)
        {
            AVLTree tree=new AVLTree();
            int[] array = new int[] {16,5,6,9,21,28,2};
            foreach (int i in array) 
            {
                tree.Insert(i);
            }

            Console.Write("Tree Inorder: ");
            PrintTree(tree.GetRoot());
            Console.WriteLine($"\nBalanced Tree Height: {tree.GetHeight(tree.GetRoot())}");
            Console.Read();

        }
    }
}
