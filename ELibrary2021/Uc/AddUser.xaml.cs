using ELibrary2021.Classes;
using ELibrary2021.DataModel;
using ELibrary2021.Windows;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : UserControl
    {
        LogClass logNew = new LogClass();
        public AddUser()
        {
            InitializeComponent();
        }

        private void btnAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DbContextClass db = new DbContextClass())
                {
                    if (txtUserName.Text == string.Empty)
                    {
                        txtUserName.Focus();
                    }
                    else if (txtPassword.Password == string.Empty)
                    {
                        txtPassword.Focus();
                    }
                    else
                    {
                        UsersModel NewUser = new UsersModel();
                        NewUser.Username = txtUserName.Text.Trim();
                        NewUser.password = txtPassword.Password.Trim();
                        db.Add(NewUser);
                        db.SaveChanges();

                        var myWindow = Window.GetWindow(this);
                        myWindow.Close();

                        SettingsWindow wnd = new SettingsWindow();
                        wnd.loadUsers();
                        wnd.ShowDialog();
                        txtPassword.Clear();
                        txtUserName.Clear();
                        wnd.spAddNewUser.Visibility = Visibility.Collapsed;
                        logNew.Log(Properties.Settings1.Default.CurrentUsername, "اضافة مستخدم جديد" + " - " + txtUserName.Text.Trim());
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
