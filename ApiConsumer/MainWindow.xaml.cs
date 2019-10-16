using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ApiConsumer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static HttpClient client;
        public MainWindow()
        {
            InitializeComponent();
            client = new HttpClient();
            client.BaseAddress = new Uri("https://oeapika.azurewebsites.net");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue
                ("application/json"));

            GetAll();
        }

        async Task AddToDo(Todo t)
        {
            HttpResponseMessage response =
                await client.PostAsJsonAsync("/api/values", t);

            response.EnsureSuccessStatusCode();

            GetAll();
        }

        async Task<List<Todo>> GetAll()
        {
            List<Todo> todos = new List<Todo>();
            HttpResponseMessage response = await
                client.GetAsync("/api/values");
            if (response.IsSuccessStatusCode)
            {
                //
                todos = await response.Content.ReadAsAsync<List<Todo>>();
            }
            dgrid.ItemsSource = todos;
            return todos;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Todo t = new Todo()
            {
                Name = tb_name.Text
            };
            AddToDo(t);
        }
    }
}
