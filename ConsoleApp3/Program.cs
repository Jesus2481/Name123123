using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library<Media> library = new Library<Media>();

            try
            {
                library.Add(new Book("1964", "Лобанов. Р.В", 1964, true, 800, "Кухня"));
                library.Add(new Movie("во все тяжкие", "волтер вайт", 1996, true, TimeSpan.FromHours(3.32), "волтер вайт"));
                library.Add(new MusicAlbum("красный флаг", "моргенштерн", 2025, true, "моргенштерн", 3));

                library.PrintAll();

                var availableMedia = library.GetAllAvailable();
                Console.WriteLine("\nДоступная медия:");
                foreach (var media in availableMedia)
                {
                    Console.WriteLine($"{media.Title} by {media.Author} ({media.YearPublished})");
                }

                library.Borrow("\n1999");
                Console.WriteLine("\nПосле '1999':");
                library.PrintAll();

                library.Return("\n1999");
                Console.WriteLine("\nпосле возвращения '1999':");
                library.PrintAll();

                var filteredMedia = library.FilterByYear(2010);
                Console.WriteLine("\nМедия выпущенная в 2010:");
                foreach (var media in filteredMedia)
                {
                    Console.WriteLine($"{media.Title} by {media.Author} ({media.YearPublished})");
                }

                library.Remove("\nПервая встречная");
                Console.WriteLine("\nПосле 'Первая встречная':");
                library.PrintAll();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nOwmбкa: {ex.Message}");
            }
        }
    }
}
