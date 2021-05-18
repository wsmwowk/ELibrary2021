using ELibrary2021.Classes;
using ELibrary2021.DataModel;
using ELibrary2021.Windows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ELibrary2021.Uc
{
    /// <summary>
    /// Interaction logic for editUserPassword.xaml
    /// </summary>
    public partial class editUserPassword : UserControl
    {
        LogClass logNew = new LogClass();
        public editUserPassword()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DbContextClass db = new DbContextClass())
                {
                    UsersModel OldUserPass = db.Users.Where(x => x.Username == txtUserName.Text.Trim()).FirstOrDefault();
                    if (txtOldpass.Password.Trim() == OldUserPass.password.Trim())
                    {
                        db.Users.Attach(OldUserPass);
                        db.Users.Remove(OldUserPass);
                        db.SaveChanges();

                        UsersModel NewUserPass = new UsersModel();
                        NewUserPass.Username = txtUserName.Text.Trim();
                        NewUserPass.password = txtNewpass.Password.Trim();
                        db.Users.Add(NewUserPass);
                        db.SaveChanges();
                        MessageBox.Show("تم تغيير كلمة المرور");

                        var myWindow = Window.GetWindow(this);
                        myWindow.Close();

                        SettingsWindow wnd = new SettingsWindow();
                        wnd.loadUsers();
                        wnd.ShowDialog();
                        txtOldpass.Clear();
                        txtNewpass.Clear();
                        wnd.spEditPass.Visibility = Visibility.Collapsed;
                        logNew.Log(Properties.Settings1.Default.CurrentUsername, "تغيير كلمة المرور" + " - " + txtUserName.Text.Trim());
                    }
                    else
                    {
                        MessageBox.Show("!كلمة المرور القديمة غير صالحة");
                        txtOldpass.Focus();
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
