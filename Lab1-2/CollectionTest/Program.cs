using System;
using System.Collections;
using System.Collections.Generic;
using MyList;

namespace CollectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var coll = new CustomList<int>() { 1, 2, 3 };
            EventHandler<int> onAdded = (sender, item) =>
            {
                Console.WriteLine("Added new item, {0}", item.ToString());
            };
            coll.ItemAdded += onAdded;
            Console.WriteLine("Initial state of list:");
            foreach (var a in coll)
            {
                Console.WriteLine(a);
            }

            coll.Add(4);
            Console.WriteLine("\nAddeed 4.");
            Console.WriteLine("State of list:");
            foreach (var a in coll)
            {
                Console.WriteLine(a);
            }

            Console.WriteLine("Value at [1]: {0}",coll[1]);
            Console.WriteLine("Index of 3:{0}",coll.IndexOf(3));

            coll[1] = 7;
            Console.WriteLine("\nChanged [1] element into 7");
            foreach (var a in coll)
            {
                Console.WriteLine(a);
            }
            Console.WriteLine("\nRemoved element at [2]:");
            coll.RemoveAt(2);
            foreach (var a in coll)
            {
                Console.WriteLine(a);
            }

            coll.Remove(7);
            Console.WriteLine("\nRemoved 7:");
            foreach (var a in coll)
            {
                Console.WriteLine(a);
            }

            coll.Insert(2, 12);
            Console.WriteLine("\nInserted 12 at [2]");
            foreach (var a in coll)
            {
                Console.WriteLine(a);
            }

            Console.WriteLine("\nContains 12? {0}", coll.Contains(12)? "Yes":"No");
           
            int[] numbers = new int[5];
            numbers[0] = 999999;
            coll.CopyTo(numbers, 1);
            Console.WriteLine("Side array after CopyTo (one index offset, first one of orig array is 999999):");
            foreach(var a in numbers)
            {
                Console.WriteLine(a);
            }

            coll.Clear();
            Console.WriteLine("Elements amount in list after Clear(): {0}",coll.Count);

            
        }
    }
}
