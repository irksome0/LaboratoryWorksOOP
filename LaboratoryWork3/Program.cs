namespace LaboratoryWork3
{
    // Клас, що містить інформацію про певну людину
    // В даному випадку цей клас містить інформацію про автора певної статті
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
    // Тип, що визначає частоту виходу
    public enum Frequency
    {
        Weekly,
        Monthly,
        Yearly
    }

    // Клас для статтей, що містить інформацію про автора типу Person
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
    // Клас журналів, що містить різноманітні статті
    public class Magazine: Article
    {
        private readonly string name;
        private readonly Frequency frequency;
        private readonly DateTime date;
        private readonly int amount;
        private List<Article> articles;

        public Magazine(string nm, Frequency freq, DateTime dt, int a) { name = nm; frequency = freq; date = dt; amount = a; }
        public Magazine() { name = "No info"; frequency = 0; date = DateTime.Now; amount = 0; }

        public string GetName()
        {
            return name;
        }
        public Frequency GetFrequency()
        {
            return frequency;
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

        // ПОБУДУВАТИ ІНДЕКСАТОР

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
            var articles = new List<Article>();
            Frequency freq = Frequency.Weekly;
            articles.Add(new Article(new Person("Jong", "Chunk", DateTime.Now), "Week", 9.9));
            articles.Add(new Article(new Person("Jong", "Chunk", DateTime.Now), "Boba", 9.3));
            Magazine magazine = new Magazine("Weekler", freq, DateTime.Now, 15);
            magazine.SetArticles(articles);

            Console.WriteLine(magazine.ToShortString());
            Console.WriteLine("============================");
            Console.WriteLine(magazine.ToString());
        }
    }
}
