// Program.cs
using System;
using System.Linq; 

class Program
{
    static DiaryService _diaryService = new DiaryService(); 

    static void Main(string[] args)
    {
        Console.Title = "Dagboksappen";
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
            Console.WriteLine($"2. Lista/Visa inlägg ({_diaryService.Entries.Count} st)"); 
            Console.WriteLine("3. Ta bort ett inlägg"); 
            Console.WriteLine("4. Redigera ett inlägg"); // NYTT MENYVAL
            Console.WriteLine("5. Avsluta"); // Avsluta flyttad till 5
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
                    RemoveEntry();
                    break;
                case "4": // HANTERAR REDIGERA
                    EditEntry();
                    break;
                case "5": // HANTERAR AVSLUTA
                    isRunning = false;
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

    static void AddEntry()
    {
        Console.Clear();
        Console.WriteLine("--- Lägg till nytt inlägg ---");
        Console.Write("Titel: ");
        string title = Console.ReadLine();

        Console.WriteLine("Innehåll (avsluta med en tom rad):");
        string text = "";
        string line;
        
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

    static void ListEntries()
    {
        Console.Clear();
        Console.WriteLine("--- Alla Dagboksinlägg ---");

        if (_diaryService.Entries.Count == 0)
        {
            Console.WriteLine("Inga inlägg hittades.");
            Console.WriteLine("\nTryck valfri tangent för att återgå till menyn.");
            Console.ReadKey(true);
            return; 
        }

        for (int i = 0; i < _diaryService.Entries.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_diaryService.Entries[i]}"); 
        }

        Console.WriteLine("\nAnge numret på inlägget du vill se (eller tryck Enter för att gå tillbaka):");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int index) && index > 0 && index <= _diaryService.Entries.Count)
        {
            ViewEntry(index - 1); 
            ListEntries();
        }
        else if (string.IsNullOrEmpty(input))
        {
           
        }
        else
        {
            Console.WriteLine("Ogiltigt val. Tryck valfri tangent.");
            Console.ReadKey(true);
        }
    }

    static void ViewEntry(int index)
    {
        DiaryEntry entry = _diaryService.Entries[index];

        Console.Clear();
        Console.WriteLine($"--- Detaljer: {entry.Title} ---");
        Console.WriteLine($"Datum: {entry.Date.ToString("yyyy-MM-dd HH:mm")}");
        Console.WriteLine("-----------------------------------");
        Console.WriteLine(entry.Text); 
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("\nTryck valfri tangent för att återgå till listan.");
        Console.ReadKey(true);
    }

    static void RemoveEntry()
    {
        Console.Clear();
        Console.WriteLine("--- Ta bort inlägg ---");

        if (_diaryService.Entries.Count == 0)
        {
            Console.WriteLine("Inga inlägg att ta bort.");
            Console.WriteLine("\nTryck valfri tangent för att återgå till menyn.");
            Console.ReadKey(true);
            return;
        }
        
        for (int i = 0; i < _diaryService.Entries.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_diaryService.Entries[i]}"); 
        }

        Console.WriteLine("\nAnge numret på inlägget du vill ta bort (eller tryck Enter för att avbryta):");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int index) && index > 0 && index <= _diaryService.Entries.Count)
        {
            if (_diaryService.DeleteEntry(index - 1))
            {
                Console.WriteLine($"\nInlägg {index} har tagits bort! Tryck valfri tangent.");
            }
            else
            {
                Console.WriteLine("\nKunde inte ta bort inlägget.");
            }
        }
        else if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("\nÅtgärd avbruten.");
        }
        else
        {
            Console.WriteLine("\nOgiltigt val.");
        }
        Console.ReadKey(true);
    }
    
    
    static void EditEntry()
    {
        Console.Clear();
        Console.WriteLine("--- Redigera inlägg ---");

        if (_diaryService.Entries.Count == 0)
        {
            Console.WriteLine("Inga inlägg att redigera.");
            Console.WriteLine("\nTryck valfri tangent för att återgå till menyn.");
            Console.ReadKey(true);
            return;
        }
        
        for (int i = 0; i < _diaryService.Entries.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_diaryService.Entries[i]}"); 
        }

        Console.WriteLine("\nAnge numret på inlägget du vill redigera (eller tryck Enter för att avbryta):");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int index) && index > 0 && index <= _diaryService.Entries.Count)
        {
            int entryIndex = index - 1;
            DiaryEntry originalEntry = _diaryService.Entries[entryIndex];

            Console.WriteLine($"\nRedigerar inlägg {index}: '{originalEntry.Title}'");
            
            
            Console.Write($"Ny titel (lämna tom för att behålla '{originalEntry.Title}'): ");
            string newTitle = Console.ReadLine();
            if (string.IsNullOrEmpty(newTitle))
            {
                newTitle = originalEntry.Title;
            }

            
            Console.WriteLine("\nNytt innehåll (lämna tomt för att behålla befintligt, skriv nytt och avsluta med tom rad):");
            string newText = "";
            string line;
            
            
            while (!string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                newText += line + Environment.NewLine;
            }
            
            if (string.IsNullOrEmpty(newText))
            {
                newText = originalEntry.Text;
            }
            
            
            DiaryEntry updatedEntry = new DiaryEntry
            {
                Title = newTitle,
                Text = newText.Trim()
            };

            
            if (_diaryService.UpdateEntry(entryIndex, updatedEntry))
            {
                Console.WriteLine($"\nInlägg {index} har uppdaterats! Tryck valfri tangent.");
            }
            else
            {
                Console.WriteLine("\nKunde inte uppdatera inlägget.");
            }
        }
        else if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("\nÅtgärd avbruten.");
        }
        else
        {
            Console.WriteLine("\nOgiltigt val.");
        }
        Console.ReadKey(true);
    }
}