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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_CRUD_APP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //creating a DB object
        public BookDBEntities db = new BookDBEntities();
        public static DataGrid data;
        public MainWindow()
        {
            InitializeComponent();
            datagrid();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //here the new Book refers to the class
            var book = new Book
            {
                Title=txtTitle.Text,
                Author=txtAuthor.Text,
                Publication_Date = (DateTime)txtDate.SelectedDate,
                ISBN =txtISBN.Text
            };
            //here the Books refers to the DbSet
            db.Books.Add(book);
            db.SaveChanges();
            datagrid();
            clearFields();
        }

        public void datagrid()
        {
            MyBooksGrid.ItemsSource = db.Books.ToList();
            data = MyBooksGrid;
        }

        public void clearFields()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtISBN.Clear();
            txtDate.Text = "";
        }
    }
}
