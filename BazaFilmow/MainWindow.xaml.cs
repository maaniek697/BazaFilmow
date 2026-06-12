using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BazaFilmow
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient klientHttp = new HttpClient();
        
        // WKLEJ SWÓJ KLUCZ PONIŻEJ!
        private readonly string kluczApi = "TUTAJ_WKLEJ_SWOJ_KLUCZ"; 
        
        // Zmienna trzymająca nazwę naszego pliku, żeby nie wpisywać jej ręcznie za każdym razem
        private readonly string sciezkaPliku = "MojeFilmy.txt";

        public MainWindow()
        {
            InitializeComponent();
            
            // Ładujemy listę od razu przy starcie aplikacji!
            OdswiezListeFilmow(); 
        }

        // NOWOŚĆ: Funkcja do czytania pliku i ładowania go na ekran
        private void OdswiezListeFilmow()
        {
            // Sprawdzamy, czy plik w ogóle istnieje (żeby program nie wyrzucił błędu przy pierwszym uruchomieniu)
            if (File.Exists(sciezkaPliku))
            {
                // Czyta wszystkie linijki tekstu z pliku i wrzuca je prosto do naszej wizualnej listy
                string[] zapisaneFilmy = File.ReadAllLines(sciezkaPliku);
                ListaFilmowBox.ItemsSource = zapisaneFilmy;
            }
        }

        private void TytulTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SzukajButton_Click(sender, e);
            }
        }

        private async void SzukajButton_Click(object sender, RoutedEventArgs e)
        {
            string wpisanyTytul = TytulTextBox.Text.Trim();
            if (string.IsNullOrEmpty(wpisanyTytul)) return;

            TytulTextBlock.Text = "Szukam...";
            ZapiszButton.Visibility = Visibility.Collapsed;
            KomunikatTextBlock.Text = "";

            string url = $"http://www.omdbapi.com/?t={wpisanyTytul}&apikey={kluczApi}";

            try
            {
                string odpowiedzZSerwera = await klientHttp.GetStringAsync(url);
                using JsonDocument dokument = JsonDocument.Parse(odpowiedzZSerwera);
                JsonElement dane = dokument.RootElement;

                if (dane.TryGetProperty("Response", out JsonElement odpowiedz) && odpowiedz.GetString() == "True")
                {
                    TytulTextBlock.Text = dane.GetProperty("Title").GetString();
                    RokTextBlock.Text = "Rok: " + dane.GetProperty("Year").GetString();
                    OcenaTextBlock.Text = "Ocena IMDb: " + dane.GetProperty("imdbRating").GetString();
                    OpisTextBlock.Text = dane.GetProperty("Plot").GetString();

                    string linkDoPlakatu = dane.GetProperty("Poster").GetString();
                    if (linkDoPlakatu != "N/A" && !string.IsNullOrEmpty(linkDoPlakatu))
                    {
                        PlakatImage.Source = new BitmapImage(new Uri(linkDoPlakatu));
                    }
                    else
                    {
                        PlakatImage.Source = null;
                    }

                    ZapiszButton.Visibility = Visibility.Visible;
                }
                else
                {
                    TytulTextBlock.Text = "Nie znaleziono filmu!";
                    RokTextBlock.Text = "";
                    OcenaTextBlock.Text = "";
                    OpisTextBlock.Text = "Sprawdź, czy wpisałeś poprawny angielski tytuł.";
                    PlakatImage.Source = null;
                }
            }
            catch (Exception ex)
            {
                TytulTextBlock.Text = "Błąd połączenia!";
                OpisTextBlock.Text = ex.Message;
            }
        }

        private void ZapiszButton_Click(object sender, RoutedEventArgs e)
        {
            string tytul = TytulTextBlock.Text;
            string rok = RokTextBlock.Text;
            string ocena = OcenaTextBlock.Text;

            string wpisDoPliku = $"{tytul} | {rok} | {ocena}\n";
            File.AppendAllText(sciezkaPliku, wpisDoPliku);

            KomunikatTextBlock.Text = "Zapisano do kolekcji!";
            ZapiszButton.Visibility = Visibility.Collapsed;

            // NOWOŚĆ: Odświeżamy listę, żeby nowy film pojawił się na niej od razu po kliknięciu!
            OdswiezListeFilmow();
        }
    }
}