using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Library
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Entities entities;
        public MainWindow()
        {
            InitializeComponent();            
            Read();
        }

        // Чтение данных
        public void Read()
        {
            // Инициализация сущности базы данных
            entities = new Entities();
            DataContext = entities.Database;
            // Инициализация таблиц
            DataGridIssuance.ItemsSource = entities.Issuance.ToArray();
            DataGridBooks.ItemsSource = entities.Books.ToArray();
            DataGridReaders.ItemsSource = entities.Readers.ToArray();
            DataGridAuthors.ItemsSource = entities.Authors.ToArray();
            DataGridPublishers.ItemsSource = entities.Publishers.ToArray();
            DataGridDepartments.ItemsSource = entities.Departments.ToArray();
            // Инициализация фильтров
            ComboBoxFilterReader.ItemsSource=entities.Genders.Select(x=>x.Gender_Name).ToArray();
            ComboBoxFilterPublisher.ItemsSource=entities.Publishers.Select(x=>x.Publisher_Name.Trim()).ToArray();
            ComboBoxFilterAuthor.ItemsSource=entities.Authors.Select(x=>x.Name.Trim()).ToArray();   
            ComboBoxFilterDepartment.ItemsSource=entities.Departments.Select(x=>x.id_department.Trim()).ToArray();
            ComboBoxFilterYear.ItemsSource=entities.Books.Select(x=>x.Release_year.ToString()).ToArray();
            ComboBoxFilterYear.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
        }

        // Удаление
        private void DeleteRowIssue_Click(object sender, RoutedEventArgs e)
        {
            var selected = ((sender as Button).DataContext as Issuance);
            if (MessageBox.Show($"Вы точно хотите удалить эту запись?",
                "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                entities.Issuance.Remove(selected);
                entities.SaveChanges();
                Read();
            }
        }
        private void DeleteRowBook_Click(object sender, RoutedEventArgs e)
        {
            var selected = ((sender as Button).DataContext as Books);
            if (MessageBox.Show($"Вы точно хотите удалить эту запись?",
                "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                entities.Books.Remove(selected);
                entities.SaveChanges();
                Read();
            }
        }
        private void DeleteRowReader_Click(object sender, RoutedEventArgs e)
        {
            var selected = ((sender as Button).DataContext as Readers);
            if (MessageBox.Show($"Вы точно хотите удалить эту запись?",
                "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                entities.Readers.Remove(selected);
                entities.SaveChanges();
                Read();
            }
        }
        private void DeleteRowPublisher_Click(object sender, RoutedEventArgs e)
        {
            var selected = ((sender as Button).DataContext as Publishers);
            if (MessageBox.Show($"Вы точно хотите удалить эту запись?",
                "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                entities.Publishers.Remove(selected);
                entities.SaveChanges();
                Read();
            }
        }

        private void DeleteRowAuthor_Click(object sender, RoutedEventArgs e)
        {
            var selected = ((sender as Button).DataContext as Authors);
            if (MessageBox.Show($"Вы точно хотите удалить эту запись?",
                "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                entities.Authors.Remove(selected);
                entities.SaveChanges();
                Read();
            }
        }

        // Изменение
        private void EditRowIssue_Click(object sender, RoutedEventArgs e)
        {
            var selected = ((sender as Button).DataContext as Issuance);
            var windows = new AddEditIssuanceWindow(selected, entities, this);
            var result = windows.ShowDialog();
            Read();
        }
        private void EditRowBook_Click(object sender, RoutedEventArgs e)
        {
            var selected = ((sender as Button).DataContext as Books);
            var windows = new AddEditBooksWindow(selected, entities, this);
            var result = windows.ShowDialog();
            Read();
        }
        private void EditRowReader_Click(object sender, RoutedEventArgs e)
        {
            var selected = ((sender as Button).DataContext as Readers);
            var windows = new AddEditReadersWindow(selected, entities, this);
            var result = windows.ShowDialog();
            Read();
        }
        private void EditRowPublisher_Click(object sender, RoutedEventArgs e)
        {
            var selected = ((sender as Button).DataContext as Publishers);
            var windows = new AddEditPublishersWindow(selected, entities, this);
            var result = windows.ShowDialog();
            Read();
        }

        private void EditRowAuthor_Click(object sender, RoutedEventArgs e)
        {
            var selected = ((sender as Button).DataContext as Authors);
            var windows = new AddEditAuthorsWindow(selected, entities, this);
            var result = windows.ShowDialog();
            Read();
        }

        // Добавление
        private void AddRowIssueButton_Click(object sender, RoutedEventArgs e)
        {
            var windows = new AddEditIssuanceWindow(null, entities, this);
            var result = windows.ShowDialog();
            Read();
        }
        private void AddRowBookButton_Click(object sender, RoutedEventArgs e)
        {
            var windows = new AddEditBooksWindow(null, entities, this);
            var result = windows.ShowDialog();
            Read();
        }
        private void AddRowReaderButton_Click(object sender, RoutedEventArgs e)
        {
            var windows = new AddEditReadersWindow(null, entities, this);
            var result = windows.ShowDialog();
            Read();
        }
        private void AddRowAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            var windows = new AddEditAuthorsWindow(null, entities, this);
            var result = windows.ShowDialog();
            Read();
        }

        private void AddRowPublisherButton_Click(object sender, RoutedEventArgs e)
        {
            var windows = new AddEditPublishersWindow(null, entities, this);
            var result = windows.ShowDialog();
            Read();
        }

        // Поиск
        private void TextBoxSearchIssue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxSearchIssue.Text != null)
            {
                var input = TextBoxSearchIssue.Text.ToLower().Trim();
                var filteredData = entities.Issuance.Where(x => x.Readers.First_Name.Contains(input) ||
                x.Books.Title.Contains(input)).ToArray();
                DataGridIssuance.ItemsSource = filteredData;
            }
        }
        private void TextBoxSearchBook_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxSearchBook.Text != null)
            {
                var input = TextBoxSearchBook.Text.ToLower().Trim();
                var filteredData = entities.Books.Where(x => x.Title.Contains(input) ||
                x.id_books.ToString().Contains(input)).ToArray();
                DataGridBooks.ItemsSource = filteredData;
            }
        }
        private void TextBoxSearchReader_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxSearchReader.Text != null)
            {
                var input = TextBoxSearchReader.Text.ToLower().Trim();
                var filteredData = entities.Readers.Where(x => x.First_Name.Contains(input) ||
                x.Middle_Name.Contains(input) || x.Last_Name.Contains(input)).ToArray();
                DataGridReaders.ItemsSource = filteredData;
            }
        }
        private void TextBoxSearchAutor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxSearchAuthor.Text != null)
            {
                var input = TextBoxSearchAuthor.Text.ToLower().Trim();
                var filteredData = entities.Authors.Where(x => x.Name.Contains(input)).ToArray();
                DataGridAuthors.ItemsSource = filteredData;
            }
        }

        private void TextBoxSearchPublisher_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxSearchPublisher.Text != null)
            {
                var input = TextBoxSearchPublisher.Text.ToLower().Trim();
                var filteredData = entities.Publishers.Where(x => x.Publisher_Name.Contains(input)).ToArray();
                DataGridPublishers.ItemsSource = filteredData;
            }
        }

        private void TextBoxSearchDepartment_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxSearchDepartment.Text != null)
            {
                var input = TextBoxSearchDepartment.Text.ToLower().Trim();
                var filteredData = entities.Departments.Where(x => x.Department_Name.Contains(input) ||
                x.id_department.Contains(input)).ToArray();
                DataGridDepartments.ItemsSource = filteredData;
            }
        }

        // Сброс фильтров и поиска
        private void ClearSearch_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxFilterYear.Text = "Год";
            ComboBoxFilterReader.Text = "Пол";
            ComboBoxFilterDepartment.Text = "Отдел";
            ComboBoxFilterAuthor.Text = "Автор/Соавтор";
            ComboBoxFilterPublisher.Text = "Издательство";
            TextBoxSearchBook.Clear();
            TextBoxSearchIssue.Clear();
            TextBoxSearchReader.Clear();
            TextBoxSearchDepartment.Clear();
            Read();
        }

        // Фильтры
        private void ComboBoxFilterReader_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxFilterReader.SelectedIndex >= 0)
            {
                var input = ComboBoxFilterReader.SelectedItem.ToString();
                var filteredData = entities.Readers.Where(x => x.Genders.Gender_Name.ToString().Contains(input)).ToArray();
                DataGridReaders.ItemsSource = filteredData;
            }
            else Read();
        }
        private void ComboBoxFilterPublisher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            ComboBoxFilterAuthor.Text = "Автор/Соавтор";
            
            if (ComboBoxFilterPublisher.SelectedIndex >= 0)
            {
                var input = ComboBoxFilterPublisher.SelectedItem.ToString();
                var filteredData = entities.Books.Where(x => x.Publishers.Publisher_Name.ToString().Contains(input)).ToArray();
                DataGridBooks.ItemsSource = filteredData;
            }
            else Read();
        }
        private void ComboBoxFilterAuthor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            ComboBoxFilterPublisher.Text = "Издательство";

            if (ComboBoxFilterAuthor.SelectedIndex >= 0)
            {
                var input = ComboBoxFilterAuthor.SelectedItem.ToString();
                var filteredData = entities.Books.Where(x => x.Authors.Name.ToString().Contains(input) ||
                x.Authors1.Name.ToString().Contains(input)).ToArray();
                DataGridBooks.ItemsSource = filteredData;
            }
            else Read();
        }
        private void ComboBoxFilterYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxFilterAuthor.Text = "Автор/Соавтор";
            ComboBoxFilterPublisher.Text = "Издательство";
            ComboBoxFilterDepartment.Text = "Отдел";

            if (ComboBoxFilterYear.SelectedIndex >= 0)
            {
                var input = ComboBoxFilterYear.SelectedItem.ToString();
                var filteredData = entities.Books.Where(x =>
                    x.Release_year.ToString().Trim().Contains(input)).ToArray();
                DataGridBooks.ItemsSource = filteredData;
            }
            else Read();
        }

        private void ComboBoxFilterDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxFilterAuthor.Text = "Автор/Соавтор";
            ComboBoxFilterPublisher.Text = "Издательство";
            ComboBoxFilterYear.Text = "Год";

            if (ComboBoxFilterDepartment.SelectedIndex >= 0)
            {
                var input = ComboBoxFilterDepartment.SelectedItem.ToString();
                var filteredData = entities.Books.Where(x =>
                    x.Departments.id_department.Trim().Contains(input)).ToArray();
                DataGridBooks.ItemsSource = filteredData;
            }
            else Read();
        }

        // Невидимый поиск отделов
        private void CheckOnOff_Checked(object sender, RoutedEventArgs e)
        {            
            TextBoxSearchDepartment.Visibility = Visibility.Visible; 
            ClearSearch6.Visibility = Visibility.Visible;
        }

        private void CheckOnOff_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBoxSearchDepartment.Visibility = Visibility.Collapsed;
            ClearSearch6.Visibility = Visibility.Collapsed;
        }       
    }
}
