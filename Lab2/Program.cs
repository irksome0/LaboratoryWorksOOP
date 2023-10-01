using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace LaboratoryWork2
{
    public class Task1
    {
        Random random = new Random();
        ArraySegment<int> segment;
        private int max = 0, min = 0;
        private int maxId = 0, minId = 0;
        private int product = 1;

        public Task1(int n)
        {
            try
            {
                int[] array = new int[n];
                segment = new ArraySegment<int>(array);
                getNumbers();
                sortArray();
                searchMin();
                searchMax();
                calculateProduct();
            }
            catch (OverflowException e)
            {
                Console.WriteLine("Масив не може мати вiд'ємну кiлькiсть елементiв");
                int[] array = new int[0];
                segment = new ArraySegment<int>(array);
            }
        }

        private void getNumbers()
        {
            for (int i = 0; i < segment.Count; i++)
            {
                segment[i] = random.Next(-20, 15);
            }
        }
        public int getSumOfPositiveNumbers()
        {
            int sum = 0;
            try
            {
                foreach (int i in segment)
                {
                    if (i >= 0)
                    {
                        sum += i;
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
            }
            return sum;
        }
        private void sortArray()
        {
            for (int i = 0; i < segment.Count - 1; i++)
            {
                for (int j = i + 1; j < segment.Count; j++)
                {
                    if (segment[i] < segment[j])
                    {
                        int temp = segment[j];
                        segment[j] = segment[i];
                        segment[i] = temp;
                    }
                }
            }
        }
        private void searchMax()
        {
            if (Math.Abs(segment[0]) > Math.Abs(segment[^1]))
            {
                max = segment[0];
                maxId = 0;
            }
            else
            {
                max = segment[^1];
                maxId = segment.Count - 1;
            }
        }
        private void searchMin()
        {
            min = segment[^1];
            minId = segment.Count - 1;
            if (min < 0)
            {
                while (min < 0)
                {
                    minId--;
                    min = segment[minId];
                }
                if (Math.Abs(min) > Math.Abs(segment[minId + 1]))
                {
                    min = segment[minId + 1];
                }
            }

        }
        private void calculateProduct()
        {
            if (minId < maxId)
            {
                for (int i = minId + 1; i < maxId; i++)
                {
                    product *= segment[i];
                }
            }
            else
            {
                for (int i = maxId + 1; i < minId + 1; i++)
                {
                    product *= segment[i];
                }
            }
        }
        public void see()
        {
            Console.WriteLine($"Найменший елемент масиву: {min}");
            Console.WriteLine($"Найбiльший елемент масиву: {max}");
            Console.WriteLine($"Добуток: {product}");
            Console.WriteLine($"Сума: {getSumOfPositiveNumbers()}");
        }
        public void show()
        {
            Console.Write("[");
            Console.Write(string.Join(',', segment));
            Console.WriteLine("]");
        }
    }
    public class Task2
    {
        Random random = new Random();
        private int[,] array;
        private int[] sum;
        private int rows, cols;
        public Task2(int n, int k)
        {
            array = new int[n, k];
            rows = n; cols = k;
            sum = new int[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    array[i, j] = random.Next(0, 4);
                }
            }
            findColumns();
            findSum();
            sort();
        }
        public void show()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{array[i, j]} ");
                }
                Console.WriteLine();
            }
        }
        private void findColumns()
        {
            int amount = 0;
            for (int i = 0; i < cols; i++)
            {
                bool flag = false;
                for (int j = 0; j < rows; j++)
                {
                    if (array[j, i] == 0)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag) { amount++; }
            }
            Console.WriteLine(amount);
        }
        private void findSum()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (array[i, j] >= 0)
                    {
                        sum[i] += array[i, j];
                    }
                }
            }
        }
        private void sort()
        {
            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = i; j < rows - 1; j++)
                {
                    if (sum[j] > sum[j + 1])
                    {
                        for (int k = 0; k < cols; k++)
                        {
                            int tempSum = sum[j];
                            sum[j] = sum[j + 1];
                            sum[j + 1] = tempSum;
                            int temp = array[j, k];
                            array[j, k] = array[j + 1, k];
                            array[j + 1, k] = temp;
                        }
                    }
                }
            }
        }
    }
    public class Task3
    {
        private string sample;
        public Task3(string str)
        {
            sample = str;
            check();
        }
        private void check()
        {
            Dictionary<char, char> pairs = new Dictionary<char, char>()
            {
                {'(', ')'},
                {'[', ']'},
                {'{', '}'}
            };
            Stack<char> brackets = new Stack<char>();
            foreach(char ch in sample)
            {
                if (pairs.ContainsKey(ch))
                {
                    brackets.Push(ch);
                }else if (pairs.ContainsValue(ch) && brackets.Count != 0 && brackets.Pop() == ch)
                {
                    brackets.Pop();
                }
            }
            if (brackets.Count == 0)
            {
                Console.WriteLine("Accept");
            }
            else
            {
                Console.WriteLine("Declined");
            }
        }
    }
    internal class Program
    {
        private static void task1()
        {
            int n;
            try
            {
                Console.Write("Кiлькiсть елементiв масиву: ");
                n = Convert.ToInt32(Console.ReadLine());

                Task1 task1 = new Task1(n);
                task1.see();
                task1.show();
            }
            catch (OverflowException e)
            {
                Console.WriteLine("Введене число вийшло за рамки норми.");
            }
        }
        private static void task2()
        {
            int n, k;
            Console.Write("Кiлькiсть елементiв масиву: ");
            
            n = Convert.ToInt32(Console.ReadLine());
            Console.Write("Кiлькiсть елементiв масиву: ");
            k = Convert.ToInt32(Console.ReadLine());

            Task2 task2 = new Task2(n, k);

            task2.show();
        }
        private static void task3()
        {
            string str;
            str = Console.ReadLine();
            Task3 task3 = new Task3(str);
        }
        public static void Main(string[] args)
        {
            task1();
            task2();
            task3();
        }
    }
}