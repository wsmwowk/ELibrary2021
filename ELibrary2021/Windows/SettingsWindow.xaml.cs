using ELibrary2021.DataModel;
using ELibrary2021.Uc;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace ELibrary2021.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            loadUsers();
        }

        public void loadUsers()
        {
            try
            {
                List<UsersModel> HidePasswordModel = new List<UsersModel>();
                using (DbContextClass db = new DbContextClass())
                {
                    foreach (var user in db.Users)
                    {
                        user.password = "*****";
                        HidePasswordModel.Add(user);
                    }
                    if (HidePasswordModel.Count == 0)
                    {
                        MessageBox.Show("!لا يوجد مستخدمين");
                    }
                    else
                    {
                        lstUsers.ItemsSource = HidePasswordModel;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            UsersModel SelectedUser = lstUsers.SelectedItem as UsersModel;
            if (SelectedUser != null)
            {
                editPassUc.txtUserName.Text = SelectedUser.Username.Trim();
                spEditPass.Visibility = Visibility.Visible;
                spAddNewUser.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("!يرجى تحديد المستخدمين من القائمة");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
                spEditPass.Visibility = Visibility.Collapsed;
                spAddNewUser.Visibility = Visibility.Visible;
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
