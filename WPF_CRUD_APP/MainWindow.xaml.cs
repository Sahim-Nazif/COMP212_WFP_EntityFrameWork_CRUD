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

        // added mouse_double click event for the datagrid when item is selected should populate in the text-boxes
        // to add this event simply select datagrid and then select mousedouble click event in the properties winds to the right
        private void MyBooksGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            try
            {
                DataGridCellInfo cell1 = MyBooksGrid.SelectedCells[1]; txtTitle.Text = ((TextBlock)cell1.Column.GetCellContent(cell1.Item)).Text.ToString();
                DataGridCellInfo cell2 = MyBooksGrid.SelectedCells[2]; txtAuthor.Text = ((TextBlock)cell2.Column.GetCellContent(cell2.Item)).Text.ToString();
                DataGridCellInfo cell4 = MyBooksGrid.SelectedCells[4]; txtISBN.Text = ((TextBlock)cell4.Column.GetCellContent(cell4.Item)).Text.ToString();
            }
            catch
            {
               
            }

        }
    }
}
