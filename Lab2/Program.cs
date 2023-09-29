using System.Runtime.CompilerServices;

namespace LaboratoryWork2
{
    internal class Program
    {
        public class Task1
        {
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
                catch(OverflowException e)
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
                    segment[i] = Convert.ToInt32(Console.ReadLine());
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
                catch(InvalidOperationException e)
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
                    while(min < 0) { 
                        min = segment[minId];
                        minId--;
                    }
                    if(Math.Abs(min) > segment[minId + 1])
                    {
                        min = segment[minId + 1];
                    }
                }

            }
            private void calculateProduct()
            {
                if(minId < maxId)
                {
                    for(int i = minId + 1; i < maxId; i++)
                    {
                        product *= segment[i];
                    }
                }
                else
                {
                    for (int i = maxId; i < minId; i++)
                    {
                        product *= segment[i];
                    }
                }
            }
            public void see()
            {
                Console.WriteLine(min);
                Console.WriteLine(max);
                Console.WriteLine(product);
                Console.WriteLine(getSumOfPositiveNumbers());
            }
            public void show()
            {
                Console.Write("[");
                Console.Write(string.Join(',', segment));
                Console.Write("]");
            }
        }
        public static void Main(string[] args)
        {
            int n;
            try
            {
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
    }
}