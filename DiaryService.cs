// DiaryService.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq; 

public class DiaryService
{
    private const string FilePath = "diary_data.json";

    public List<DiaryEntry> Entries { get; private set; } = new List<DiaryEntry>();

    public void AddEntry(DiaryEntry entry)
    {
        Entries.Add(entry);
    }

    
    public List<DiaryEntry> SearchEntries(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return new List<DiaryEntry>();
        }

        string lowerSearchTerm = searchTerm.ToLower();

        
        return Entries
            .Where(e => 
                e.Title.ToLower().Contains(lowerSearchTerm) ||
                e.Text.ToLower().Contains(lowerSearchTerm)
            )
            .ToList();
    }

    public bool UpdateEntry(int index, DiaryEntry updatedEntry)
    {
        if (index >= 0 && index < Entries.Count)
        {
            updatedEntry.Date = Entries[index].Date; 
            Entries[index] = updatedEntry;
            return true;
        }
        return false;
    }

    public bool DeleteEntry(int index)
    {
        if (index >= 0 && index < Entries.Count)
        {
            Entries.RemoveAt(index);
            return true;
        }
        return false;
    }

    public void LoadFromFile()
    {
        if (File.Exists(FilePath))
        {
            try
            {
                string json = File.ReadAllText(FilePath);
                var loadedEntries = JsonSerializer.Deserialize<List<DiaryEntry>>(json);

                if (loadedEntries != null)
                {
                    Entries = loadedEntries;
                    Console.WriteLine($"Laddade {Entries.Count} inlägg från fil.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid inläsning: {ex.Message}. Startar med tom lista.");
            }
        }
        else
        {
            Console.WriteLine("Ingen gammal dagboksfil hittades. Startar med tom lista.");
        }
    }

    public void SaveToFile()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(Entries, options);
            File.WriteAllText(FilePath, json);
            Console.WriteLine($"Sparade {Entries.Count} inlägg till filen.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid sparande: {ex.Message}");
        }
    }
}