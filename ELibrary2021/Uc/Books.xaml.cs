using ELibrary2021.Classes;
using ELibrary2021.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ELibrary2021.Uc
{
    /// <summary>
    /// Interaction logic for Books.xaml
    /// </summary>
    public partial class Books : UserControl
    {
        MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        LogClass logNew = new LogClass(); 
        public Books()
        {
            InitializeComponent();
            LoadBooksList();
        }

        public async void LoadBooksList()
        {
            try
            {
                using (DbContextClass db = new DbContextClass())
                {
                    dgShow.ItemsSource = await db.Books.ToListAsync();
                    btnReset.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            vbAddBookUc.Visibility = Visibility.Visible;
            vbClassBooks.Visibility = Visibility.Collapsed;
        }
        private void btnClassify_Click(object sender, RoutedEventArgs e)
        {
            vbAddBookUc.Visibility = Visibility.Collapsed;
            vbClassBooks.Visibility = Visibility.Visible;
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            LoadBooksList();
            btnReset.Visibility = Visibility.Collapsed;
        }
        private void btnSearchBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSearchBook.Text != string.Empty)
                {
                    using (DbContextClass db = new DbContextClass())
                    {
                        List<BooksModel> SearchedBooks = new List<BooksModel>();
                        foreach (var book in db.Books)
                        {
                            if (book.BookAuthor.Contains(txtSearchBook.Text.Trim()))
                            {
                                SearchedBooks.Add(book);
                            }
                            else if (book.BookClass.Contains(txtSearchBook.Text.Trim()))
                            {
                                SearchedBooks.Add(book);
                            }
                            else if (book.BookLang.Contains(txtSearchBook.Text.Trim()))
                            {
                                SearchedBooks.Add(book);
                            }
                            else if (book.BookName.Contains(txtSearchBook.Text.Trim()))
                            {
                                SearchedBooks.Add(book);
                            }
                        }
                        if (SearchedBooks.Count() == 0)
                        {
                            MessageBox.Show("لا تتوفر نتائج!");
                        }
                        else
                        {
                            dgShow.ItemsSource = SearchedBooks.ToList();
                            btnReset.Visibility = Visibility.Visible;
                            logNew.Log(Properties.Settings1.Default.CurrentUsername, "تم اجراء بحث كتب" + " - " + txtSearchBook.Text.Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BooksModel SelectedBook = dgShow.SelectedItem as BooksModel;
                if (MessageBox.Show(" هل تريد مسح الكتاب: " + SelectedBook.BookName + "?", "تأكيد", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    using (DbContextClass db = new DbContextClass())
                    {
                        db.Books.Attach(SelectedBook);
                        db.Books.Remove(SelectedBook);
                        db.SaveChanges();
                        LoadBooksList();
                        wnd.giveAndtakeUc.LoadBooksList();
                        logNew.Log(Properties.Settings1.Default.CurrentUsername, "حذف كتاب" + " - " + SelectedBook.BookName.Trim());

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
