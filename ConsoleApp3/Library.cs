using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Library<T> : IMediaManager<T> where T : Media
    {
        private List<T> mediaCollections = new List<T>();
        private Dictionary<string, T> mediaDictionary = new Dictionary<string, T>();

        public void Add(T item)
        {
            if (mediaDictionary.ContainsKey(item.Title))
            {
                throw new Exception("Item with this title already exists.");
            }
            mediaCollections.Add(item);
            mediaDictionary[item.Title] = item;
        }

        public bool Remove(string title)
        {
            if (!mediaDictionary.ContainsKey(title))
            {
                throw new Exception("Элемент уже существует");
            }
            T item = mediaDictionary[title];
            mediaCollections.Remove(item);
            mediaDictionary.Remove(title);
            return true;
        }

        public T FindByTitle(string title)
        {
            if (!mediaDictionary.TryGetValue(title, out T item))
            {
                throw new Exception("Элемент не найден");
            }
            return item;
        }

        public IEnumerable<T> FilterByYear(int year)
        {
            return mediaCollections.Where(Collections => Collections.YearPublished == year);
        }

        public IEnumerable<T> GetAllAvailable()
        {
            return mediaCollections.Where(Collections => Collections.IsAvailable);
        }

        public void PrintAll()
        {
            foreach (var media in mediaCollections)
            {
                Console.WriteLine($"{media.GetType().Name}: {media.Title}, Автор: {media.Author}, Год: {media.YearPublished}, Доступно: {media.IsAvailable}");
            }
        }

        public void Borrow(string title)
        {
            T item = FindByTitle(title);
            if (!item.IsAvailable)
                throw new Exception("Элемент нельзя выдать");

            item.IsAvailable = false;
            Console.WriteLine($"Выдан элемент: {title}");
        }

        public void Return(string title)
        {
            T item = FindByTitle(title);
            if (item.IsAvailable)
                throw new Exception("Элемент уже возвращен");

            item.IsAvailable = true;
            Console.WriteLine($"Возвращен элемент: {title}");
        }
    }

}
