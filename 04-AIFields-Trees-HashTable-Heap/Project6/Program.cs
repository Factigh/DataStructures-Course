using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Project6
{

    class Subfield : IComparable<Subfield> //Subfield class for name, definition and application of subfield.
    {
        public string name;
        public string def;
        public string appl;

        public Subfield(string name, string def, string appl)
        {
            this.name = name;
            this.def = def;
            this.appl = appl;
        }

        public override string ToString()
        {
            return "Subfield: " + name+"\nDefinition: "+def+"\nApplications: "+appl+"\n";
            
        }
 
        public int CompareTo(Subfield other) //Method for comparing alphabetically
        {
            if (other == null) return 1; 
            return string.Compare(this.name, other.name, true); //Case-insensitive
        }

    }


    class Field : IComparable<Field> //Field class for name and tree of subfields.
    {
        public string name;
        public Tree<Subfield> subfieldTree; 


        public Field(string name)
        {
            this.name = name;
            subfieldTree = new Tree<Subfield>();    
        }

        public override string ToString()
        {
            return name+"\n";          
        }
  
        public int CompareTo(Field other)
        {
            if (other == null) return 1;
            return string.Compare(this.name, other.name, true);
        }

    }

    class WordClass : IComparable<WordClass> //Word class for word and to hold it's count value when inserting.
    {
        public string word;
        public int count;

        public WordClass(string word)
        {
            this.word = word;
            count = 0;
        }

        public int CompareTo(WordClass other)
        {
            if (other == null) return 1;
           
            return string.Compare(other.word, this.word, true); //Comparing in reverse alphabetically          
        }

        public override string ToString()
        {
            return $"Word: {word,-20} Count: {count} ";                
        }
    }

    class Node<T> //Generic Node class to hold different type of data
    {
        public T data;
        public Node<T> leftChild;
        public Node<T> rightChild;

        public Node(T data)
        {
            this.data = data;
            leftChild = null;
            rightChild = null;
        }

    }
    class Tree<T> where T : IComparable<T> //Generic Tree class for 3 different type of trees. (Field,Subfield,Word)
    {
        Node<T> root;
       
        public Tree() { root = null;  }

        public Node<T> GetRoot() { return root; }
        public void InOrder(Node<T> localRoot) //Method for printing the tree in alphabetical order by using recursion.
        {
            if (localRoot != null)
            {
                InOrder(localRoot.leftChild);
                Console.WriteLine(localRoot.data.ToString());
                InOrder(localRoot.rightChild);
            }
        }

        public void InOrder()
        {
            InOrder(root); // Overload
        }

        public void ReverseInOrder(Node<T> localRoot) //Method for printing the tree in reverse alphabetical order by using recursion.
        {
            if (localRoot != null)
            {
                ReverseInOrder(localRoot.rightChild);              
                Console.WriteLine(localRoot.data.ToString());
                ReverseInOrder(localRoot.leftChild);
            }
        }

        public void ReverseInOrder() //Overload
        {
            ReverseInOrder(root);
        }

        public Node<T> Insert(T newData) //Method for inserting new Data in correct place
        {
            
            Node<T> newNode=new Node<T>(newData);

            if (root == null)
            {
                root = newNode;
                return root;
            }
            else
            {
                Node<T> current = root;
                Node<T> parent;

                while (true)
                {
                    parent = current;
                    if (newData.CompareTo(current.data) < 0)
                    {
                        current = current.leftChild;
                        if (current == null)
                        {
                            parent.leftChild = newNode;
                            return parent.leftChild;
                        }
                    }
                    else if(newData.CompareTo(current.data) > 0)
                    {
                        current = current.rightChild;
                        if (current == null)
                        {
                            parent.rightChild = newNode;
                            return parent.rightChild;
                        }
                    }
                    else
                    {
                        return current;
                    }
                    
                }
                
            }
        }
        public int GetHeight(Node<T> node)
        {
            if (node == null)
                return -1;

            int leftHeight = GetHeight(node.leftChild);
            int rightHeight = GetHeight(node.rightChild);

            return Math.Max(leftHeight, rightHeight)+1;
        }
       
        public int GetHeight() //Overload
        {
            return GetHeight(root);
        }

    }

    class MinHeap //Minheap class with using List
    {
        public List<Field> list;

        public MinHeap()
        {
            list=new List<Field>();
        }

        public void Insert(Field newField)
        {
            list.Add(newField);
            TrickleUp(list.Count-1);
        }

        public void TrickleUp(int index) //Method for correcting the place of the inserted data.
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (list[index].CompareTo(list[parentIndex]) < 0)
                {
                    Swap(index, parentIndex);
                    index=parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        public Field Delete() 
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Heap is empty.");
                return null;
            }

            Field root = list[0];

            if (list.Count == 1)
            {
                list.RemoveAt(0);
                return root;
            }
            list[0] = list[list.Count-1];
            list.RemoveAt(list.Count-1);
            TrickleDown(0);
            return root;
        }

        public void TrickleDown(int index) //Method for correcting the place of the new temporary root
        {
            int smallerChild;
            Field top=list[index];

            while (index < list.Count / 2)
            {
                int leftChild = 2 * index + 1;
                int rightChild=leftChild + 1;

                if(rightChild<list.Count && list[rightChild].CompareTo(list[leftChild])<0)
                {
                    smallerChild = rightChild;
                }
                else
                {
                    smallerChild = leftChild;
                }

                if (top.CompareTo(list[smallerChild]) <= 0)
                {
                    break;
                }
                Swap(index, smallerChild);
                index=smallerChild;
            }
        }
        public void Swap(int i,int j)
        {
            Field temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

        internal class Program
        {
        static Node<Field> deepestNode = null;
        static int maxDepth = -1;
        static void FindDeepestField(Node<Field> node) //Method for finding the deepest field with using GetHeight()
        {
            if (node != null)
            {
                FindDeepestField(node.leftChild);
                int currentHeight = node.data.subfieldTree.GetHeight();
                if (currentHeight > maxDepth)
                {
                    maxDepth = currentHeight;
                    deepestNode = node;
                }
                FindDeepestField(node.rightChild);
            }
        }
        public static void FillTree(Tree<Field> tree)//Parsing and transferring the data to tree.
        {
            string text = @"
  Machine Learning

Supervised Learning

Definition: Training models using labeled data to predict outcomes.

Applications: Spam detection, sentiment analysis, price prediction.

Unsupervised Learning

Definition: Finding patterns in data without labeled examples.

Applications: Clustering customers, anomaly detection, dimensionality reduction.

Reinforcement Learning

Definition: Learning optimal actions through trial and error and rewards.

Applications: Game AI, robotics, autonomous navigation.

  Natural Language Processing

Text Classification

Definition: Automatically categorizing text into predefined classes.

Applications: Email filtering, sentiment analysis, topic detection.

Machine Translation

Definition: Automatically translating text between languages.

Applications: Google Translate, localization, multilingual chatbots.

Question Answering

Definition: Providing answers to natural language questions from a dataset.

Applications: Virtual assistants, search engines, customer support.

  Computer Vision

Image Classification

Definition: Assigning a label to an entire image.

Applications: Medical imaging, object recognition, autonomous vehicles.

Object Detection

Definition: Identifying and localizing objects within images.

Applications: Surveillance, self-driving cars, face detection.

Image Segmentation

Definition: Dividing an image into meaningful segments.

Applications: Medical imaging, scene understanding, AR/VR.

  Robotics

Motion Planning

Definition: Computing a sequence of movements to reach a goal.

Applications: Autonomous robots, drones, warehouse automation.

Control Systems

Definition: Designing controllers for robots to follow trajectories.

Applications: Industrial robots, prosthetics, autonomous vehicles.

  Generative AI

GANs

Definition: Generating realistic data by training a generator against a discriminator.

Applications: Deepfakes, image synthesis, style transfer.

Diffusion Models

Definition: Generating data by iteratively denoising random noise.

Applications: AI art, high-quality image generation, image editing.

VAEs

Definition: Learning latent representations to generate new data samples.

Applications: Anomaly detection, image generation, latent space exploration.";

            Field currentField = null;
            Subfield currentSubfield = null;
            
            string[] lines = text.Split('\n');

            string subfieldName = "";
            string def = "";
            string appl = "";

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Trim() == "") { continue; }
               
                if (line.StartsWith("  "))
                {                    
                    string fieldName = line.Substring(2).Trim();
                    currentField = new Field(fieldName);
                    tree.Insert(currentField);
                }
                else if (line.StartsWith("Definition:"))
                {
                    def = line.Substring("Definition:".Length).Trim();
                }
                else if (line.StartsWith("Applications:"))
                {
                    appl = line.Substring("Applications:".Length).Trim();
                }
                else
                {
                    subfieldName = line.Trim();
                }
                if (subfieldName!="" && def!="" && appl != "")
                {
                    currentSubfield = new Subfield(subfieldName, def, appl);
                    currentField.subfieldTree.Insert(currentSubfield);
                    subfieldName = "";
                    def = "";
                    appl = "";
                }               
            }
        }
        static void PrintFullTree(Node<Field> node) //Printing each Field with it's subfields
        {
            if (node != null)
            {
                PrintFullTree(node.leftChild);
                Console.WriteLine(node.data.ToString());
                node.data.subfieldTree.InOrder();
                Console.WriteLine("--------------");
                PrintFullTree(node.rightChild);
            }
        }       
        public static void GetWords(Node<Field> node,Tree<WordClass> wordTree) //Transferring words to Word Tree from each subfield.
        {
            if (node == null) return;

            GetWords(node.leftChild,wordTree);             
            GetWordsFromSub(node.data.subfieldTree.GetRoot(),wordTree);
            GetWords(node.rightChild,wordTree);
        }
        public static void GetWordsFromSub(Node<Subfield> node,Tree<WordClass> wordTree)
        {
            if (node == null) return;

            GetWordsFromSub(node.leftChild, wordTree);

            if (node.data.def != null)
            {
                string definition = node.data.def;
                string[] words = definition.Split(new char[] { ' ', '.', ',', '-', '(', ')', ':', ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in words)
                {
                    string cleanWord = word.ToLower().Trim();
                                                
                    WordClass newData = new WordClass(cleanWord);
                    Node<WordClass> currWordNode= wordTree.Insert(newData);
                    
                    currWordNode.data.count++;
                }
            }
            GetWordsFromSub(node.rightChild, wordTree);
        }    
        public static void FillDictionary(Node<Field> node, Dictionary<string, Node<Field>> dict)  //Transferring data to Dictionary from Tree.
        {
                if (node == null) return;
                FillDictionary(node.leftChild,dict);
                dict[node.data.name] = node; 
                FillDictionary(node.rightChild,dict);
        }
        public static void UpdateDictionary(Dictionary<string, Node<Field>> dict) //Updating the Field name in Dictionary with given input
        {
            Console.Write("Enter old field name: ");
            string oldName = Console.ReadLine();

            while (!dict.ContainsKey(oldName))
            {
                Console.Write("Field not found. Enter field again: ");
                oldName=Console.ReadLine();
            }
            Console.Write("Enter new field name: ");
            string newName = Console.ReadLine();

            Node<Field> node = dict[oldName];
            dict.Remove(oldName);
            dict[newName] = node;
            Console.WriteLine("Updated successfully.");
        }
        public static void PrintDictionary(Dictionary<string, Node<Field>> dict)
        {
            foreach(string key in dict.Keys)
            {
                Console.WriteLine(key);
            }
        }
        public static void FillHeap(Node<Field> node,MinHeap minheap) //Transferring each Field to Heap structure
        {
            if(node == null) return;
            FillHeap(node.leftChild,minheap);
            minheap.Insert(node.data);
            FillHeap(node.rightChild,minheap);
        }
        public static void PrintHeap(MinHeap minheap) //Printing top element(min.) each time from heap structure
        {           
            if (minheap.list.Count == 0)
            {
                Console.WriteLine("Heap is empty.");
                return;
            }
            MinHeap clone = new MinHeap();
         
            foreach (Field field in minheap.list)
            {
                clone.list.Add(field);
            }
            for (int i = 0; i < 5; i++)
            {
                Field topField = clone.Delete();
                if (topField == null) break;

                Console.WriteLine(topField.ToString());
                topField.subfieldTree.InOrder();
                Console.WriteLine("--------------");
            }
        }
        static void Main(string[] args)
        {
            Tree<Field> mainTree = new Tree<Field>();
            FillTree(mainTree);
            Console.WriteLine("-ARTIFICIAL INTELLIGENCE FIELDS-\n");
            PrintFullTree(mainTree.GetRoot());
            FindDeepestField(mainTree.GetRoot());
            if (deepestNode != null)
            {
                Console.WriteLine();
                Console.WriteLine("-Deepest Field-");
                Console.WriteLine("Field Name: " + deepestNode.data.name);
                Console.WriteLine("Max Depth: " + maxDepth);
                Console.WriteLine();
                deepestNode.data.subfieldTree.InOrder();
            }
            else
            {
                Console.WriteLine("Data not found.");
            }
            Tree<WordClass> wordTree = new Tree<WordClass>();

            GetWords(mainTree.GetRoot(),wordTree);
            Console.WriteLine("         -Word Frequency-         ") ;                                   
            wordTree.ReverseInOrder();

            Dictionary<string,Node<Field>> dict = new Dictionary<string, Node<Field>>(StringComparer.OrdinalIgnoreCase);

            FillDictionary(mainTree.GetRoot(), dict);

            UpdateDictionary(dict);
            Console.WriteLine("\nDICTIONARY FIELDS:");
            PrintDictionary(dict);
            Console.WriteLine();
            
            MinHeap minheap= new MinHeap();
            FillHeap(mainTree.GetRoot(),minheap);                
                
            Console.WriteLine("HEAP DATA:\n");
            PrintHeap(minheap);
            Console.Read();
        }
    }
} 
