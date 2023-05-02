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
    /// Логика взаимодействия для AddEditReadersWindow.xaml
    /// </summary>
    public partial class AddEditReadersWindow : Window
    {
        public AddEditReadersWindow()
        {
            InitializeComponent();
        }

        Entities _entities;
        Readers _readers;
        MainWindow _mainWindow;    

        public AddEditReadersWindow(Readers readers, Entities entities, MainWindow main)
        {
            InitializeComponent();
            if (readers == null)
            {
                Title = "Добавление";
                _readers = new Readers();
                ClassAddEdit.ID = 0;
            }
            else
            {
                Title = "Изменение";
                _readers = readers;
                ClassAddEdit.ID = 1;
            }
            _entities = entities;
            _mainWindow = main;
            DataContext = _readers;
        }

        protected void SaveChanges()
        {
            Validate();
            if (ClassAddEdit.ID == 0)
            { _entities.Readers.Add(_readers); }
            _entities.SaveChanges();
            _mainWindow.Read();
        }

        void Validate()
        {
            // Чтение данных
            _readers.id_reader_card = Convert.ToInt32(TextBoxId.Text);
            _readers.First_Name = TextBoxFName.Text;
            _readers.Middle_Name = TextBoxMName.Text;
            _readers.Last_Name = TextBoxLName.Text;
            _readers.Birthday = Convert.ToDateTime(DatePickerBDay.Text);
            _readers.Gender = ComboBoxGender.SelectedIndex + 1;
            _readers.Phone = TextBoxPhone.Text;
            _readers.Address = TextBoxAddress.Text;     
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Инициализация данных
            if (ClassAddEdit.ID == 0)
            { 
                TextBoxId.Text = (_entities.Readers.Select(x => x.id_reader_card).Max() + 1).ToString();
            }           
            ComboBoxGender.ItemsSource = _entities.Genders.ToList();
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
