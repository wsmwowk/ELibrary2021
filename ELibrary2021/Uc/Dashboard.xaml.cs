using ELibrary2021.DataModel;
using ELibrary2021.Windows;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ELibrary2021.Uc
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        public Dashboard()
        {
            InitializeComponent();
            LoadLateTakers();
            LoadCurrentUsers();
            LoadRecentActivites();
            loadCounters();
        }
        
        public void loadCounters()
        {
            try
            {
                using (DbContextClass db = new DbContextClass())
                {
                    txtTotalBooksNumber.Text = db.Books.Count().ToString();
                    txtGivedBooks.Text = db.Takers.Count().ToString();
                    txtReturnedBooksNum.Text = db.Returner.Count().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadLateTakers()
        {
            try
            {
                lstLateTakers.Items.Clear();
                int FinalPeriod = 0;
                int dayCount = 0;
                using (DbContextClass db = new DbContextClass())
                {
                    foreach (var item in db.Takers)
                    {
                        FinalPeriod = Convert.ToInt32((Convert.ToDateTime(DateTime.Now.ToShortDateString()) - item.TakedDate).TotalDays);
                        if (FinalPeriod > item.periodInDays)
                        {
                            dayCount = FinalPeriod - item.periodInDays;
                            lstLateTakers.Items.Add(item.TakerName + " - " + item.BookName + " - " + dayCount);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadCurrentUsers()
        {
            try
            {
                lstCurrentUsers.Items.Clear();
                using (DbContextClass db = new DbContextClass())
                {
                    foreach (var item in db.Users)
                    {
                            lstCurrentUsers.Items.Add(item.Username);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadRecentActivites()
        {
            try
            {
                lstLastOperations.Items.Clear();
                using (DbContextClass db = new DbContextClass())
                {
                    var mostUsedTags = db.Logs.OrderByDescending(t => t.OperationDate).Take(5);
                    foreach (var item in mostUsedTags)
                    {
                        lstLastOperations.Items.Add(item.operation + " - " + item.username);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnGoToLog_Click(object sender, RoutedEventArgs e)
        {
            wnd.ResetOpacity(wnd.btnReportsWindow);
            wnd.vbDashUc.Visibility = Visibility.Collapsed;
            wnd.vbLogAndReports.Visibility = Visibility.Visible;
            wnd.logAndReportUc.LoadLogs();
        }

        private void btnGoToUsers_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow Users = new SettingsWindow();
            Users.loadUsers();
            Users.Show();
        }

        private void btnGoToGiveAndTake_Click(object sender, RoutedEventArgs e)
        {
            wnd.ResetOpacity(wnd.btnGiveWindow);
            wnd.vbDashUc.Visibility = Visibility.Collapsed;
            wnd.giveAndtakeUc.TabGiveAndTakePages.SelectedIndex = 1;
            wnd.vbGivingAndReciveng.Visibility = Visibility.Visible;
            wnd.giveAndtakeUc.LoadBooksList();
            wnd.giveAndtakeUc.loadBarrowedBooks();
            wnd.giveAndtakeUc.loadReturnerList();
        }
    }
}
