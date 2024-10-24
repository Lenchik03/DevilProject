using Devil_sOffice;
using System;
using System.Collections.Generic;
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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DevilsOfficeWPF
{
    /// <summary>
    /// Логика взаимодействия для NewAndUpdateDevil.xaml
    /// </summary>
    public partial class NewAndUpdateDevil : Window, INotifyPropertyChanged
    {
        HttpClient httpClient = new HttpClient();

        public Devil Devil { get; set; } = new Devil();

        private List<Devil> devils;

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public List<Devil> Devils
        {
            get => devils;
            set
            {
                devils = value;
                Signal();
            }
        }

        public NewAndUpdateDevil()
        {
            InitializeComponent();
            httpClient.BaseAddress = new Uri("http://localhost:5073/api/");
            DataContext = this;
            UpdateList();
        }
        public NewAndUpdateDevil(Devil devil)
        {
            InitializeComponent();
            httpClient.BaseAddress = new Uri("http://localhost:5073/api/");
            this.Devil = devil;
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
        }

        private async void SaveClick(object sender, RoutedEventArgs e)
        {
            if (Devil == null)
               return;

            if (Devil.Id == 0)
            {
                
                string arg = JsonSerializer.Serialize(Devil);
                var responce = await httpClient.PostAsync($"Devils/AddDevil",
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
                string arg = JsonSerializer.Serialize(Devil);
                var responce = await httpClient.PostAsync($"Devils/UpdateDevil",
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
