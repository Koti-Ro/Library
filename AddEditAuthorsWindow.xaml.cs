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
    /// Логика взаимодействия для AddEditAuthorsWindow.xaml
    /// </summary>
    public partial class AddEditAuthorsWindow : Window
    {
        public AddEditAuthorsWindow()
        {
            InitializeComponent();
        }

        Entities _entities;
        Authors _authors;
        MainWindow _mainWindow;

        public AddEditAuthorsWindow(Authors authors, Entities entities, MainWindow main)
        {
            InitializeComponent();
            if (authors == null)
            {
                Title = "Добавление";
                _authors = new Authors();
                ClassAddEdit.ID = 0;
            }
            else
            {
                Title = "Изменение";
                _authors = authors;
                ClassAddEdit.ID = 1;
            }
            _entities = entities;
            _mainWindow = main;
            DataContext = _authors;
        }

        protected void SaveChanges()
        {
            Validate();
            if (ClassAddEdit.ID == 0)
            { _entities.Authors.Add(_authors); }
            _entities.SaveChanges();
            _mainWindow.Read();
        }

        void Validate()
        {
            // Чтение данных
            _authors.id_authors = Convert.ToInt32(TextBoxId.Text);
            _authors.Name = TextBoxName.Text;            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Инициализация данных
            if (ClassAddEdit.ID == 0)
            {
                TextBoxId.Text = (_entities.Authors.Select(x => x.id_authors).Max() + 1).ToString();
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
