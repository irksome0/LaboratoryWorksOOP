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
            public Task1(int n)
            {
                int[] array = new int[n];
                segment = new ArraySegment<int>(array);
                getNumbers();
                sortArray();
            }

            public void getNumbers()
            {
                for (int i = 0; i < segment.Count; i++)
                {
                    segment[i] = Convert.ToInt32(Console.ReadLine());
                }
            }
            public int getSumOfPositiveNumbers()
            {
                int sum = 0;
                foreach (int i in segment)
                {
                    if (i >= 0)
                    {
                        sum += i;
                    }
                }
                return sum;
            }
            public void sortArray()
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
            public void searchMax()
            {
                if (Math.Abs(segment[0]) > Math.Abs(segment[^1]))
                {
                    max = segment[0];
                }
                else
                {
                    max = segment[^1];
                    maxId = segment.Count - 1;
                }
            }
            public void searchMin()
            {
                min = segment[^1];
                if (min < 0)
                {
                    int minId = segment.Count - 1;
                    while(min < 0) { 
                        min = segment[i];
                        minId--;
                    }
                    if(Math.Abs(min) > segment[i + 1])
                    {
                        min = segment[i + 1];
                    }
                }
            }
            public void see()
            {
                Console.WriteLine(min);
                Console.WriteLine(max);
            }
            public void show()
            {
                foreach (int i in segment)
                {
                    Console.WriteLine(i);
                }
            }
        }
        public static void Main(string[] args)
        {
            int n;
            n = Convert.ToInt32(Console.ReadLine());

            Task1 a = new Task1(n);
            Console.WriteLine("A sum of positive numbers: " + a.getSumOfPositiveNumbers());
            a.searchMin();
            a.searchMax();
            a.see();
            a.show();
        }
    }
}