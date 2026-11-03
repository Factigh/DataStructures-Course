using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Project4
{
    class TextStack //Stack class with using array
    {
        int maxSize;
        public string[] array;
        public int top;

        public TextStack(int maxSize)
        {
            this.maxSize = maxSize;
            array = new string[maxSize];
            top = -1;
        }

        //Methods
        public void push(string s)
        {
            if (isFull())
            {
                Console.WriteLine("Stack is full.");
                return;
            }
            array[++top] = s;
        }

        public string pop() 
        {
            if (isEmpty()) return null;
            return array[top--];
        }

        public bool isEmpty()
        {
            return top == -1;
        }

        public bool isFull()
        {
            return top == maxSize-1;
        }

        public void clear()
        {
            top = -1;
        }
        
    }

    class CommandQ //Queue class with using array
    {
        int maxSize;
        string[] quearray;
        int front;
        int rear;
        int itemCount;

        public CommandQ(int maxSize) {
            this.maxSize = maxSize;
            quearray = new string[maxSize];
            front = 0;
            rear = 0;
            itemCount = 0;
        }

        //Methods
        public void enque(string s) {

            if (isFull()) 
            {
                Console.WriteLine("Queue is full.");
                return;
            }

            quearray[rear] = s;
            rear= (rear+1) % maxSize;           
            itemCount++;
        }
        public string deque() {
            if (isEmpty()) return null;

            string word= quearray[front];
            front= (front+1) % maxSize; 
            itemCount--;
            return word;

        }
        public bool isEmpty() {
            return itemCount == 0;      
        }

        public bool isFull() {
            return itemCount == maxSize;
        }
    }

    internal class Program
    {

        static TextStack undoStack; //Creating objects
        static TextStack redoStack;
        static CommandQ queue;

        //Methods for text editing
        public static void write() 
        {
            string word = queue.deque();
            undoStack.push(word);
            redoStack.clear();

        }

        public static void undo()
        {
            if (!undoStack.isEmpty()) {
                string temp = undoStack.pop();
                redoStack.push(temp);
            }
            
        }

        public static void redo() 
        {
            if (!redoStack.isEmpty()) {
                string temp = redoStack.pop();
                undoStack.push(temp);
            }
        }

        public static void show()
        {
         for (int i = 0; i <= undoStack.top; i++)
            {
                Console.Write(undoStack.array[i]+" ");
            }
            Console.WriteLine();  
        }

        public static void execute(string text) //Method for splitting text and executing commands
        {
            string[] tokens=text.Split(' ');

            int maxSize = tokens.Length;

            undoStack = new TextStack(maxSize);
            redoStack = new TextStack(maxSize);
            queue = new CommandQ(maxSize);

            for (int i = 0;i<tokens.Length;i++) 
            {
               string token=tokens[i];
                if (token == "w")
                {
                    queue.enque(token);
                    if (i+1< tokens.Length) 
                    {
                        queue.enque(tokens[i + 1]);
                        i++;
                    }
                    else
                    {
                        Console.WriteLine("Error occurred: w is at the end.");
                    }
                    
                }
                else if (token=="u" || token=="r" || token=="s")
                {
                    queue.enque(token);
                }
                else
                {
                    Console.WriteLine($"Invalid command: {token}");
                    continue;
                }               
            }

            while (!queue.isEmpty())
            {
                string command=queue.deque();
                if (command == "w")
                {
                    write();
                }
                else if (command == "u")
                {
                    undo();
                }
                else if(command == "r") 
                { 
                    redo();                    
                }
                else if( command == "s")
                {
                    show();
                }
            }
        }
        static void Main(string[] args)
        {
            string text = "w Data w MINING u w Structures s";
            execute(text);
                     
        }
    }
}
