// DiaryEntry.cs
using System;

public class DiaryEntry
{
    public DateTime Date { get; set; } = DateTime.Now;
    public string Title { get; set; }
    public string Text { get; set; }

    public override string ToString()
    {
        return $"[{Date.ToString("yyyy-MM-dd")}] {Title}";
    }
}