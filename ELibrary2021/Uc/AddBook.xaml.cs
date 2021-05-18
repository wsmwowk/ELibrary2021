using ELibrary2021.Classes;
using ELibrary2021.DataModel;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ELibrary2021.Uc
{
    /// <summary>
    /// Interaction logic for AddBook.xaml
    /// </summary>
    public partial class AddBook : UserControl
    {
        MainWindow wnd = (MainWindow)System.Windows.Application.Current.MainWindow;
        string CheckedClasses = string.Empty;
        LogClass LogNew = new LogClass();

        public AddBook()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
            wnd.BooksUc.vbAddBookUc.Visibility = Visibility.Collapsed;
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtBookName.Text == string.Empty)
                {
                    txtBookName.Focus();
                }
                else if (txtBookAuthorName.Text == string.Empty)
                {
                    txtBookAuthorName.Focus();
                }
                else if (txtBookPartsNum.Text == string.Empty)
                {
                    txtBookPartsNum.Focus();
                }
                else if (txtSelectedClasses.Text == string.Empty || txtSelectedClasses.Text == " ")
                {
                    if (cmboBookClass.SelectedIndex == -1)
                    {
                        MessageBox.Show("يرجى اختيار فئة واحدة على الأقل!");
                    }
                    else if (cmboBookLang.SelectedIndex == -1)
                    {
                        MessageBox.Show("يرجى تحديد اللغة!");
                    }
                    else
                    {
                        using (DbContextClass db = new DbContextClass())
                        {
                            BooksModel NewBook = new BooksModel();
                            NewBook.BookName = txtBookName.Text.Trim();
                            NewBook.BookAuthor = txtBookAuthorName.Text.Trim();
                            NewBook.BookParts = Convert.ToInt32(txtBookPartsNum.Text.Trim());
                            if (CheckedClasses == string.Empty)
                            {
                                NewBook.BookClass = cmboBookClass.Text.Trim();
                            }
                            else
                            {
                                NewBook.BookClass = CheckedClasses.Remove(CheckedClasses.LastIndexOf(","));
                            }
                            NewBook.BookLang = cmboBookLang.Text.Trim();
                            db.Books.AddAsync(NewBook);
                            db.SaveChangesAsync();
                            wnd.BooksUc.LoadBooksList();
                            MessageBox.Show( " تم أضافة الكتاب " + " - " + txtBookName.Text.Trim());
                            LogNew.Log(Properties.Settings1.Default.CurrentUsername, "اضافة كتاب" + " - " + txtBookName.Text.Trim());
                            wnd.BooksUc.vbAddBookUc.Visibility = Visibility.Collapsed;
                            wnd.giveAndtakeUc.LoadBooksList();
                            ClearInputs();
                        }
                    }
                }
                else
                {
                    if (cmboBookLang.SelectedIndex == -1)
                    {
                        MessageBox.Show("يرجى تحديد اللغة!");
                    }
                    else
                    {
                        using (DbContextClass db = new DbContextClass())
                        {
                            BooksModel NewBook = new BooksModel();
                            NewBook.BookName = txtBookName.Text.Trim();
                            NewBook.BookAuthor = txtBookAuthorName.Text.Trim();
                            NewBook.BookParts = Convert.ToInt32(txtBookPartsNum.Text.Trim());
                            if (CheckedClasses == string.Empty)
                            {
                                NewBook.BookClass = cmboBookClass.Text.Trim();
                            }
                            else
                            {
                                NewBook.BookClass = CheckedClasses.Remove(CheckedClasses.LastIndexOf(","));
                            }
                            NewBook.BookLang = cmboBookLang.Text.Trim();
                            db.Books.AddAsync(NewBook);
                            db.SaveChangesAsync();
                            wnd.BooksUc.LoadBooksList();
                            MessageBox.Show(txtBookName.Text.Trim() + " : تم أضافة الكتاب");
                            LogNew.Log(Properties.Settings1.Default.CurrentUsername, "اضافة كتاب");
                            wnd.BooksUc.vbAddBookUc.Visibility = Visibility.Collapsed;
                            wnd.giveAndtakeUc.LoadBooksList();
                            ClearInputs();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!CheckedClasses.Contains((string)((CheckBox)sender).Content))
            {
                CheckedClasses += (string)((CheckBox)sender).Content + " , ";
                txtSelectedClasses.Text += (string)((CheckBox)sender).Content + " , ";
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            string newString = CheckedClasses.Replace((string)((CheckBox)sender).Content + " , ", "");
            CheckedClasses = newString;
            txtSelectedClasses.Text = newString;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public void ClearInputs()
        {
            txtBookAuthorName.Clear();
            txtBookName.Clear();
            txtBookPartsNum.Clear();
            txtSelectedClasses.Text = string.Empty;
            cmboBookClass.SelectedIndex = -1;
            cmboBookLang.SelectedIndex = -1;
            foreach (CheckBox ClassSelection in cmboBookClass.Items)
            {
                if (ClassSelection.IsChecked == true)
                {
                    ClassSelection.IsChecked = false;
                }
            }

        }
    }
}
