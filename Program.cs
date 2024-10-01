using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task ProcessNamesAsync(string[] names)
    {
        await Task.Run(() =>
        {
            var duplicates = names.GroupBy(name => name)
                                  .Where(group => group.Count() > 1)
                                  .Select(group => group.Key)
                                  .ToList();

            if (duplicates.Any())
            {
                throw new ArgumentException($"Найдены повторяющиеся имена: {string.Join(", ", duplicates)}");
            }
            else
            {
                Console.WriteLine($"Имена: {string.Join(", ", names)}");
            }
        });
    }

    static async Task Main()
    {
        List<Exception> exceptions = new List<Exception>();

        // Наборы имен
        string[] names1 = { "Иван", "Мария", "Петр", "Иван" }; // Есть дубликат
        string[] names2 = { "Анна", "Ольга", "Виктор" };       // Без дубликатов
        string[] names3 = { "Игорь", "Сергей", "Игорь" };       // Есть дубликат

        try
        {
            await ProcessNamesAsync(names1);
        }
        catch (Exception ex)
        {
            exceptions.Add(ex);
        }

        try
        {
            await ProcessNamesAsync(names2);
        }
        catch (Exception ex)
        {
            exceptions.Add(ex);
        }

        try
        {
            await ProcessNamesAsync(names3);
        }
        catch (Exception ex)
        {
            exceptions.Add(ex);
        }

        if (exceptions.Any())
        {
            Console.WriteLine("\nОшибки:");
            foreach (var ex in exceptions)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.WriteLine("\nОшибок не обнаружено.");
        }
    }
}
