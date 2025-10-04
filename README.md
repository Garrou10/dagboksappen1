# Dagboksappen 1 (CRUD-applikation med persistens)

## Projektbeskrivning
Detta är en enkel konsolapplikation (CLI) utvecklad i C# (.NET) som fungerar som en personlig dagbok. Appen stöder full **CRUD**-funktionalitet (Create, Read, Update, Delete) samt en sökfunktion.

All data sparas lokalt i en JSON-fil (`diary_data.json`) för att säkerställa att inläggen finns kvar mellan sessioner (persistens).

## Funktioner
Applikationen har följande menyval och funktioner:
* **1. Lägg till nytt inlägg (Create):** Skapa ett nytt dagboksinlägg med titel och text.
* **2. Lista/Visa inlägg (Read):** Visar en lista över alla inlägg och tillåter detaljvy.
* **3. Ta bort ett inlägg (Delete):** Ta bort ett befintligt inlägg.
* **4. Redigera ett inlägg (Update):** Ändra titel och textinnehåll i ett befintligt inlägg.
* **5. Sök inlägg:** Filtrera inläggen genom att söka efter ord i både titeln och texten.
* **6. Avsluta:** Sparar alla ändringar till disk och stänger applikationen.

## Hur man kör applikationen
1.  Se till att du har [.NET SDK](https://dotnet.microsoft.com/download) installerat på din maskin.
2.  Klona eller ladda ner detta repo.
3.  Öppna en terminal i projektmappen (`dagboksappen1/`).
4.  Starta appen med kommandot:
    ```bash
    dotnet run
    ```