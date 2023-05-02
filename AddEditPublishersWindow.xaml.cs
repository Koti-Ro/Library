using System;
using System.Collections.Generic;
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

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для AddEditPublishersWindow.xaml
    /// </summary>
    public partial class AddEditPublishersWindow : Window
    {
        public AddEditPublishersWindow()
        {
            InitializeComponent();
        }

        Entities _entities;
        Publishers _publishers;
        MainWindow _mainWindow;

        public AddEditPublishersWindow(Publishers publishers, Entities entities, MainWindow main)
        {
            InitializeComponent();
            if (publishers == null)
            {
                Title = "Добавление";
                _publishers = new Publishers();
                ClassAddEdit.ID = 0;
            }
            else
            {
                Title = "Изменение";
                _publishers = publishers;
                ClassAddEdit.ID = 1;
            }
            _entities = entities;
            _mainWindow = main;
            DataContext = _publishers;
        }

        protected void SaveChanges()
        {
            Validate();
            if (ClassAddEdit.ID == 0)
            { _entities.Publishers.Add(_publishers); }
            _entities.SaveChanges();
            _mainWindow.Read();
        }

        void Validate()
        {
            // Чтение данных
            _publishers.id_publishers = Convert.ToInt32(TextBoxId.Text);
            _publishers.Publisher_Name = TextBoxName.Text;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Инициализация данных
            if (ClassAddEdit.ID == 0)
            {
                TextBoxId.Text = (_entities.Publishers.Select(x => x.id_publishers).Max() + 1).ToString();
            }            
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges();
            Close();
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
