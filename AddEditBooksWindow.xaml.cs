using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для AddEditBooksWindow.xaml
    /// </summary>
    public partial class AddEditBooksWindow : Window
    {
        Entities _entities;
        Books _books;
        MainWindow _mainWindow; 

        public AddEditBooksWindow(Books books, Entities entities, MainWindow main)
        {
            InitializeComponent();
            if (books == null)
            {
                Title = "Добавление";
                _books = new Books();
                ClassAddEdit.ID = 0;
            }
            else
            {
                Title = "Изменение";
                _books = books;
                ClassAddEdit.ID = 1;
            }
            _entities = entities;
            _mainWindow = main;
            DataContext = _books;
        }
        
        protected void SaveChanges()
        {
            Validate();
            if (ClassAddEdit.ID == 0)
            { _entities.Books.Add(_books); }            
            _entities.SaveChanges();
            _mainWindow.Read();
        }

        void Validate()
        {
            // Чтение данных
            _books.id_books = Convert.ToInt32(TextBoxId.Text);
            _books.Title = TextBoxTitle.Text;
            _books.Author = ComboBoxAuthor.SelectedIndex + 1;
            if (ComboBoxCoAuthor.SelectedIndex != -1)
            { _books.Co_Author = ComboBoxCoAuthor.SelectedIndex + 1; }
            _books.Publisher = ComboBoxPublisher.SelectedIndex + 1;
            _books.Release_year =Convert.ToInt32(TextBoxYear.Text);
            _books.Page_Count = Convert.ToInt32(TextBoxPage.Text); 
            _books.Department = ComboBoxDepartment.Text;
            _books.Price = Convert.ToDecimal(TextBoxPrice.Text);            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Инициализация данных
            if (ClassAddEdit.ID == 0) TextBoxId.Text = "";
            ComboBoxAuthor.ItemsSource = _entities.Authors.ToList();
            ComboBoxCoAuthor.ItemsSource = _entities.Authors.ToList();
            ComboBoxPublisher.ItemsSource = _entities.Publishers.ToList();
            ComboBoxDepartment.ItemsSource = _entities.Departments.ToList();            
            if (ClassAddEdit.ID == 1)
            {
                TextBoxId.IsReadOnly = true;
                TextBoxPrice.Text = TextBoxPrice.Text.Trim().Substring
                    (0, TextBoxPrice.Text.Length - 2).Replace('.', ',');
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
