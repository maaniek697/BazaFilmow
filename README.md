# 🎬 Baza Filmów (Movie Database App)

Nowoczesna aplikacja okienkowa (Desktop App) napisana w języku C# przy użyciu frameworka WPF i platformy .NET 8.0. Program pozwala na wyszukiwanie informacji o filmach w czasie rzeczywistym oraz tworzenie własnej, trwałej kolekcji.

## ✨ Główne funkcje

* **Integracja z zewnętrznym API:** Aplikacja łączy się z bazą OMDB API, pobierając dane w formacie JSON (tytuł, rok, ocena IMDb, opis fabuły).
* **Pobieranie obrazów z sieci:** Automatyczne renderowanie plakatów filmowych bezpośrednio z adresów URL.
* **Asynchroniczność:** Wykorzystanie `async/await` oraz `HttpClient`, co gwarantuje płynne działanie interfejsu (brak zawieszeń) podczas oczekiwania na odpowiedź z serwera.
* **Trwały zapis danych:** Możliwość dodania wyszukanego filmu do "Kolekcji". Dane są dopisywane do lokalnego pliku tekstowego (`MojeFilmy.txt`).
* **Dynamiczny interfejs:** Automatyczne odczytywanie bazy przy starcie programu i wyświetlanie zapisanych filmów na zintegrowanej liście (`ListBox`).
* **Nowoczesny UI/UX:** Ciemny motyw (Dark Mode) zaprojektowany w XAML, wygodne wyszukiwanie klawiszem `Enter` oraz ukrywanie elementów interfejsu w zależności od kontekstu.

## 🛠️ Technologie

* **Język:** C# 12
* **Framework:** WPF (Windows Presentation Foundation) / .NET 8.0
* **Biblioteki:** `System.Net.Http`, `System.Text.Json`, `System.IO`
* **Architektura:** Klient-Serwer, REST API

## 🚀 Jak uruchomić projekt lokalnie?

1. Sklonuj repozytorium na swój komputer:
   ```bash
   git clone [https://github.com/TwojaNazwaUzytkownika/BazaFilmow.git](https://github.com/TwojaNazwaUzytkownika/BazaFilmow.git)

Otwórz plik rozwiązania (.sln) w IDE (np. JetBrains Rider lub Visual Studio).

Ważne: Aby wyszukiwanie działało, potrzebujesz darmowego klucza API ze strony OMDB API.

Otwórz plik MainWindow.xaml.cs i podmień zmienną kluczApi:

private readonly string kluczApi = "TWÓJ_KLUCZ_API";
