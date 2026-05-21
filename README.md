# SYSTEM OBSŁUGI KLIENTA (CUSTOMER SERVICE SYSTEM)

Kompleksowy, wieloplatformowy system obsługi klienta i sprzedaży, składający się z centralnego serwera backendowego, zaawansowanego panelu administracyjnego (WPF) oraz mobilno-desktopowej aplikacji sklepowej dla klientów (.NET MAUI).

System integruje się z lokalną bazą danych SQL Server oraz zewnętrznym API produktowym w celu demonstracji pełnego cyklu życia zamówień i zarządzania danymi.

---

# ARCHITEKTURA SYSTEMU

Projekt został podzielony na trzy główne warstwy (monorepo / architektura wieloprojektowa):

## Backend Server (Web API)

Centralny punkt systemu odpowiedzialny za:

- logikę biznesową,
- autoryzację,
- synchronizację danych,
- komunikację z bazą danych MS SQL.

## WPF Desktop App (Dashboard)

Zaawansowany panel analityczno-zarządczy przeznaczony dla:

- pracowników BOK,
- administratorów systemu.

## .NET MAUI Multi-platform App (Sklep)

Aplikacja kliencka działająca na:

- Androidzie,
- iOS,
- Windows,
- macOS.

Integruje dane wewnętrzne z usługami zewnętrznymi.

---

# Schemat komunikacji

```text
                  +-----------------------------+
                  |     Baza Danych MS SQL      |
                  +--------------+--------------+
                                 |
                                 v

+------------------------+    +------+------+    +------------------------+
|  WPF Desktop Dashboard |<-->|  Web API   |<-->| MAUI Multi-platform App |
+------------------------+    |  (Serwer)  |    +-----------+------------+
                              +------+------+                |
                                     |                       |
                                     v                       |
                          +----------+-----------+           |
                          |      FakeStore API   |<----------+
                          +----------------------+
```

---

# KOMPONENTY I FUNKCJONALNOŚCI

# Serwer (ASP.NET Core Web API)

## Główne funkcjonalności

- ORM Entity Framework Core
- Architektura RESTful API
- JWT Authentication / Authorization
- Repozytoria i serwisy
- Synchronizacja danych klientów i zamówień

## Technologie backendowe

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Bearer Tokens

---

# Panel Administratora (WPF)

## Funkcjonalności

### Dashboard analityczny

- wykresy,
- statystyki,
- monitoring aktywności klientów.

### Zarządzanie CRM

Pełny zestaw operacji CRUD:

- Create
- Read
- Update
- Delete

### System ticketowy

Obsługa zgłoszeń klientów z poziomu aplikacji.

### Wzorzec MVVM

Czyste oddzielenie:

- warstwy wizualnej (XAML),
- logiki biznesowej.

Wykorzystano:

- `CommunityToolkit.Mvvm`

---

# Aplikacja Kliencka (.NET MAUI)

## Główne funkcje sklepu

- przeglądanie produktów,
- koszyk zakupowy,
- składanie zamówień,
- historia zakupów,
- status realizacji zamówienia.

## Integracja Hybrid API

### FakeStore API

Pobieranie:

- produktów,
- kategorii,
- zdjęć,
- cen,
- opisów.

### Własny serwer Web API

Synchronizacja:

- użytkowników,
- koszyka,
- historii zakupów,
- zamówień.

---

# TECHNOLOGIE I NARZĘDZIA

| Kategoria | Technologie |
|---|---|
| Platforma | .NET 10 |
| Język | C# 14 |
| Baza danych | Microsoft SQL Server |
| Backend | ASP.NET Core Web API |
| ORM | Entity Framework Core |
| Desktop | WPF |
| Mobile | .NET MAUI |
| API zewnętrzne | FakeStore API |
| MVVM | CommunityToolkit.Mvvm |
| Wykresy | LiveCharts2 / Microcharts |
| JSON | System.Text.Json / Newtonsoft.Json |

---

# STRUKTURA BAZY DANYCH (MSSQL)

## Customers

Przechowuje:

- dane klientów,
- loginy,
- zahaszowane hasła,
- statusy kont.

## Orders

Zawiera:

- nagłówki zamówień,
- status zamówienia,
- datę złożenia,
- powiązanie z klientem.

## OrderItems

Przechowuje:

- produkty zamówienia,
- ilości,
- ceny,
- identyfikatory produktów z FakeStore API.

## SupportTickets

System zgłoszeń klientów obsługiwanych przez panel WPF.

---

# INSTALACJA I KONFIGURACJA

# Wymagania wstępne

- Visual Studio 2022
- .NET Desktop Development
- ASP.NET and Web Development
- .NET MAUI Development
- Microsoft SQL Server
- Połączenie z internetem

---

# Krok 1 — Klonowanie repozytorium

```bash
git clone https://github.com/twoj-user/system-obslugi-klienta.git

cd system-obslugi-klienta
```

---

# Krok 2 — Konfiguracja bazy danych

Otwórz plik:

```text
appsettings.json
```

i ustaw poprawny `ConnectionString`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=CustomerServiceDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

Następnie uruchom migracje:

```powershell
Update-Database
```

---

# Krok 3 — Konfiguracja klientów (WPF i MAUI)

Ustaw adres URL lokalnego API.

## WPF

```text
https://localhost:7001/api/
```

## Android Emulator (.NET MAUI)

```text
https://10.0.2.2:7001/api/
```

---

# URUCHOMIENIE APLIKACJI

## 1. Uruchom serwer API

- ustaw projekt Web API jako startowy,
- uruchom aplikację (`F5`),
- sprawdź Swagger:

```text
https://localhost:7001/swagger
```

---

## 2. Uruchom aplikację WPF

Zaloguj się jako administrator i:

- zarządzaj klientami,
- przeglądaj zgłoszenia,
- analizuj statystyki.

---

## 3. Uruchom aplikację MAUI

Na emulatorze lub urządzeniu:

- zarejestruj konto,
- przeglądaj produkty,
- dodawaj do koszyka,
- składaj zamówienia.

Nowe zamówienia pojawią się automatycznie:

- w bazie MSSQL,
- w panelu WPF.

---

# PODSUMOWANIE

Projekt demonstruje kompletny ekosystem nowoczesnej aplikacji biznesowej opartej o platformę .NET:

- backend REST API,
- desktopowy panel administracyjny,
- aplikację mobilno-desktopową,
- integrację z SQL Server,
- komunikację z zewnętrznym API,
- architekturę MVVM,
- zarządzanie klientami i zamówieniami.

System stanowi solidną bazę pod rozwój pełnoprawnej platformy e-commerce i CRM.
