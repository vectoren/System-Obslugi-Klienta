using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SOK_WPF.Models;
using System.Diagnostics;
using System.Windows.Controls;

namespace SOK_WPF.ViewModels
{
    public partial class OrdersIssuesVM : ObservableObject
    {
        [ObservableProperty]
        List<ClientIssue>? issuesList = new List<ClientIssue>()
        {
             new()
    {
        Id = 1,
        IssueTopic = "Uszkodzona paczka w transporcie",
        ClientId = 101,
        ClientMail = "jan.kowalski@example.com",
        OrderId = 5001,
        IssueStatus = "Nowe",
        IssuedOn = new DateOnly(2026, 5, 20),
        AffectedProducts = [ new() { Id = 10, Name = "Monitor LED 24", Description = "Monitor biurowy full HD" } ],
        Description = "Karton przyszedł rozerwany, matryca monitora jest pęknięta.",
        Expectations = "Wymiana sprzętu na nowy lub zwrot gotówki."
    },
    new()
    {
        Id = 2,
        IssueTopic = "Brakujący element w zamówieniu",
        ClientId = 102,
        ClientMail = "anna.nowak@example.com",
        OrderId = 5002,
        IssueStatus = "W toku",
        IssuedOn = new DateOnly(2026, 5, 21),
        AffectedProducts = [ new() { Id = 11, Name = "Biurko Gamingowe", Description = "Biurko z podświetleniem LED" } ],
        Description = "W paczce brakowało śrub montażowych oraz instrukcji.",
        Expectations = "Dosłanie brakujących elementów montażowych."
    },
    new()
    {
        Id = 3,
        IssueTopic = "Opóźnienie w wysyłce",
        ClientId = 103,
        ClientMail = "tomasz.wisniewski@example.com",
        OrderId = 5003,
        IssueStatus = "Nowe",
        IssuedOn = new DateOnly(2026, 5, 22),
        AffectedProducts = [ new() { Id = 12, Name = "Buty do biegania", Description = "Obuwie sportowe męskie" } ],
        Description = "Zamówienie opłacone 5 dni temu, status nadal nie uległ zmianie.",
        Expectations = "Informacja o planowanej dacie dostawy."
    },
    new()
    {
        Id = 4,
        IssueTopic = "Błędny rozmiar produktu",
        ClientId = 104,
        ClientMail = "katarzyna.wojcik@example.com",
        OrderId = 5004,
        IssueStatus = "Rozwiązane",
        IssuedOn = new DateOnly(2026, 5, 18),
        AffectedProducts = [ new() { Id = 13, Name = "Kurtka Zimowa", Description = "Kurtka puchowa z kapturem" } ],
        Description = "Zamówiłam rozmiar M, otrzymałam rozmiar XL.",
        Expectations = "Wymiana kurtki na poprawny rozmiar M."
    },
    new()
    {
        Id = 5,
        IssueTopic = "Problem z działaniem kodu rabatowego",
        ClientId = 105,
        ClientMail = "michal.lewandowski@example.com",
        OrderId = 5005,
        IssueStatus = "Odrzucone",
        IssuedOn = new DateOnly(2026, 5, 19),
        AffectedProducts = [],
        Description = "Kod promocyjny 'START10' nie naliczył zniżki w koszyku.",
        Expectations = "Zwrot różnicy wartości zamówienia (10%)."
    },
    new()
    {
        Id = 6,
        IssueTopic = "Urządzenie nie włącza się",
        ClientId = 106,
        ClientMail = "magda.kaminska@example.com",
        OrderId = 5006,
        IssueStatus = "W toku",
        IssuedOn = new DateOnly(2026, 5, 23),
        AffectedProducts = [ new() { Id = 14, Name = "Ekspres do kawy", Description = "Automatyczny ekspres ciśnieniowy" } ],
        Description = "Po podłączeniu do prądu ekspres nie reaguje na włącznik.",
        Expectations = "Naprawa gwarancyjna lub wymiana."
    },
    new()
    {
        Id = 7,
        IssueTopic = "Dostarczono niewłaściwy produkt",
        ClientId = 107,
        ClientMail = "piotr.zielinski@example.com",
        OrderId = 5007,
        IssueStatus = "Nowe",
        IssuedOn = new DateOnly(2026, 5, 24),
        AffectedProducts = [ new() { Id = 15, Name = "Słuchawki Bezprzewodowe", Description = "Słuchawki douszne ANC" } ],
        Description = "Zamiast słuchawek czarnych otrzymałem wersję białą.",
        Expectations = "Wymiana na kolor zgodny z zamówieniem."
    },
    new()
    {
        Id = 8,
        IssueTopic = "Podwójne pobranie płatności",
        ClientId = 108,
        ClientMail = "monika.szymanska@example.com",
        OrderId = 5008,
        IssueStatus = "W toku",
        IssuedOn = new DateOnly(2026, 5, 25),
        AffectedProducts = [],
        Description = "System płatności PayU pobrał kwotę zamówienia dwukrotnie z konta.",
        Expectations = "Zwret nadpłaconej kwoty na konto bankowe."
    },
    new()
    {
        Id = 9,
        IssueTopic = "Wada fabryczna - porysowana obudowa",
        ClientId = 109,
        ClientMail = "lukasz.wozniak@example.com",
        OrderId = 5009,
        IssueStatus = "Nowe",
        IssuedOn = new DateOnly(2026, 5, 26),
        AffectedProducts = [ new() { Id = 16, Name = "Smartfon 5G", Description = "Smartfon z ekranem AMOLED" } ],
        Description = "Telefon wyjęty prosto z zafoliowanego pudełka posiada rysę na pleckach.",
        Expectations = "Rabat cenowy lub wymiana na egzemplarz bez wad."
    },
    new()
    {
        Id = 10,
        IssueTopic = "Rezygnacja z zamówienia (zwrot 14 dni)",
        ClientId = 110,
        ClientMail = "natalia.koziol@example.com",
        OrderId = 5010,
        IssueStatus = "Rozwiązane",
        IssuedOn = new DateOnly(2026, 5, 15),
        AffectedProducts = [
            new() { Id = 17, Name = "Klawiatura Mechaniczna", Description = "Klawiatura RGB" },
            new() { Id = 18, Name = "Mysz Bezprzewodowa", Description = "Mysz ergonomiczna" }
        ],
        Description = "Chcę skorzystać z prawa do zwrotu towaru bez podania przyczyny.",
        Expectations = "Zwrot pełnej kwoty po odesłaniu paczki."
    }
        };
        public Frame MainFrame { get; set; }

        [ObservableProperty]
        ClientIssue? selectedIssue;

        public OrdersIssuesVM(Frame frame)
        {
            MainFrame = frame;
        }
        [RelayCommand]
        public void GoBack() => MainFrame.GoBack();
        [RelayCommand]
        public void ClientContact() => Process.Start(new ProcessStartInfo() { FileName = $"mailto:{SelectedIssue?.ClientMail}?subject=Dotyczy zamówienia nr {SelectedIssue?.OrderId}", UseShellExecute = true });
    }
}
