using ELibrary2021.DataModel;
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
    /// Interaction logic for ClassificationUc.xaml
    /// </summary>
    public partial class ClassificationUc : UserControl
    {
        string CheckedClasses = string.Empty;
        MainWindow wnd = (MainWindow)System.Windows.Application.Current.MainWindow;
        public ClassificationUc()
        {
            InitializeComponent();
        }

        private void rdClass_Checked(object sender, RoutedEventArgs e)
        {
            cmboBookClass.IsEnabled = true;
            cmboBookLang.IsEnabled = false;
        }

        private void rdLang_Checked(object sender, RoutedEventArgs e)
        {
            cmboBookClass.IsEnabled = false;
            cmboBookLang.IsEnabled = true;
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

        private void btnClass_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rdClass.IsChecked == false && rdLang.IsChecked == false)
                {
                    MessageBox.Show("!يرجى تحديد نوع التصنيف");
                }
                else
                {
                    using (DbContextClass db = new DbContextClass())
                    {
                        if (rdLang.IsChecked == true)
                        {
                            if (cmboBookLang.SelectedIndex == -1)
                            {
                                MessageBox.Show("!يرجى اختيار اللغة المطلوبة");
                            }
                            else
                            {
                                //BooksModel ClassedByLangBooks = ;
                                wnd.BooksUc.dgShow.ItemsSource = db.Books.Where(x => x.BookLang.Contains(cmboBookLang.Text.Trim())).ToList();
                                wnd.BooksUc.classification.clearInputs();
                                wnd.BooksUc.btnReset.Visibility = Visibility.Visible;
                                wnd.BooksUc.vbClassBooks.Visibility = Visibility.Collapsed;
                            }
                        }
                        else
                        {
                            int i = 0;
                            List<BooksModel> ClassedByClassList = new List<BooksModel>();
                            foreach(CheckBox classItem in cmboBookClass.Items)
                            {
                                if (classItem.IsChecked == true)
                                {
                                    BooksModel model = db.Books.Where(x => x.BookClass.Contains(classItem.Content.ToString())).FirstOrDefault();
                                    if (model != null)
                                    {
                                        ClassedByClassList.Add(model);
                                    }
                                }
                                else
                                {
                                    i = i + 1;
                                }
                            }
                            if (i == 24)
                            {
                                MessageBox.Show("!يرجى تحديد فئة واحدة على الأقل");
                            }
                            else if (ClassedByClassList.Count == 0)
                            {
                                MessageBox.Show("!لا توجد كتب تحت هذه الفئات");
                            }
                            else
                            {
                                wnd.BooksUc.dgShow.ItemsSource = ClassedByClassList.ToList();
                                wnd.BooksUc.classification.clearInputs();
                                wnd.BooksUc.btnReset.Visibility = Visibility.Visible;
                                wnd.BooksUc.vbClassBooks.Visibility = Visibility.Collapsed;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            clearInputs();
            wnd.BooksUc.vbClassBooks.Visibility = Visibility.Collapsed;
        }

        public void clearInputs()
        {
            rdClass.IsChecked = false;
            rdLang.IsChecked = false;
            txtSelectedClasses.Text = string.Empty;
            cmboBookLang.SelectedIndex = -1;
            cmboBookLang.IsEnabled = false;
            foreach (CheckBox classItem in cmboBookClass.Items)
            {
                if (classItem.IsChecked == true)
                {
                    classItem.IsChecked = false;
                }
            }
            cmboBookClass.IsEnabled = false;
        }


    }
}
