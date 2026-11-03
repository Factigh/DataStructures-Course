using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project5
{
    class PQ //Priority Queue class
    {
        List<int> list; 

        public PQ()
        {
            list = new List<int>();
        }

        //Methods for ascending order queue
        public void enque(int num) 
        {
            if (isEmpty())
            {
                list.Add(num);
            }
            else 
            {
                int index = 0;
                while (index<list.Count && list[index]<num)
                {
                    index++;
                }
                list.Insert(index, num);
            }
        }

        public int deque()
        {
            if (isEmpty()) 
            {
                Console.WriteLine("Queue is empty.");
                return 0;
            }
            else
            {
                int value = list[0];
                list.RemoveAt(0);
                return value;
            }
        }


        public bool isEmpty()
        {
            return list.Count == 0;
        }


        public void calcTime() //Priority queue waiting time calculating method
        {
            if (isEmpty())
            {
                Console.WriteLine("Queue is empty.");
            }
            else{
                PQ clone= new PQ(); //creating a clone so the original queue stays safe
                clone.list = new List<int>(this.list);

                double totalTime = 0;
                double customerTime = 0;
                int customerCount = 0;

                while (!clone.isEmpty())
                {
                    int number = clone.deque();
                    customerTime += number * 2.7;
                    totalTime += customerTime;
                    customerCount++;
                    Console.WriteLine($"{customerCount}.customer's waiting time: {customerTime}s");

                }
                double avrTime = totalTime / customerCount;
                Console.WriteLine($"\nAverage waiting time per customer: {avrTime}s");

            }
        }
    }


    internal class Program
    {
        public static void calcTime(Queue<int> queue) //Normal queue waiting time calculating method
        {
            if (queue.Count == 0) {
                Console.WriteLine("Queue is empty.");
            }
            else
            {
                Queue<int> clone = new Queue<int>(queue); //creating a clone so the original queue stays safe

                double totalTime = 0;
                double customerTime = 0;
                int customerCount = 0;

                while (clone.Count > 0)
                {
                    int number = clone.Dequeue();
                    customerTime += number * 2.7;
                    totalTime += customerTime;
                    customerCount++;
                    Console.WriteLine($"{customerCount}.customer's waiting time: {customerTime}s");

                }
                double avrTime = totalTime / customerCount;
                Console.WriteLine($"\nAverage waiting time per customer: {avrTime}s");

            }
        }

        static void Main(string[] args)
        {
            Queue<int> normalQueue = new Queue<int>(); //Creating objects
            PQ PQueue = new PQ();

            int[] productNums = new int[] { 7, 9, 5, 4, 15, 21, 6, 13, 3, 2 };
            foreach (int number in productNums)
            {
                normalQueue.Enqueue(number);
                PQueue.enque(number);
            }
            Console.WriteLine("For Normal Queue:");
            calcTime(normalQueue);
            Console.WriteLine("\nFor Priority Queue:");
            PQueue.calcTime();
                                 
        }
    }
}
