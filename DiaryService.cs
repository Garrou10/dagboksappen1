// DiaryService.cs
using System;
using System.Collections.Generic;
using System.IO;        // Behövs för File.Exists/ReadAllText/WriteAllText
using System.Text.Json; // Behövs för JsonSerializer

public class DiaryService
{
    // Konstanter för filnamn. Filen kommer att skapas i samma mapp som .exe-filen.
    private const string FilePath = "diary_data.json";

    // Huvudlistan som håller alla inlägg i minnet
    public List<DiaryEntry> Entries { get; private set; } = new List<DiaryEntry>();

    public void AddEntry(DiaryEntry entry)
    {
        Entries.Add(entry);
    }

    public void LoadFromFile()
    {
        if (File.Exists(FilePath))
        {
            try
            {
                // 1. Läser all text (JSON) från filen
                string json = File.ReadAllText(FilePath);
                
                // 2. Konverterar JSON-strängen till en C# List<DiaryEntry>
                var loadedEntries = JsonSerializer.Deserialize<List<DiaryEntry>>(json);

                if (loadedEntries != null)
                {
                    Entries = loadedEntries;
                    Console.WriteLine($"Laddade {Entries.Count} inlägg från fil.");
                }
            }
            catch (Exception ex)
            {
                // Om filen är korrupt eller det uppstår ett annat fel
                Console.WriteLine($"Fel vid inläsning: {ex.Message}. Startar med tom lista.");
            }
        }
        else
        {
            // Meddelande om ingen fil hittades vid första körningen
            Console.WriteLine("Ingen gammal dagboksfil hittades. Startar med tom lista.");
        }
    }

    public void SaveToFile()
    {
        try
        {
            // 1. Konverterar C# List<DiaryEntry> till en JSON-sträng
            var options = new JsonSerializerOptions { WriteIndented = true }; // Gör JSON-filen läsbar
            string json = JsonSerializer.Serialize(Entries, options);

            // 2. Skriver JSON-strängen till filen
            File.WriteAllText(FilePath, json);
            Console.WriteLine($"Sparade {Entries.Count} inlägg till filen.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid sparande: {ex.Message}");
        }
    }
}