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
            Console.WriteLine($"2. Lista alla inlägg ({_diaryService.Entries.Count} st) (Inkl. Detaljvy)"); 
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

    // Metod för att lista alla inlägg och sedan låta användaren välja en detaljvy
    static void ListEntries()
    {
        Console.Clear();
        Console.WriteLine("--- Alla Dagboksinlägg ---");

        if (_diaryService.Entries.Count == 0)
        {
            Console.WriteLine("Inga inlägg hittades.");
            Console.WriteLine("\nTryck valfri tangent för att återgå till menyn.");
            Console.ReadKey(true);
            return; // Avslutar metoden om listan är tom
        }

        // Loppar igenom listan och skriver ut (titel och datum)
        for (int i = 0; i < _diaryService.Entries.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_diaryService.Entries[i]}"); 
        }

        // NY LOGIK: Fråga användaren vilket inlägg de vill se
        Console.WriteLine("\nAnge numret på inlägget du vill se (eller tryck Enter för att gå tillbaka):");
        string input = Console.ReadLine();

        // Försöker konvertera input till ett nummer och validerar indexet
        if (int.TryParse(input, out int index) && index > 0 && index <= _diaryService.Entries.Count)
        {
            // Gå till Detaljvy för valt inlägg (index - 1 eftersom listor är 0-baserade)
            ViewEntry(index - 1); 
            
            // Efter ViewEntry, visar vi listan igen (för att inte hoppa direkt till menyn)
            ListEntries();
        }
        else if (string.IsNullOrEmpty(input))
        {
            // Går tillbaka till menyn (gör ingenting)
        }
        else
        {
            Console.WriteLine("Ogiltigt val. Tryck valfri tangent.");
            Console.ReadKey(true);
        }
    }

    // NY METOD: Visa hela inlägget
    static void ViewEntry(int index)
    {
        DiaryEntry entry = _diaryService.Entries[index];

        Console.Clear();
        Console.WriteLine($"--- Detaljer: {entry.Title} ---");
        Console.WriteLine($"Datum: {entry.Date.ToString("yyyy-MM-dd HH:mm")}");
        Console.WriteLine("-----------------------------------");
        Console.WriteLine(entry.Text); // Hela inläggets innehåll
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("\nTryck valfri tangent för att återgå till listan.");
        Console.ReadKey(true);
    }
}