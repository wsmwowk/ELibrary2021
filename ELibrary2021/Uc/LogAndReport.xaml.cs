using ELibrary2021.Classes;
using ELibrary2021.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ELibrary2021.Uc
{
    /// <summary>
    /// Interaction logic for LogAndReport.xaml
    /// </summary>
    public partial class LogAndReport : UserControl
    {
        LogClass logNew = new LogClass();

        public LogAndReport()
        {
            InitializeComponent();
        }

        #region Logs
        public async void LoadLogs()
        {
            try
            {
                using (DbContextClass db = new DbContextClass())
                {
                    dgShow.ItemsSource = await db.Logs.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion

        #region Reports
        private void btnDailyReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                reportWindowUc.txtReportType.Text = "يومي";
                reportWindowUc.txtReportDate.Text = DateTime.Now.ToString();
                vbReportWindow.Visibility = Visibility.Visible;

                using (DbContextClass db = new DbContextClass())
                {
                    reportWindowUc.txtReportTotalBookNum.Text = db.Books.Count().ToString();
                    var DailyGivedBooks = db.Takers.Where(t => t.TakedDate > DateTime.Now.AddDays(-1) && t.TakedDate < DateTime.Now.AddDays(+1));
                    var DailyReturnedBooks = db.Returner.Where(t => t.ReturnDate > DateTime.Now.AddDays(-1) && t.ReturnDate < DateTime.Now.AddDays(+1));
                    reportWindowUc.txtReportTotalBarrowedBooks.Text = DailyGivedBooks.Count().ToString();
                    reportWindowUc.txtReportTotalRecivedBooks.Text = DailyReturnedBooks.Count().ToString();
                    logNew.Log(Properties.Settings1.Default.CurrentUsername, "انشاء تقرير يومي");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnWeeklyReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DayOfWeek weekStart = DayOfWeek.Saturday; // or Sunday, or whenever
                DateTime startingDate = DateTime.Today;

                while (startingDate.DayOfWeek != weekStart)
                    startingDate = startingDate.AddDays(-1);

                DateTime previousWeekStart = startingDate.AddDays(-7);
                DateTime previousWeekEnd = startingDate.AddDays(-1);

                reportWindowUc.txtReportDate.Text = previousWeekStart.ToShortDateString() + " - " + previousWeekEnd.ToShortDateString() + " الاسبوع الماضي ";
                reportWindowUc.txtReportType.Text = "أسبوعي";
                vbReportWindow.Visibility = Visibility.Visible;

                using (DbContextClass db = new DbContextClass())
                {
                    reportWindowUc.txtReportTotalBookNum.Text = db.Books.Count().ToString();
                    var WeeklyGivedBooks = db.Takers.Where(t => t.TakedDate > previousWeekStart && t.TakedDate < previousWeekEnd);
                    var WeeklyReturnedBooks = db.Returner.Where(t => t.ReturnDate > previousWeekStart && t.ReturnDate < previousWeekEnd);
                    reportWindowUc.txtReportTotalBarrowedBooks.Text = WeeklyGivedBooks.Count().ToString();
                    reportWindowUc.txtReportTotalRecivedBooks.Text = WeeklyReturnedBooks.Count().ToString();
                    logNew.Log(Properties.Settings1.Default.CurrentUsername, "انشاء تقرير أسبوعي");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void btnMonthlyReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var today = DateTime.Today;
                var month = new DateTime(today.Year, today.Month, today.Day);
                var first = month.AddMonths(-1);
                var last = month.AddDays(-1);

                reportWindowUc.txtReportDate.Text = last.ToShortDateString() + " - " + first.ToShortDateString() + " الشهر الماضي ";
                reportWindowUc.txtReportType.Text = "شهري";
                vbReportWindow.Visibility = Visibility.Visible;

                using (DbContextClass db = new DbContextClass())
                {
                    reportWindowUc.txtReportTotalBookNum.Text = db.Books.Count().ToString();
                    var MonthlyGivedBooks = db.Takers.Where(t => t.TakedDate > first && t.TakedDate < last);
                    var MonthlyReturnedBooks = db.Returner.Where(t => t.ReturnDate > first && t.ReturnDate < last);
                    reportWindowUc.txtReportTotalBarrowedBooks.Text = MonthlyGivedBooks.Count().ToString();
                    reportWindowUc.txtReportTotalRecivedBooks.Text = MonthlyReturnedBooks.Count().ToString();
                    logNew.Log(Properties.Settings1.Default.CurrentUsername, "انشاء تقرير شهري");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion 
    }
}
