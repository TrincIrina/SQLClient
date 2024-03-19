using SQLClientExample.DB;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SQLClientExample
{
    /// <summary>
    /// Логика взаимодействия для ConnectionWindow.xaml
    /// </summary>
    public partial class ConnectionWindow : Window
    {
        public ConnectionWindow()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string serverName = serverNameTextBox.Text;
                string dbName = databaseTextBox.Text;
                using (SqlConnection connection = DbConnectionProvider.OpenConnection(serverName, dbName))
                {
                    MessageBox.Show(
                        "Connection is successfully established",
                        "Connection is OK",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                    // скрыть ConnectionWindow
                    Hide();
                    // открыть форму для запросов, передав открытое соединение
                    MainWindow mainWindow = new MainWindow(connection);
                    mainWindow.ShowDialog();
                    // по закрытии формы запросов снова отобразить ConnectionWindow
                    Show();
                }
            } catch (Exception ex)
            {
                MessageBox.Show(
                    $"database connection error: {ex.Message}", 
                    "Connection error", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error
                );
            }
        }
    }
}
