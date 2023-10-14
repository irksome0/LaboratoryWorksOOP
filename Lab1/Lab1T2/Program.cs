using System.ComponentModel.DataAnnotations;

namespace LaboratoryWork1
{
    //Task 1
    class Func
    {
        public double Y(double a, double x)
        {
            double y = 0;
            y = Math.Log2(x - 4) + Math.Pow(Math.E, 2 * a - x);
            return y;
        }
    }
    //Task 2
    class Mersenne
    {
        public List<int> FindAll(int n)
        {
            List<int> result = new List<int>();
            for(int i = 1; i < n; i++)
            {
                bool isPrime = true;
                for(int j = 2; j < i; j++)
                {
                    if(i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    result.Add(Convert.ToInt32(Math.Pow(2, i) - 1));
                }
            }
            return result;
        }
    }
    //Task 3
    class Solution
    {
        public double Y(double s, double t)
        {
            return G(1.2, t) + G(t, s) - G(2 * s - 1, s * t);
        }
        
        private double G(double a, double b)
        {
            double result = 0;
            result = (Math.Pow(a, 2) + Math.Pow(b, 2)) / (Math.Pow(a, 2) + 2 * a * b + 3 * Math.Pow(b, 2) + 4);
            return result;
        }
    }
    internal static class Task2
    {
        public static void Main(string[] args)
        {
            //Task 1
            Func func = new Func();
            double a, x;

            Console.WriteLine("Enter a and x:");
            a = Convert.ToDouble(Console.ReadLine());
            x = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine(func.Y(a, x));

            //Task 2
            Mersenne func = new Mersenne();

            int n;
            Console.WriteLine("Enter a number:");
            n = Convert.ToInt32(Console.ReadLine());

            var y = func.FindAll(n);
            Console.WriteLine("[" + String.Join(',',y) + "]");

            //Task 3
            Solution func = new Solution();

            double s, t;

            Console.WriteLine("Enter s and t:");
            s = Convert.ToDouble(Console.ReadLine());
            t = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Result is: " + func.Y(s, t));
        }
    }
}
