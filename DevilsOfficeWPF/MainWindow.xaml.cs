using Devil_sOffice;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DevilsOfficeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public Rack Rack { get; set; } = new();

        public Devil Devil { get; set; } = new();

        private List<Devil> devils;

        public List<Devil> Devils
        {
            get => devils;
            set
            {
                devils = value;
                Signal();
            }
        }

        private List<Rack> racks;

        public List<Rack> Racks
        {
            get => racks;
            set
            {
                racks = value;
                Signal();
            }
        }

        HttpClient httpClient = new HttpClient();

        public DispatcherTimer timer;

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public MainWindow()
        {
            InitializeComponent();
            httpClient.BaseAddress = new Uri("http://localhost:5073/api/");
            DataContext = this;
            UpdateList();

        }

        public async void UpdateList()
        {
            string arg = JsonSerializer.Serialize(Devils);
            var responce = await httpClient.PostAsync($"Devils/GetDevils",
                new StringContent(arg, Encoding.UTF8, "application/json"));

            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                return;
            }
            else
            {
                Devils = await responce.Content.ReadFromJsonAsync<List<Devil>>();
                //MessageBox.Show("Всё ОК");
            }

            string arg1 = JsonSerializer.Serialize(Devils);
            var responce1 = await httpClient.PostAsync($"Racks/GetRacks",
                new StringContent(arg1, Encoding.UTF8, "application/json"));

            if (responce1.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce1.Content.ReadAsStringAsync();
                return;
            }
            else
            {
                Racks = await responce1.Content.ReadFromJsonAsync<List<Rack>>();
                //MessageBox.Show("Всё ОК");
            }

        }

        private void NewRack(object sender, RoutedEventArgs e)
        {
            NewAndUpdateRack newAndUpdateRack = new NewAndUpdateRack();
            newAndUpdateRack.ShowDialog();
        }

        private void UpdateRack(object sender, RoutedEventArgs e)
        {
            NewAndUpdateRack newAndUpdateRack = new NewAndUpdateRack();
            newAndUpdateRack.ShowDialog();
        }

        private async void DeleteRack(object sender, RoutedEventArgs e)
        {
            string arg = JsonSerializer.Serialize(Rack);
            var responce = await httpClient.PostAsync($"Disposals/DisposalRack",
                new StringContent(arg, Encoding.UTF8, "application/json"));

            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                return;
            }
            else
            {
                //var answer = await responce.Content.ReadFromJsonAsync<Passport>();
                MessageBox.Show("Сообщение отправлено");
            }

            string arg1 = JsonSerializer.Serialize(Rack);
            var responce1 = await httpClient.PostAsync($"Racks/DeleteRack",
                new StringContent(arg1, Encoding.UTF8, "application/json"));

            if (responce1.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce1.Content.ReadAsStringAsync();
                return;
            }
            else
            {
                //var answer = await responce.Content.ReadFromJsonAsync<Passport>();
                MessageBox.Show("Сообщение отправлено");
            }
        }

        private void NewDevil(object sender, RoutedEventArgs e)
        {
            NewAndUpdateDevil newAndUpdateDevil = new NewAndUpdateDevil();
            newAndUpdateDevil.ShowDialog();
        }

        private void UpdateDevil(object sender, RoutedEventArgs e)
        {
            NewAndUpdateDevil newAndUpdateDevil = new NewAndUpdateDevil(Devil);
            newAndUpdateDevil.ShowDialog();
        }

        private async void DeleteDevil(object sender, RoutedEventArgs e)
        {
            
            string arg = JsonSerializer.Serialize(Devil);
            var responce = await httpClient.PostAsync($"Disposals/DisposalDevil",
                new StringContent(arg, Encoding.UTF8, "application/json"));

            if (responce.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var result = await responce.Content.ReadAsStringAsync();
                return;
            }
            else
            {
                //var answer = await responce.Content.ReadFromJsonAsync<Passport>();
                MessageBox.Show("Сообщение отправлено");
            }
        }
    }
}