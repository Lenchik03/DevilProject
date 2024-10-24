using Devil_sOffice;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DevilsOfficeWPF
{
    /// <summary>
    /// Логика взаимодействия для NewAndUpdateRack.xaml
    /// </summary>
    public partial class NewAndUpdateRack : Window, INotifyPropertyChanged
    {
        HttpClient httpClient = new HttpClient();

        public Rack Rack { get; set; } = new Rack();

        public Devil SelectedDevil
        {
            get => selectedDevil;
            set
            { 
                selectedDevil = value;
                Signal();
            }
        }

        private ObservableCollection<Devil> devils;
        private Devil selectedDevil;

        public ObservableCollection<Devil> Devils
        {
            get => devils;
            set
            {
                devils = value;
                Signal();
            }
        }
        

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public NewAndUpdateRack()
        {
            InitializeComponent();
            httpClient.BaseAddress = new Uri("http://localhost:5073/api/");
            DataContext = this;
            UpdateList();
        }

        

        public NewAndUpdateRack(Rack rack)
        {
            InitializeComponent();
            Rack = rack;
            httpClient.BaseAddress = new Uri("http://localhost:5073/api/");
            DataContext = this;
            UpdateList();
            

        }

        

        private async void UpdateList()
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
                Devils = await responce.Content.ReadFromJsonAsync<ObservableCollection<Devil>>();
                //MessageBox.Show("Всё ОК");
                if (Rack == null)
                {
                    return;
                }
                SelectedDevil = Devils.FirstOrDefault(s => s.Id == Rack.IdDevil);
            }
        }

        private async void SaveClick(object sender, RoutedEventArgs e)
        {
            if (Rack == null)
                return;
            if (SelectedDevil == null)
            { 
                Rack.IdDevil = 38;
                Rack.IdDevilNavigation = Devils.FirstOrDefault(s => s.Id == 38);
            }

                if (SelectedDevil != null)
                {
                    Rack.IdDevil = SelectedDevil.Id;
                    Rack.IdDevilNavigation = SelectedDevil;
                }
           
                if (Rack.Id == 0)
                {
                    string arg = JsonSerializer.Serialize(Rack);
                    var responce = await httpClient.PostAsync($"Racks/AddRack",
                        new StringContent(arg, Encoding.UTF8, "application/json"));

                    if (responce.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        var result = await responce.Content.ReadAsStringAsync();
                        return;
                    }
                    else
                    {
                        //Devils = await responce.Content.ReadFromJsonAsync<List<Devil>>();
                        MessageBox.Show("Всё ОК");
                    }
                }
                else
                {
                    string arg = JsonSerializer.Serialize(Rack);
                    var responce = await httpClient.PostAsync($"Racks/UpdateRack",
                        new StringContent(arg, Encoding.UTF8, "application/json"));

                    if (responce.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        var result = await responce.Content.ReadAsStringAsync();
                        return;
                    }
                    else
                    {
                        //Devils = await responce.Content.ReadFromJsonAsync<List<Devil>>();
                        MessageBox.Show("Всё ОК");
                    }
                }
            }
        
        
    }
}
