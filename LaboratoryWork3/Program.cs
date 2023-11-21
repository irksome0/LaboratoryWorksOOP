using System.Runtime.CompilerServices;

namespace LaboratoryWork3
{
    // Class that contains info about some person
    // Used to contain information about authors of articles
    public class Person
    {
        private string firstName;
        private string surName;
        private DateTime birthday;

        public Person(string fn, string sn, DateTime dt)
        {
            firstName = fn;
            surName = sn;
            birthday = dt;
        }
        public Person()
        {
            firstName = "None";
            surName = "None";
            birthday = DateTime.Now;
        }
        public string GetFirstName()
        {
            return firstName;
        }
        public string GetSurName()
        {
            return surName;
        }
        public DateTime GetBirthday()
        {
            return birthday;
        }
        public void SetFirstName(string fn)
        {
            firstName = fn;
        }
        public void SetSurName(string sn)
        {
            surName = sn;
        }
        public void SetBirthday(DateTime dt)
        {
            birthday = dt;
        }
        public int GetBirthYear()
        {
            return birthday.Year;
        }
        public void SetBirthYear(int year)
        {
            DateTime newBirthday = new DateTime(year, birthday.Month, birthday.Day, birthday.Hour, birthday.Minute, birthday.Second, DateTimeKind.Utc);
            birthday = newBirthday;
        }
        public override string ToString()
        {
            return $"{firstName}, {surName}, {birthday}";
        }
        public virtual string ToShortString()
        {
            return $"{firstName}, {surName}";
        }
    }
    // Enum field for frequency of releases
    public enum Frequency
    {
        Weekly = 2,
        Monthly = 8,
        Yearly = 96
    }

    // Class for articles that also contains info about actile author
    public class Article: Person
    {
        public readonly Person author;
        public readonly string nameOfArticle;
        public readonly double rating;

        public Article(Person p, string info, double num) { author = p; nameOfArticle = info; rating = num; }
        public Article() { author = new Person(); nameOfArticle = "No info"; rating = 0; }

        public override string ToString()
        {
            return $"Author: {author.ToString()}\nArticle: {nameOfArticle}, Rating: {rating}";
        }
    }

    // Class for magazines. Contains articles
    public class Magazine : Article
    {
        private readonly string name;
        private Frequency frequency;
        private readonly DateTime date;
        private readonly int amount;
        private List<Article> articles;

        public Magazine(string nm, Frequency freq, DateTime dt, int a) { name = nm; frequency = freq; date = dt; amount = a; }
        public Magazine() { name = "No info"; frequency = 0; date = DateTime.Now; amount = 0; }

        public string GetName()
        {
            return name;
        }
        public Frequency GetFrequency(int index)
        {
            switch (index)
            {
                case 0:
                    return Frequency.Weekly;
                case 1:
                    return Frequency.Monthly;
                case 2:
                    return Frequency.Yearly;
                default:
                    return 0;
            }
        }
        public DateTime GetDate()
        {
            return date;
        }
        public int GetAmount()
        {
            return amount;
        }
        public List<Article> GetArticles()
        {
            return articles;
        }

        public double GetAverageRating()
        {
            double sum = 0;
            foreach (var article in articles)
            {
                sum += article.rating;
            }
            return sum / articles.Count;
        }

        public Frequency this[int index]
        {
            get => GetFrequency(index);
        }

        public void AddArticle(Article article)
        {
            articles.Add(article);
        }
        public void SetArticles(List<Article> ar)
        {
            articles = ar;
        }

        public override string ToString()
        {
            string result = $"Magazine: {name}\nReleasing: {frequency}\nRealeased: {date}\nEdition: {amount}\n";
            foreach (var article in articles)
            {
                result += article.ToString() + "\n";
            }
            return result;
        }
        public override string ToShortString()
        {
            string result = $"Magazine: {name}\nReleasing: {frequency}\nRealeased: {date}\nEdition: {amount}\n";
            result += GetAverageRating();
            return result;
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            int nrow, ncol;
            Console.Write("Enter rows and columns separated by a space: ");
            string input = Console.ReadLine();
            string[] sep = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            nrow = Convert.ToInt16(sep[0]);
            ncol = Convert.ToInt16(sep[1]);

            int[] lineArray = new int[nrow * ncol];
            int[,] rectangularArray = new int[nrow, ncol];
            int[][] jaggedArray = new int[nrow][];
            for(int i = 0; i < nrow; i++)
            {
                jaggedArray[i] = new int[ncol];
            }

            for (int i = 0; i < nrow * ncol; i++)
            {
                lineArray[i] = i;
                rectangularArray[i / ncol, i % ncol] = i;
                jaggedArray[i / ncol][i % ncol] = i;
            }

            int start = Environment.TickCount;
            for(int i = 0; i < nrow * ncol; i++)
            {
                lineArray[i] = lineArray[i] * 2;
            }
            int end = Environment.TickCount;
            Console.WriteLine($"Line array time: {end - start}");

            start = Environment.TickCount;
            for (int i = 0; i < nrow; i++){
                for(int j = 0; j < ncol; j++)
                {
                    rectangularArray[i, j] = rectangularArray[i, j] * 2;
                }
            }
            end = Environment.TickCount;
            Console.WriteLine($"Rectangular array time: {end - start}");

            start = Environment.TickCount;
            for (int i = 0; i < nrow; i++){
                for (int j = 0; j < ncol; j++)
                {
                    jaggedArray[i][j] = jaggedArray[i][j] * 2;
                }
            }
            end = Environment.TickCount;
            Console.WriteLine($"Jagged array time: {end - start}");

            var articles = new List<Article>();
            Frequency freq = Frequency.Weekly;
            articles.Add(new Article(new Person("Jong", "Chunk", DateTime.Now), "Week", 9.9));
            articles.Add(new Article(new Person("Jong", "Chunk", DateTime.Now), "Boba", 9.3));
            Magazine magazine = new Magazine("Weekler", freq, DateTime.Now, 15);
            magazine.SetArticles(articles);

            Console.WriteLine(magazine.ToShortString());
            Console.WriteLine("============================");
            Console.WriteLine(magazine.ToString());
            Console.WriteLine(magazine[2]);
        }
    }
}
