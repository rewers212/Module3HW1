using System;
using System.Linq.Expressions;

namespace Module3HW1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var test = new NewList<int>();
            test.Add(5);
            test.Add(7);
            test.Add(55);
            test.Add(4);
            test.Add(0);
            test.Add(9);
            test.Add(10);
            test.Remove(7);
            test.Remove(55);
            test.Add(76);
            test.Add(35);
            test.Remove(4);
            test.Add(99);
            test.Add(0);
            test.Add(22);
            test.Remove(0);
            test.AddRange(new int[] { 36, 37, 38, 39, 40, 41 });
            test.Sort();
            foreach (var item in test)
            {
                Console.WriteLine(item);
            }
        }
    }
}