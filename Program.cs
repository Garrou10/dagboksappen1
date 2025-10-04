// Program.cs
using System;
using System.Linq; // Behövs för att hantera textinmatning smidigare

class Program
{
    // Skapar en instans av tjänsten som används genom hela programmet
    static DiaryService _diaryService = new DiaryService(); 

    static void Main(string[] args)
    {
        Console.Title = "Dagboksappen";
        
        // Ladda in sparade inlägg direkt vid start
        _diaryService.LoadFromFile(); 

        RunMenu();
    }

    static void RunMenu()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("--- Dagboksappen - Meny ---");
            Console.WriteLine("1. Lägg till nytt inlägg");
            // Visar antalet inlägg direkt i menyn
            Console.WriteLine($"2. Lista alla inlägg ({_diaryService.Entries.Count} st)"); 
            Console.WriteLine("3. Avsluta");
            Console.Write("Välj ett alternativ: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddEntry(); 
                    break;
                case "2":
                    ListEntries(); 
                    break;
                case "3":
                    isRunning = false;
                    // Spara till fil innan programmet stängs
                    _diaryService.SaveToFile();
                    Console.WriteLine("\nSparar och avslutar...");
                    break;
                default:
                    Console.WriteLine("\nOgiltigt val. Tryck valfri tangent för att fortsätta.");
                    Console.ReadKey(true);
                    break;
            }
        }
    }

    // Metod för att lägga till ett nytt inlägg
    static void AddEntry()
    {
        Console.Clear();
        Console.WriteLine("--- Lägg till nytt inlägg ---");

        Console.Write("Titel: ");
        string title = Console.ReadLine();

        Console.WriteLine("Innehåll (avsluta med en tom rad):");
        string text = "";
        string line;
        
        // Läser rader tills användaren trycker Enter på en tom rad
        while (!string.IsNullOrEmpty(line = Console.ReadLine()))
        {
            text += line + Environment.NewLine;
        }

        DiaryEntry newEntry = new DiaryEntry 
        { 
            Title = title, 
            Text = text.Trim() 
        };

        _diaryService.AddEntry(newEntry);
        Console.WriteLine("\nInlägg sparat! Tryck valfri tangent.");
        Console.ReadKey(true);
    }

    // Metod för att lista alla inlägg
    static void ListEntries()
    {
        Console.Clear();
        Console.WriteLine("--- Alla Dagboksinlägg ---");

        if (_diaryService.Entries.Count == 0)
        {
            Console.WriteLine("Inga inlägg hittades.");
        }
        else
        {
            // Loppar igenom listan och skriver ut (använder ToString() från DiaryEntry)
            for (int i = 0; i < _diaryService.Entries.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_diaryService.Entries[i]}"); 
            }
        }

        Console.WriteLine("\nTryck valfri tangent för att återgå till menyn.");
        Console.ReadKey(true);
    }
}