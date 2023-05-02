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
    /// Логика взаимодействия для AddEditIssuanceWindow.xaml
    /// </summary>
    public partial class AddEditIssuanceWindow : Window
    {
        Entities _entities;
        Issuance _issuance;
        MainWindow _mainWindow;
        DateTime _now = DateTime.Now;       

        public AddEditIssuanceWindow(Issuance issuance, Entities entities, MainWindow main)
        {
            InitializeComponent();            
            if (issuance == null)
            {
                Title = "Добавление";
                _issuance = new Issuance();
                ClassAddEdit.ID = 0;
            }
            else
            {
                Title = "Изменение";
                _issuance = issuance;
                ClassAddEdit.ID = 1;
            }
            _entities = entities;
            _mainWindow = main;
            DataContext = _issuance;
        }

        protected void SaveChanges()
        {
            Validate();
            if (ClassAddEdit.ID == 0)
            { _entities.Issuance.Add(_issuance); }
            _entities.SaveChanges();
            _mainWindow.Read();
        }

         void Validate()
         {
            // Чтение данных
             _issuance.id_issuance = Convert.ToInt32(TextBoxId.Text);            
             _issuance.Issue_Date = Convert.ToDateTime(DatePickerIsDate.Text);
             _issuance.Est_Return_Date = Convert.ToDateTime(DatePickerEstReDate.Text);
             if (_issuance.Extension_Period != null)
             { _issuance.Extension_Period = Convert.ToInt32(TextBoxPeriod.Text); }
             //
             if (ClassAddEdit.ID == 0 || _issuance.Return_Date != null)             
                _issuance.Return_Date = Convert.ToDateTime(DatePickerReDate.Text);
             if (ClassAddEdit.ID == 1 || _issuance.Return_Date != null)
                _issuance.Return_Date = Convert.ToDateTime(DatePickerReDate.Text);
                _entities.Issuance.AddOrUpdate(_issuance);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Инициализация данных
            if (ClassAddEdit.ID == 0)
            { 
                TextBoxId.Text = (_entities.Issuance.Select(x => x.id_issuance).Max() + 1).ToString();
                DatePickerIsDate.Text = _now.ToString();                
            }            
            ComboBoxReader.ItemsSource = _entities.Readers.ToList();
            ComboBoxBook.ItemsSource = _entities.Books.ToList();
            DatePickerEstReDate.Text = Convert.ToDateTime(DatePickerIsDate.Text).AddDays(14).ToString();
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
