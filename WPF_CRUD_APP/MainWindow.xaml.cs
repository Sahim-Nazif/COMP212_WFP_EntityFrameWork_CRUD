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

       
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //in order to update the book, first need to retrieve the ID of the selected Book
            int Id = (MyBooksGrid.SelectedItem as Book).ID;
            var updateBook = db.Books.Where(b => b.ID == Id).Single();

            updateBook.Title = txtTitle.Text;
            updateBook.Author = txtAuthor.Text;
            updateBook.ISBN = txtISBN.Text;
            updateBook.Publication_Date = (DateTime)txtDate.SelectedDate;
            db.SaveChanges();
            datagrid();

            clearFields();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int Id = (MyBooksGrid.SelectedItem as Book).ID;
            var deleteBook = db.Books.Where(b => b.ID == Id).Single();

            db.Books.Remove(deleteBook);
            db.SaveChanges();
            datagrid();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            clearFields();
        }
    }
}
