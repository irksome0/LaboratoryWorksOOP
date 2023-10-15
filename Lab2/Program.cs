using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using System.Xml.Linq;

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
        public Task3(string input)
        {
            if (IsPaired(input))
            {
                Console.WriteLine("Дужки спiвпадають");
            }
            else
            {
                Console.WriteLine("Дужки не спiвпадають");
            }
            Console.WriteLine($"Найдовше слово: {findTheLongestWord(input)}");
            Console.WriteLine($"Рядок без слiв, що складаються лише з латинських лiтер: {removeWords(input)}");
        }
        private bool IsPaired(string input)
        {
            Dictionary<char, char> keyValuePairs = new Dictionary<char, char>()
            {
                {'(', ')'},
                {'[', ']'},
            };

            List<char> opening_brackets = new List<char> { };

            if (string.IsNullOrEmpty(input)) { return true; }

            foreach (char ch in input)
            {
                if (keyValuePairs.ContainsKey(ch) || keyValuePairs.ContainsValue(ch))
                {
                    if (keyValuePairs.ContainsKey(ch)) { opening_brackets.Add(ch); }
                    else
                    {
                        if (opening_brackets.Count == 0) { return false; }
                        char check = ' ';
                        foreach (char key in keyValuePairs.Keys)
                        {
                            if (keyValuePairs[key] == ch)
                            {
                                check = key;
                            }
                        }

                        if (check != opening_brackets[opening_brackets.Count - 1]) { return false; }

                        opening_brackets.RemoveAt(opening_brackets.Count - 1);
                    }
                }
            }
            if (opening_brackets.Count != 0)
            {
                return false;
            }
            return true;
        }
        private string findTheLongestWord(string input)
        {
            string theLongest = "";
            string temp = "";
            for (int i = 0; i < input.Length; i++)
            {
                int ch = char.ConvertToUtf32(input, i);
                if (ch >= 97 && ch <= 122 || ch >= 65 && ch <= 90 || ch >= 1072 && ch <= 1105 || ch == 63)
                {
                    if (ch == 63)
                    {
                        temp += 'i';
                    }
                    else
                    {
                        temp += char.ConvertFromUtf32(ch);
                    }
                }
                else if (temp.Length != 0)
                {
                    if (temp.Length > theLongest.Length)
                    {
                        theLongest = temp;
                    }
                    temp = "";
                }
            }
            if (temp.Length > theLongest.Length)
            {
                theLongest = temp;
            }
            return theLongest;
        }
        private string removeWords(string input)
        {
            List<string> words = new List<string>();
            string temp = "";
            for (int i = 0; i < input.Length; i++)
            {
                int ch = char.ConvertToUtf32(input, i);
                if (ch >= 97 && ch <= 122 || ch >= 65 && ch <= 90 || ch >= 1072 && ch <= 1105 || ch == 63)
                {
                    if (ch == 63)
                    {
                        temp += 'i';
                    }
                    else
                    {
                        temp += char.ConvertFromUtf32(ch);
                    }
                }
                else
                {
                    words.Add(temp);
                    temp = "";
                }
            }
            words.Add(temp);
            for (int k = 0; k < words.Count; k++)
            {
                bool flag = true;
                for (int i = 1072; i < 1106; i++)
                {
                    if (words[k].Contains(char.ConvertFromUtf32(i)))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    words.RemoveAt(k);
                }
            }
            input = string.Join(' ', words);
            return input;
        }
    }
    public class DataBase
    {
        protected static readonly Random random = new Random();
        protected List<Owner> owners;
        public DataBase()
        {
            owners = new List<Owner>();
        }
        protected struct Owner
        {
            public Owner()
            {
                this.firstName = "";
                this.lastName = "";
                this.phoneNumber = "";
                this.contracts = new List<Contract>();
                this.city = "";
            }
            public Owner(string fname, string sname, string phoneNumber, string city)
            {
                this.firstName = fname;
                this.lastName = sname;
                this.phoneNumber = phoneNumber;
                this.contracts = new List<Contract>();
                this.city = city;
            }
            public string firstName;
            public string lastName;
            public string phoneNumber;

            public List<Contract> contracts;
            public string city;
            public override string ToString()
            {
                return $"{firstName}, {lastName}, {phoneNumber}, {city}. Кiлькiсть контрактiв: {contracts.Count}";
            }
        }
        protected struct Contract
        {
            public Contract()
            {
                this.contractID = Convert.ToString(random.Next(1000, 1000000));
                this.address = "";
                this.apartmentPhoneNumber = "";

                this.penalty = 0;
                this.insurance = 0;

                this.monthlyPayment = 0;

                this.contractStartDate = "";
                this.contractEndDate = "";
            }
            public Contract(string address, string apNumber, double penalty, double insurance, string start, string end)
            {
                this.contractID = Convert.ToString(random.Next(1000, 1000000));
                this.address = address;
                this.apartmentPhoneNumber = apNumber;

                this.penalty = penalty;
                this.insurance = insurance;

                this.monthlyPayment = insurance * 0.05;

                this.contractStartDate = start;
                this.contractEndDate = end;
            }
            public string contractID;
            public string address;
            public string apartmentPhoneNumber;

            public double penalty;
            public double insurance;

            public double monthlyPayment;

            public string contractStartDate;
            public string contractEndDate;

            public string toString()
            {
                return @$"ID: {contractID} 
Адреса: {address}
Номер квартири:{apartmentPhoneNumber}
Штраф за хибний виклик: {penalty}
Страхування: {insurance}
Мiсячна плата: {monthlyPayment}
Початок договору: {contractStartDate}
Кiнець договору:{contractEndDate}";



            }
        }
        protected struct securityAlarms
        {
            public string date;
            public string reason;
        }
        public virtual void AddOwner()
        {
            Console.Write("Введiть ваше iм'я: ");
            string firstName = Console.ReadLine();
            Console.Write("Введiть ваше прiзвище: ");
            string lastName = Console.ReadLine();
            Console.Write("Введiть ваш номер телефону: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Введiть ваше мiсто: ");
            string city = Console.ReadLine();

            Owner owner = new Owner(firstName, lastName, phoneNumber, city);

            owners.Add(owner);

            Console.WriteLine("Вас успiшно додано в систему!");
        }
        private void ProcessContract()
        {
            Console.Write("Виберiть користувача по номеру: ");
            string choice = Console.ReadLine();
            Console.Write("Введiть ваш адрес:");
            string address = Console.ReadLine();
            Console.Write("Введiть квартирний номер: ");
            string apNumber = Console.ReadLine();
            Console.Write("Введiть суму страхування: ");
            double insurance = Convert.ToDouble(Console.ReadLine());
            Contract cont = new Contract(address, apNumber, insurance * 0.15, insurance, Convert.ToString(DateTime.Now), Convert.ToString(DateTime.Now.AddYears(5)));
            owners[Convert.ToInt32(choice) - 1].contracts.Add(cont);
        }
        public virtual void AddContract()
        {
            Console.Write("Введiть ваше iм'я: ");
            string name = Console.ReadLine();

            bool foundUser = false;
            for(int i = 0; i < owners.Count; i++)
            {
                if (owners[i].firstName == name)
                {
                    Console.WriteLine($"{i + 1}.{owners[i].ToString()}");
                    foundUser = true;
                }
            }
            if (!foundUser)
            {
                Console.WriteLine("=========== Користувача не знайдено в системi ===========");
                Console.WriteLine("1. Додати користувача");
                Console.WriteLine("2. Спробувати знову");
                Console.Write(">> ");
                string choice = Console.ReadLine();
                Console.WriteLine("=========================================================");
                switch (choice)
                {
                    case "1":
                        AddOwner();
                        return;
                    case "2":
                        AddContract();
                        return;
                    default:
                        return;
                }
            }
            else {
                ProcessContract();
            }
        }
        public void DeleteUser()
        {
            Console.Write("Виберiть користувача по номеру: ");
            string choice = Console.ReadLine();
            owners.RemoveAt(Convert.ToInt32(choice));
            Console.WriteLine("Користувача видалено!");
        }
        public virtual void Search()
        {
            Console.WriteLine("Введiть данi для пошуку(iм'я, прiзвище, номер телефону, мiсто):");
            string request = Console.ReadLine();
            bool foundUser = false;
            for(var i = 0; i < owners.Count; i++)
            {
                if (owners[i].firstName == request || owners[i].lastName == request ||
                    owners[i].phoneNumber == request || owners[i].city == request)
                {
                    Console.WriteLine($"{i + 1}. {owners[i].ToString()}");
                    foundUser = true;
                }
            }
            if (!foundUser)
            {
                Console.WriteLine("=========== Користувача не знайдено в системi ===========");
                Console.WriteLine("1. Додати користувача");
                Console.WriteLine("2. Спробувати знову");
                Console.Write(">> ");
                string choice = Console.ReadLine();
                Console.WriteLine("=========================================================");
                switch (choice)
                {
                    case "1":
                        AddOwner();
                        return;
                    case "2":
                        Search();
                        return;
                    default:
                        return;
                }
            }
            else
            {
                Console.WriteLine("=========== Виберiть дiю ===========");
                Console.WriteLine("1. Додати контракт");
                Console.WriteLine("2. Видалити користувача");
                Console.WriteLine("3. Показати контракти");
                Console.Write(">> ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ProcessContract();
                        return;
                    case "2":
                        DeleteUser();
                        return;
                    case "3":
                        ShowContract();
                        return;
                    default:
                        return;
                }
            }
        }
        public virtual void ShowContract()
        {
            Console.Write("Виберiть користувача по номеру: ");
            string choice = Console.ReadLine();
            foreach (var con in owners[Convert.ToInt32(choice) - 1].contracts)
            {
                Console.WriteLine("==============================================");
                Console.WriteLine(con.toString());
                Console.WriteLine("==============================================");
            }
        }
        public virtual void Show()
        {
            foreach (var owner in owners)
            {
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine(owner.ToString());
                foreach (var contract in owner.contracts)
                {
                    Console.WriteLine("==============================================");
                    Console.WriteLine(contract.toString());
                    Console.WriteLine("==============================================");
                }
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++");
            }
        }
        public virtual void ShowOwners()
        {
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++");
            foreach (var owner in owners)
            {
                Console.WriteLine(owner.ToString());
            }
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++");
        }
    }
    public class Task4 : DataBase
    {
        DataBase db;
        public Task4()
        {
            db = new DataBase();
        }
        public override void AddOwner()
        {
            db.AddOwner();
        }
        public override void AddContract()
        {
            db.AddContract();
        }

        public override void Show()
        {
            db.Show();
        }
        public override void ShowOwners()
        {
            db.ShowOwners();
        }
        public override void Search()
        {
            db.Search();
        }
        public void menu()
        {
            bool flag = true;
            string choice = "";
            while (flag) {
                Console.WriteLine("========================");
                Console.WriteLine("1) Вiдобразити всi данi:");
                Console.WriteLine("2) Вiдобразити всiх власникiв квартир:");
                Console.WriteLine("3) Додати власника квартири:");
                Console.WriteLine("4) Укласти контракт:");
                Console.WriteLine("5) Пошук власникiв по iменi/фамiлiї/номеру телефона/мiсту");
                Console.Write("Виберiть дiю: ");
                choice = Console.ReadLine();
                Console.WriteLine("========================");
                switch (choice)
                {
                    case "1":
                        Show();
                        break;
                    case "2":
                        ShowOwners();
                        break;
                    case "3":
                        AddOwner();
                        break;
                    case "4":
                        AddContract();
                        break;
                    case "5":
                        Search();
                        break;
                    default:
                        flag = false;
                        break;
                }
            }
        }
    }
    internal static class Program
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
            Console.Write("Введiть рядок: ");
            str = Console.ReadLine();
            Task3 task3 = new Task3(str);
        }
        private static void task4()
        {
            Task4 task4 = new Task4();
            task4.menu();
        }
        public static void Main(string[] args)
        {
            bool flag = true;
            while (flag)
            {
                Console.Write("Виберiть варiант завдання: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        task1();
                        break;
                    case "2":
                        task2();
                        break;
                    case "3":
                        task3();
                        break;
                    case "4":
                        task4();
                        break;
                    default:
                        flag = false;
                        break;
                }
            }
        }
    }
}