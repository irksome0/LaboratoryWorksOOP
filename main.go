package main

import (
	"errors"
	"fmt"
	"strings"
	"time"
)

// Базовий клас, вказаний для створення у всіх варіантах
// Містить дані про певну особу
// (ім'я; прізвище; дата народження)

type Person struct {
	firstName string
	surName   string
	birthday  time.Time
}

// Містить дані про певну статтю
// (автор, раніше визначеного типу Person; назву статті, рейтинг статті)

type Article struct {
	author        Person
	nameOfArticle string
	rating        float64
}

// Додатковий клас для класу Magazine
// (назва видання; дата видання; тираж)

type Edition struct {
	name        string
	releaseDate time.Time
	edition     int
}

// Містить дані про журнал
// (назва журналу; частота публікування; дата публікування; номер журналу)
// Також містить масив, визначеного типу Article

type Magazine struct {
	edition   Edition
	frequency int
	editors   []Person
	articles  []Article
}

// Інтерфейс для об'єктів із рейтингом та можливістю копіювання

type IRateAndCopy interface {
	GetRating() float64
	SetRating(rating float64)
	DeepCopy() IRateAndCopy
}

// Enum тип Frequency для визначення частоти публікування журналів

const (
	Weekly = iota
	Monthly
	Yearly
)

// Головний клас для реалізації

func main() {

	// Два об'єкта типу Edition
	weekley := Edition{
		name:        "Weekley",
		releaseDate: time.Now(),
		edition:     3,
	}
	strair := Edition{
		name:        "Strair",
		releaseDate: time.Now(),
		edition:     39,
	}
	// Приклад помилки при спробі присвоїння тиражеві від'ємного значення
	weekley, err := weekley.SetEditionNumber(2) // -2
	if err != nil {
		panic(err)
	}
	strair, err = strair.SetEditionNumber(1)
	if err != nil {
		panic(err)
	}

	//  Завдання для роботи з Magazine
	weekleyEditors := []Person{
		{"Kayle", "Loren", time.Now()},
		{"Johhan", " Absol", time.Now()},
		{"Leyla", "Sidde", time.Now()},
	}
	weekleyArticles := []Article{
		{Person{}, "Nowadays", 9.4},
		{Person{}, "Sidewalk", 7.5},
	}
	weekleyMagazine := Magazine{edition: weekley}
	weekleyMagazine.SetArticles(weekleyArticles)
	weekleyMagazine.AddEditors(weekleyEditors)

	fmt.Printf("%s\n", weekleyMagazine.edition.ToString())

	weekleyMagazineCopy := weekleyMagazine.DeepCopy()
	weekleyMagazineCopy.editors[0].SetName("Plai")
	weekleyMagazineCopy.articles[0].nameOfArticle = "Astro"
	weekleyMagazineCopy.edition.name = "Monthley"

	//
	// ???
	// ПРИ ЗАСТОСУВАННІ DeepCopy() І ЗМІНІ В КОПІЇ ДАНИХ В ОРИГІНАЛІ ТАКОЖ ДАНІ ЗМІНЮЮТЬСЯ +
	// ???
	//
	fmt.Printf("Оригінал:\n%s\n", weekleyMagazine.toString())
	fmt.Printf("Змінена копія:\n%s\n", weekleyMagazineCopy.toString())

	for _, article := range weekleyMagazine.GetCertainRatedArticles(8) {
		fmt.Printf("%s\n", article.ToString())
	}
	for _, article := range weekleyMagazine.GetCertainNamedArticles("days") {
		fmt.Printf("%s\n", article.ToString())
	}
	for _, article := range weekleyMagazineCopy.GetCertainNamedArticles("stro") {
		fmt.Printf("%s\n", article.ToString())
	}
}

// Метод для отримання імені з класу типу Person

func (e *Person) GetName() string {
	return e.firstName
}

// Метод для отримання прізвища з класу типу Person

func (e *Person) GetSurname() string {
	return e.surName
}

// Метод для отримання дати народження з класу типу Person

func (e *Person) GetDate() time.Time {
	return e.birthday
}

// Віртуальний метод

func (*Person) GetRating() float64 {
	panic("unimplemented")
}

// Метод для зміни імені в існуючому об'єкті типу Person

func (e *Person) SetName(name string) {
	e.firstName = name
}

// Метод для зміни прізвища в існуючому об'єкті типу Person

func (e *Person) SetSurname(surname string) {
	e.surName = surname
}

// Метод для зміни дня народження в існуючому об'єкті типу Person

func (e *Person) SetDate(date time.Time) {
	e.birthday = date
}

// Віртуальний метод

func (*Person) SetRating(rating float64) {
	panic("unimplemented")
}

// Повертає коротко інформацію про об'єкт типу Person

func (p *Person) ToShortString() string {
	return fmt.Sprintf("%s, %s, %v", p.firstName, p.surName, p.birthday)
}

func New(name string, surname string, date time.Time) *Person {
	e := &Person{name, surname, date}
	return e
}

// Повертає відформатовану інформацію про об'єкт типу Person

func (e *Person) ToString() string {
	return "Name: " + e.firstName + " Surname: " + e.surName + " Date: " + e.birthday.String()
}

// Реалізація для порівняння двох об'єктів типу Person

func (e *Person) Equals(other *Person) bool {
	if e.firstName == other.firstName &&
		e.surName == other.surName &&
		e.birthday == other.birthday {
		return true
	}
	return false
}

// Повертає копію об'єкта типу Person

func (p *Person) DeepCopy() Person {
	var newPerson Person
	newFirstName := p.firstName
	newSurName := p.surName
	newBirthday := p.birthday
	newPerson.firstName = newFirstName
	newPerson.surName = newSurName
	newPerson.birthday = newBirthday
	return newPerson
}

// Повертає значення Frequency відносно індексу

func GetFrequency(index int) int {
	switch index {
	case 0:
		return Weekly
	case 1:
		return Monthly
	case 2:
		return Yearly
	default:
		return 0
	}
}

// Повертає середній рейтинг для об'єкта типу Magazine
// Середній рейтинг вираховується шляхом отримання рейтингів з усіх статтів

func (m *Magazine) GetAverageRating() float64 {
	sum := 0.0
	for _, article := range m.articles {
		sum += article.rating
	}
	return sum / float64(len(m.articles))
}

// Повертає коротку інформацію про об'єкт типу Magazine

func (m *Magazine) toShortString() string {
	return fmt.Sprintf("Magazine: %s\nReleasing: %v\n",
		m.edition.ToString(), m.GetAverageRating())
}

// Повертає повну інформацію про об'єкт типу Magazine та про усі об'єкти типу Article

func (m *Magazine) toString() string {
	result := fmt.Sprintf("Magazine: %s\nReleasing: %d\n",
		m.edition.ToString(), m.frequency)
	for _, editor := range m.editors {
		result += fmt.Sprintf("Name: %s\nSurname: %s\nBirthday: %s\n", editor.firstName, editor.surName, editor.birthday)
	}
	for _, article := range m.articles {
		result += fmt.Sprintf("Author: %s\nArticle: %s, Rating: %.2f\n", article.author.ToShortString(), article.nameOfArticle, article.rating)
	}
	return result
}

// Змінює дані для статтей для об'єкта типу Magazine

func (m *Magazine) SetArticles(articles []Article) {
	m.articles = articles
}

// Додає нові статті для об'єкта типу Magazine

func (m *Magazine) AddArticle(article Article) {
	m.articles = append(m.articles, article)
}

// Встановює нове значення для редакторів

func (m *Magazine) AddEditors(editors []Person) {
	m.editors = editors
}

// Повертає копію об'єкта типу Magazine

func (m Magazine) DeepCopy() Magazine {
	var newMagazine Magazine
	newEdition := Edition{
		name:        m.edition.name,
		releaseDate: m.edition.releaseDate,
		edition:     m.edition.edition,
	}
	newEditors := make([]Person, 0, len(m.editors))
	for _, author := range m.editors {
		newEditors = append(newEditors, author.DeepCopy())
	}
	newArticles := make([]Article, 0, len(m.articles))
	for _, article := range m.articles {
		newArticles = append(newArticles, article.DeepCopy())
	}
	newMagazine.edition = newEdition
	newMagazine.editors = newEditors
	newMagazine.articles = newArticles
	return newMagazine
}

// Віртуальний метод

func (m *Magazine) GetRating() float64 {
	panic("")
}

// Віртуальний метод

func (m *Magazine) SetRating(rating float64) {
	panic("")
}

// Повертає edition

func (m *Magazine) GetEdition() Edition {
	return m.edition
}

// Повертає статті із рейтингом вище заданого значення

func (m *Magazine) GetCertainRatedArticles(rating float64) []Article {
	articles := make([]Article, 0, len(m.articles))
	for _, article := range m.articles {
		if article.rating > rating {
			articles = append(articles, article)
		}
	}
	return articles
}

// Повертає статті із назвою, що містить ключовий набір букв

func (m *Magazine) GetCertainNamedArticles(substr string) []Article {
	articles := make([]Article, 0, len(m.articles))
	for _, article := range m.articles {
		if strings.Contains(article.nameOfArticle, substr) {
			articles = append(articles, article)
		}
	}
	return articles
}

// Повертає копію об'єкта типу Article

func (a Article) DeepCopy() Article {
	var newArticle Article
	newAuthor := a.author
	newArticleName := a.nameOfArticle
	newRating := a.rating
	newArticle.author = newAuthor
	newArticle.nameOfArticle = newArticleName
	newArticle.rating = newRating
	return newArticle
}

func (a *Article) ToString() string {
	return fmt.Sprintf("%s\nArticle name: %s Rating: %v", a.author.ToString(), a.nameOfArticle, a.rating)
}

// Повертає рейтинг для об'єкту типу Article

func (e *Article) GetRating() float64 {
	return e.rating
}

// Встановлює нове значення для об'єкта типу Article

func (e *Article) SetRating(rating float64) {
	e.rating = rating
}

// Повертає назву видавництва

func (e *Edition) GetEditionName() string {
	return e.name
}

// Повертає дату випуску

func (e *Edition) GetReleaseDate() time.Time {
	return e.releaseDate
}

// Повертає тираж

func (e *Edition) GetEditionNumber() int {
	return e.edition
}

// Встановлює нове значення назви для об'єкта типу Edition

func (e *Edition) SetEditionName(name string) {
	e.name = name
}

// Встановлює нове значення дати випуску для об'єкта типу Edition

func (e *Edition) SetReleaseDate(date time.Time) {
	e.releaseDate = date
}

// Встановлює нове значення тиражу для об'єкта типу Edition

func (e *Edition) SetEditionNumber(edition int) (Edition, error) {
	if edition < 0 {
		return *e, errors.New("тираж не може набувати негативного значення")
	} else {
		e.edition = edition
		return *e, nil
	}
}

// Віртуальний метод

func (e *Edition) GetRating() float64 {
	panic("")
}

// Віртуальний метод

func (e *Edition) SetRating(rating float64) {
	panic("")
}

// Повертає копію об'єкта типу Edition

func (e *Edition) DeepCopy() Edition {
	var newEdition Edition
	newName := e.name
	newDate := e.releaseDate
	newEditionNumber := e.edition
	newEdition.SetEditionName(newName)
	newEdition.SetReleaseDate(newDate)
	newEdition.SetEditionNumber(newEditionNumber)
	return newEdition
}

func (e *Edition) ToString() string {
	return fmt.Sprintf("%s, %s, %d", e.name, e.releaseDate.String(), e.edition)
}
