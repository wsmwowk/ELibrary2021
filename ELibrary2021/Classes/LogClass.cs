using ELibrary2021.DataModel;
using System;
using System.Windows;

namespace ELibrary2021.Classes
{
    class LogClass
    {
        MainWindow wnd = (MainWindow)System.Windows.Application.Current.MainWindow;
        public void Log(string UserName, string Operation)
        {
            try
            {
                using (DbContextClass db = new DbContextClass())
                {
                    LogModel Newlog = new LogModel();
                    Newlog.OperationDate = DateTime.Now.ToLocalTime();
                    Newlog.username = UserName;
                    Newlog.operation = Operation;
                    db.Logs.Add(Newlog);
                    db.SaveChanges();
                    wnd.logAndReportUc.LoadLogs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ResetlOG()
        {
            //try
            //{
            using (DbContextClass db = new DbContextClass())
            {
                foreach (var item in db.Logs)
                {
                    if (Convert.ToDateTime(item.OperationDate).ToShortDateString() != DateTime.Now.ToShortDateString())
                    {
                        db.Logs.Attach(item);
                        db.Logs.Remove(item);
                    }
                }
                db.SaveChanges();
                wnd.logAndReportUc.LoadLogs();
            }
            //}
            //catch (Exception EX)
            //{
            //    MessageBox.Show(EX.Message);
            //}
        }
    }
}
