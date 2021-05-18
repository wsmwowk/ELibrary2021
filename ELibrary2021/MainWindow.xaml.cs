using ELibrary2021.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ELibrary2021
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            giveAndtakeUc.TabGiveAndTakePages.SelectedIndex = 0;
        }

        #region MenuButtons
        private void btnBooksWindow_Click(object sender, RoutedEventArgs e)
        {
            ResetOpacity(btnBooksWindow);
            vbDashUc.Visibility = Visibility.Collapsed;
            vbGivingAndReciveng.Visibility = Visibility.Collapsed;
            vbLogAndReports.Visibility = Visibility.Collapsed;
            vbBooksUc.Visibility = Visibility.Visible;
            BooksUc.LoadBooksList();
            //BooksUc.LoadBooksList();
        }
        private void btnDashWindow_Click(object sender, RoutedEventArgs e)
        {
            ResetOpacity(btnDashWindow);
            vbBooksUc.Visibility = Visibility.Collapsed;
            vbLogAndReports.Visibility = Visibility.Collapsed;
            vbGivingAndReciveng.Visibility = Visibility.Collapsed;
            DashUc.LoadLateTakers();
            DashUc.LoadCurrentUsers();
            DashUc.LoadRecentActivites();
            DashUc.loadCounters();
            vbDashUc.Visibility = Visibility.Visible;
        }
        private void btnGiveWindow_Click(object sender, RoutedEventArgs e)
        {
            ResetOpacity(btnGiveWindow);
            vbDashUc.Visibility = Visibility.Collapsed;
            vbBooksUc.Visibility = Visibility.Collapsed;
            vbLogAndReports.Visibility = Visibility.Collapsed;
            vbGivingAndReciveng.Visibility = Visibility.Visible;
            giveAndtakeUc.LoadBooksList();
            giveAndtakeUc.loadBarrowedBooks();
            giveAndtakeUc.loadReturnerList();
        }
        private void btnReportsWindow_Click(object sender, RoutedEventArgs e)
        {
            ResetOpacity(btnReportsWindow);
            vbDashUc.Visibility = Visibility.Collapsed;
            vbBooksUc.Visibility = Visibility.Collapsed;
            vbGivingAndReciveng.Visibility = Visibility.Collapsed;
            vbLogAndReports.Visibility = Visibility.Visible;
            logAndReportUc.LoadLogs();
        }
        #endregion


        #region WindowControlButtons
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("هل تريد الخروج من البرنامج؟: ", "تأكيد", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                //this.Width = int.Parse(SystemParameters.WorkArea.Width.ToString());
                //this.Height = int.Parse(SystemParameters.WorkArea.Height.ToString());
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
            settings.ShowDialog();
        }
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("هل تريد تسجيل الخروج من البرنامج؟: ", "تأكيد", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
                LoginWindow NewLogin = new LoginWindow();
                NewLogin.Show();
            }
        }
        #endregion

        public void ResetOpacity(Control SelectedBtn)
        {
            btnBooksWindow.Background.Opacity = 1d;
            btnDashWindow.Background.Opacity = 1d;
            btnGiveWindow.Background.Opacity = 1d;
            btnReportsWindow.Background.Opacity = 1d;

            switch (SelectedBtn.Name)
            {
                case "btnBooksWindow":
                    SelectedBtn.Background.Opacity = 0.3d;
                    break;
                case "btnDashWindow":
                    SelectedBtn.Background.Opacity = 0.3d;
                    break;
                case "btnGiveWindow":
                    SelectedBtn.Background.Opacity = 0.3d;
                    break;
                case "btnReportsWindow":
                    SelectedBtn.Background.Opacity = 0.3d;
                    break;
            }
        }
        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
