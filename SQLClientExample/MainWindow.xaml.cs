using SQLClientExample.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

namespace SQLClientExample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SqlConnection _connection;
        public MainWindow(SqlConnection connection)
        {
            InitializeComponent();

            _connection = connection;
        }

        private void executeButton_Click(object sender, RoutedEventArgs e)
        {            
            DbQueryManager manager = new DbQueryManager(queryTextBox.Text, _connection);
            try
            {
                using (SqlCommand command = new SqlCommand(queryTextBox.Text, _connection))
                {
                    switch (ParseQueri(queryTextBox.Text))
                    {
                        case "reader":
                            //тут и только тут мы будем обрабатывать селект запросы и возвращать таблицу
                            //для этого нам нужен Дата тейбл
                            DataTable dt = manager.Query();
                            //Делаем видимым Дата грид который создан специально для вывода таблиц
                            dataGrid.Visibility = Visibility.Visible;
                            //тут вставляем в дата грид то, что получилось вытащить из таблиц
                            dataGrid.ItemsSource = dt.DefaultView;
                            break;
                        case "nonquery":
                            //тут мы делаем видимым текст бокс что бы в нем отобразить результат того сколько
                            // строк у нас изменилось, т.к. он лучше подходит для этого, соответственно
                            // дата грид мы в этот момент просто скрываем
                            resultTextBox.Visibility = Visibility.Visible;
                            dataGrid.Visibility = Visibility.Hidden;
                            //тут просто впихиваем кол-во изменений
                            resultTextBox.Text = manager.Query(queryTextBox.Text);
                            break;
                        default:
                            //нераспознаный запрос записывается в текст бокс вот и всё грид скрываем
                            resultTextBox.Visibility = Visibility.Visible;
                            dataGrid.Visibility = Visibility.Hidden;
                            resultTextBox.Text = "Your request is incorrect, " +
                                "please check the spelling of your request";
                            break;
                    }

                }
            }
            catch
            {
                MessageBox.Show(
                    "An error has crept into the body of your request, please try again",
                    "Error", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// метод который парсит
        /// </summary>
        /// <param name="text"></param>
        /// <returns> 'select' для метода который обрабатывает selectзапросы
        /// 'nonquery' для тех что обрабатывают delete isnert и update
        /// и unknow если ввели какую то фигню</returns>
        private string ParseQueri(string text)
        {
            string parse = queryTextBox.Text.Split(' ')[0];

            if (parse == "SELECT")
            {
                return "reader";
            }
            else if (parse == "INSERT" || parse == "DELETE" || parse == "UPDATE")
            {
                return "nonquery";
            }
            return "unknow";
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
