using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Media
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int YearPublished { get; set; }
    public bool IsAvailable { get; set; } = true;

    protected Media(string title, string author, int yearPublished)
    {
        Title = title;
        Author = author;
        YearPublished = yearPublished;
    }

    public override string ToString()
    {
        return $"Название: {Title}, Автор: {Author}, Год: {YearPublished}, Доступность: {IsAvailable}";
    }
}

public class Book : Media
{
    public int PageCount { get; set; }
    public string Genre { get; set; }

    public Book(string title, string author, int yearPublished, int pageCount, string genre)
        : base(title, author, yearPublished)
    {
        PageCount = pageCount;
        Genre = genre;
    }

    public override string ToString()
    {
        return base.ToString() + $", Страниц: {PageCount}, Жанр: {Genre}";
    }
}

public class Movie : Media
{
    public int Duration { get; set; }
    public string Director { get; set; }

    public Movie(string title, string author, int yearPublished, int duration, string director)
        : base(title, author, yearPublished)
    {
        Duration = duration;
        Director = director;
    }

    public override string ToString()
    {
        return base.ToString() + $", Длительность: {Duration} мин., Режиссер: {Director}";
    }
}

public class MusicAlbum : Media
{
    public string Artist { get; set; }
    public int TrackCount { get; set; }

    public MusicAlbum(string title, string author, int yearPublished, string artist, int trackCount)
        : base(title, author, yearPublished)
    {
        Artist = artist;
        TrackCount = trackCount;
    }

    public override string ToString()
    {
        return base.ToString() + $", Исполнитель: {Artist}, Треков: {TrackCount}";
    }
}

public class Library<T> where T : Media
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        if (items.Any(i => i.Title == item.Title))
        {
            Console.WriteLine("Элемент с таким названием уже существует.");
            return;
        }
        items.Add(item);
    }

    public bool Remove(string title)
    {
        var item = items.FirstOrDefault(i => i.Title == title);
        if (item != null)
        {
            items.Remove(item);
            return true;
        }
        return false;
    }

    public T FindByTitle(string title)
    {
        return items.FirstOrDefault(i => i.Title == title);
    }

    public void PrintAll()
    {
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
}

class Program
{
    static void Main()
    {
        Library<Book> bookLibrary = new Library<Book>();
        Library<Movie> movieLibrary = new Library<Movie>();
        Library<MusicAlbum> musicLibrary = new Library<MusicAlbum>();

        while (true)
        {
            Console.WriteLine("\nВыберите действие: 1-Добавить, 2-Удалить, 3-Найти, 4-Вывести все, 5-Выход");
            string choice = Console.ReadLine();
            if (choice == "5") break;

            Console.WriteLine("Выберите тип: 1-Книга, 2-Фильм, 3-Музыкальный альбом");
            string typeChoice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1": 
                        Console.Write("Введите название: ");
                        string title = Console.ReadLine();
                        Console.Write("Введите автора: ");
                        string author = Console.ReadLine();
                        Console.Write("Введите год выпуска: ");
                        int year = int.Parse(Console.ReadLine());

                        if (typeChoice == "1")
                        {
                            Console.Write("Введите кол-во страниц: ");
                            int pages = int.Parse(Console.ReadLine());
                            Console.Write("Введите жанр: ");
                            string genre = Console.ReadLine();
                            bookLibrary.Add(new Book(title, author, year, pages, genre));
                        }
                        else if (typeChoice == "2")
                        {
                            Console.Write("Введите длительность в минутах: ");
                            int duration = int.Parse(Console.ReadLine());
                            Console.Write("Введите режиссера: ");
                            string director = Console.ReadLine();
                            movieLibrary.Add(new Movie(title, author, year, duration, director));
                        }
                        else if (typeChoice == "3")
                        {
                            Console.Write("Введите исполнителя: ");
                            string artist = Console.ReadLine();
                            Console.Write("Введите количество треков: ");
                            int tracks = int.Parse(Console.ReadLine());
                            musicLibrary.Add(new MusicAlbum(title, author, year, artist, tracks));
                        }
                        break;

                    case "2": 
                        Console.Write("Введите название: ");
                        string removeTitle = Console.ReadLine();
                        bool removed = typeChoice == "1" ? bookLibrary.Remove(removeTitle) :
                                       typeChoice == "2" ? movieLibrary.Remove(removeTitle) :
                                       musicLibrary.Remove(removeTitle);
                        Console.WriteLine(removed ? "Удалено." : "Не найдено.");
                        break;

                    case "3": 
                        Console.Write("Введите название: ");
                        string findTitle = Console.ReadLine();
                        Media foundItem = typeChoice == "1" ? bookLibrary.FindByTitle(findTitle) :
                                         typeChoice == "2" ? movieLibrary.FindByTitle(findTitle) :
                                         musicLibrary.FindByTitle(findTitle);
                        Console.WriteLine(foundItem != null ? foundItem.ToString() : "Не найдено.");
                        break;

                    case "4": 
                        Console.WriteLine("Книги:"); bookLibrary.PrintAll();
                        Console.WriteLine("\nФильмы:"); movieLibrary.PrintAll();
                        Console.WriteLine("\nМузыкальные альбомы:"); musicLibrary.PrintAll();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }
    }
}
