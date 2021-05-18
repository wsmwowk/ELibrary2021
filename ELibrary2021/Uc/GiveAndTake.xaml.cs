using ELibrary2021.Classes;
using ELibrary2021.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ELibrary2021.Uc
{
    /// <summary>
    /// Interaction logic for GiveAndTake.xaml
    /// </summary>
    public partial class GiveAndTake : UserControl
    {
        LogClass logNew = new LogClass();
        public GiveAndTake()
        {
            InitializeComponent();
            LoadBooksList();
            loadBarrowedBooks();
            txtDate.SelectedDate = DateTime.Now;
        }

        public void LoadBooksList()
        {
            try
            {
                cmboBooksList.ItemsSource = null;
                using (DbContextClass db = new DbContextClass())
                {
                    foreach (var book in db.Books)
                    {
                        if (!cmboBooksList.Items.Contains(book.BookName + " - " + book.BookLang))
                        {
                            cmboBooksList.Items.Add(book.BookName + " - " + book.BookLang);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void loadBarrowedBooks()
        {
            try
            {
                using (DbContextClass db = new DbContextClass())
                {
                    dgShow.ItemsSource = db.Takers.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void loadReturnerList()
        {
            try
            {
                using (DbContextClass db = new DbContextClass())
                {
                    dgShowReturner.ItemsSource = db.Returner.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmboBooksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string SelectedBookName;
                string SelectedBookLang;
                string fullString = cmboBooksList.SelectedItem.ToString().Trim();
                SelectedBookName = fullString.Substring(0, fullString.IndexOf("-"));
                SelectedBookName = SelectedBookName.Replace("-", "");
                SelectedBookLang = fullString.Replace(SelectedBookName, "");
                SelectedBookLang = SelectedBookLang.Replace("-", "");
                SelectedBookLang = SelectedBookLang.Trim();
                using (DbContextClass db = new DbContextClass())
                {
                    var filterData = db.Books.Where(x => x.BookLang == SelectedBookLang);
                    if (filterData != null)
                    {
                        txtSelectedClasses.Text = filterData.Where(x => x.BookName == SelectedBookName).FirstOrDefault().BookClass.Trim();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnGive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtTakerName.Text == string.Empty)
                {
                    txtTakerName.Focus();
                }
                else if (txtTakerPhone.Text == string.Empty)
                {
                    txtTakerPhone.Focus();
                }
                else if (txtPeriod.Text == string.Empty)
                {
                    txtPeriod.Focus();
                }
                else if (txtDate.Text == string.Empty)
                {
                    txtDate.Focus();
                }
                else if (cmboBooksList.Text == string.Empty)
                {
                    MessageBox.Show("!يرجى اختيار كتاب");
                }
                else
                {
                    using (DbContextClass db = new DbContextClass())
                    {
                        TakerModel takeModel = new TakerModel();
                        takeModel.BookName = cmboBooksList.Text.Trim();
                        takeModel.TakerName = txtTakerName.Text.Trim();
                        takeModel.TakerPhone = txtTakerPhone.Text.Trim();
                        takeModel.TakedDate = txtDate.SelectedDate.Value;
                        takeModel.periodInDays = Convert.ToInt32(txtPeriod.Text.Trim());
                        takeModel.BookClass = txtSelectedClasses.Text.Trim();
                        db.Takers.Add(takeModel);
                        db.SaveChanges();
                        loadBarrowedBooks();
                        clearInputs();
                        MessageBox.Show("تم أعارة كتاب جديد");
                        logNew.Log(Properties.Settings1.Default.CurrentUsername, "اعارة كتاب" + " - " + cmboBooksList.Text.Trim() + " - " + txtTakerName.Text.Trim());

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void clearInputs()
        {
            txtPeriod.Clear();
            txtTakerName.Clear();
            txtTakerPhone.Clear();
            LoadBooksList();
        }
        private void btnSearchTaker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSearchBook.Text != string.Empty)
                {
                    using (DbContextClass db = new DbContextClass())
                    {
                        List<TakerModel> SearchedTakers = new List<TakerModel>();
                        foreach (var Taker in db.Takers)
                        {
                            if (Taker.BookName.Contains(txtSearchBook.Text.Trim()))
                            {
                                SearchedTakers.Add(Taker);
                            }
                            else if (Taker.TakerName.Contains(txtSearchBook.Text.Trim()))
                            {
                                SearchedTakers.Add(Taker);
                            }
                            else if (Taker.TakerPhone.Contains(txtSearchBook.Text.Trim()))
                            {
                                SearchedTakers.Add(Taker);
                            }
                            else if (Taker.BookClass.Contains(txtSearchBook.Text.Trim()))
                            {
                                SearchedTakers.Add(Taker);
                            }
                        }
                        if (SearchedTakers.Count() == 0)
                        {
                            MessageBox.Show("لا تتوفر نتائج!");
                        }
                        else
                        {
                            dgShow.ItemsSource = SearchedTakers.ToList();
                            btnReset.Visibility = Visibility.Visible;
                            logNew.Log(Properties.Settings1.Default.CurrentUsername, " تم اجراء بحث عن مستعير" + " - " + txtSearchBook.Text.Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            loadBarrowedBooks();
            btnReset.Visibility = Visibility.Collapsed;
        }

        private void btnReturnBook_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("هل تم استلام الكتاب؟: ", "تأكيد", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                TakerModel DeleteTaker = dgShow.SelectedItem as TakerModel;
                using (DbContextClass db = new DbContextClass())
                {
                    ReturnedBooks ReturnModel = new ReturnedBooks();
                    ReturnModel.BookName = DeleteTaker.BookName;
                    ReturnModel.ReturnDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    ReturnModel.takerName = DeleteTaker.TakerName;
                    int FinalPeriod = Convert.ToInt32((Convert.ToDateTime(DateTime.Now.ToShortDateString()) - DeleteTaker.TakedDate).TotalDays);
                   
                    if (FinalPeriod > DeleteTaker.periodInDays)
                    {
                        ReturnModel.period = FinalPeriod - DeleteTaker.periodInDays;
                        if (MessageBox.Show("هل تريد استعادة كتاب متأخر عن الأسترداد لفترة: " + ReturnModel.period + "من الأيام!", "استرداد متأخر!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            db.Returner.Add(ReturnModel);
                            db.SaveChanges();

                            logNew.Log(Properties.Settings1.Default.CurrentUsername, "اعادة كتاب متأخر" + " - " + DeleteTaker.BookName + " - " + DeleteTaker.TakerName);

                            db.Takers.Attach(DeleteTaker);
                            db.Takers.Remove(DeleteTaker);
                            db.SaveChanges();
                            loadBarrowedBooks();
                            loadReturnerList();
                        }
                    }
                    else
                    {
                        ReturnModel.period = 0;
                        db.Returner.Add(ReturnModel);
                        db.SaveChanges();

                        logNew.Log(Properties.Settings1.Default.CurrentUsername, "اعادة كتاب" + " - " + DeleteTaker.BookName + " - " + DeleteTaker.TakerName);

                        db.Takers.Attach(DeleteTaker);
                        db.Takers.Remove(DeleteTaker);
                        db.SaveChanges();
                        loadBarrowedBooks();
                        loadReturnerList();
                    }
                }


            }
        }

        private void btnResetReturnerSearch_Click(object sender, RoutedEventArgs e)
        {
            loadReturnerList();
            btnResetReturnerSearch.Visibility = Visibility.Collapsed;
        }

        private void btnSearchReturnedTaker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSearchReturnedBook.Text != string.Empty)
                {
                    using (DbContextClass db = new DbContextClass())
                    {
                        List<ReturnedBooks> SearchedReturner = new List<ReturnedBooks>();
                        foreach (var Taker in db.Returner)
                        {
                            if (Taker.BookName.Contains(txtSearchReturnedBook.Text.Trim()))
                            {
                                SearchedReturner.Add(Taker);
                            }
                            else if (Taker.takerName.Contains(txtSearchReturnedBook.Text.Trim()))
                            {
                                SearchedReturner.Add(Taker);
                            }
                        }
                        if (SearchedReturner.Count() == 0)
                        {
                            MessageBox.Show("لا تتوفر نتائج!");
                        }
                        else
                        {
                            dgShowReturner.ItemsSource = SearchedReturner.ToList();
                            btnResetReturnerSearch.Visibility = Visibility.Visible;
                            logNew.Log(Properties.Settings1.Default.CurrentUsername, " تم اجراء بحث عن كتاب مسترجع" + " - " + txtSearchReturnedBook.Text.Trim());
                        }
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
