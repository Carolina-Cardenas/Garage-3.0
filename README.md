# Garage 2.0

En konsolapplikation i C# för att hantera ett garage med en kapacitet som användaren väljer.

## Funktioner

- Skapa ett garage med valfri kapacitet.
- Läsa in exempelfordon vid start.
- Parkera och ta bort fordon med hjälp av registreringsnummer.
- Visa parkerade fordon och deras egenskaper.
- Visa antalet fordon av varje typ.
- Söka efter registreringsnummer utan att skilja på stora och små bokstäver.
- Kombinera sökfilter för registreringsnummer, färg, antal hjul och fordonstyp.
- Kontrollera användarens inmatning och visa tydliga meddelanden.

## Starta programmet

Du behöver .NET 8 SDK.

Öppna en terminal i projektets huvudmapp och kör:

```bash
dotnet run
```

Ange garagets kapacitet och välj om exempelfordon ska läggas till. Använd sedan menyn för att hantera garaget. Välj `0` i huvudmenyn för att avsluta.

## Klasser och ansvar

- **Vehicle:** basklass med registreringsnummer, färg och antal hjul.
- **Car:** ärver från Vehicle och lägger till bränsletyp.
- **Motorcycle:** ärver från Vehicle och lägger till cylindervolym.
- **Airplane:** ärver från Vehicle och lägger till antal motorer.
- **Bus:** ärver från Vehicle och lägger till antal sittplatser.
- **Boat:** ärver från Vehicle och lägger till längd.
- **Garage:** hanterar en privat List<Vehicle>, kapacitet, parkering, borttagning, sökning och räkning av fordon.
- **ConsoleUI:** hanterar menyer, inmatning och presentation av resultat.
- **Program:** startar applikationen genom ConsoleUI.

Fordonsklasserna finns i Models, Garage finns i Services och ConsoleUI finns i UI.

## Teknik och begrepp

C#, .NET 8, objektorienterad programmering, arv, inkapsling, generiska samlingar och felhantering. Logiken är implementerad utan LINQ.

## Önskad feedback inför code review

- Är ansvarsfördelningen mellan Garage och ConsoleUI tydlig?
- Är användningen av arv och egenskaper i fordonsklasserna tydlig?
- Hanteras garagets kapacitet och dubbla registreringsnummer korrekt?
- Är sökningen med flera valfria filter lätt att förstå?
- Finns det inmatningar som inte hanteras korrekt?
- Finns det upprepad kod som kan förenklas utan att göra lösningen svårare att förstå?