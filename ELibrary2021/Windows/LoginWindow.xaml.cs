using ELibrary2021.Classes;
using ELibrary2021.DataModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ELibrary2021
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        MainWindow mainWindow = new MainWindow();
        LogClass ResetLog = new LogClass();
        public LoginWindow()
        {
            InitializeComponent();
            ResetLog.ResetlOG();
            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DbContextClass db = new DbContextClass())
                {
                    UsersModel checkUser = db.Users.Where(u => u.Username == txtUsername.Text.Trim()).FirstOrDefault();
                    if (checkUser != null)
                    {
                        if (checkUser.password == txtPassword.Password.Trim())
                        {
                            mainWindow.Show();
                            this.Hide();

                            Properties.Settings1.Default.CurrentUsername = txtUsername.Text;
                            Properties.Settings1.Default.Save();

                            ResetLog.Log(Properties.Settings1.Default.CurrentUsername, "تسجيل دخول");
                        }
                        else
                        {
                            MessageBox.Show(" عذراُ, كلمة المرور غير صحيحة", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(" عذراُ, هذا المستخدم غير مسجل في قاعدة البيانات ", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region TxtsAnimation
        private void txtUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            lblUser.Visibility = Visibility.Collapsed;
        }
        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            lblPass.Visibility = Visibility.Collapsed;
        }
        private void txtUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text.Trim() == string.Empty)
            {
                lblUser.Visibility = Visibility.Visible;
            }
        }
        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == string.Empty)
            {
                lblPass.Visibility = Visibility.Visible;
            }
        }
        private void lblUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtPassword.Password.Length == 0)
            {
                lblPass.Visibility = Visibility.Visible;
            }
            lblUser.Visibility = Visibility.Collapsed;
            txtUsername.Focus();
        }
        private void lblPass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtUsername.Text.Trim() == string.Empty)
            {
                lblUser.Visibility = Visibility.Visible;
            }
            lblPass.Visibility = Visibility.Collapsed;
            txtPassword.Focus();
        }
        #endregion

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("هل تريد الخروج من البرنامج؟: ", "تأكيد", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
