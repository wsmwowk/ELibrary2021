using ELibrary2021.Classes;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ELibrary2021.Uc
{
    /// <summary>
    /// Interaction logic for ReportPrintWindow.xaml
    /// </summary>
    public partial class ReportPrintWindow : UserControl
    {
        MainWindow wnd = (MainWindow)System.Windows.Application.Current.MainWindow;
        LogClass logNew = new LogClass();
        private readonly Random _random = new Random();
        public ReportPrintWindow()
        {
            InitializeComponent();
        }

        private void btnExitReportWindow_Click(object sender, RoutedEventArgs e)
        {
            wnd.logAndReportUc.vbReportWindow.Visibility = Visibility.Collapsed;
        }

        public void FlipTexts(TextBlock[] controls)
        {
            // flip The Texts
            ScaleTransform myScaleTransform = new ScaleTransform();
            myScaleTransform.ScaleX = -1;
            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(myScaleTransform);

            foreach (var Item in controls)
            {
                Item.RenderTransformOrigin = new Point(0.5, 0.5);
                Item.RenderTransform = transformGroup;
            }
        }

        public void FlipTextsBack(TextBlock[] controls)
        {
            // flip The Texts
            ScaleTransform myScaleTransform = new ScaleTransform();
            myScaleTransform.ScaleX = 1;
            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(myScaleTransform);
            foreach (var Control in controls)
            {
                Control.RenderTransformOrigin = new Point(0, 0);
                Control.RenderTransform = transformGroup;
            }
        }

        private void btnPrintReport_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();

            TextBlock[] textBlocks = new TextBlock[] { lblReport, lblReportDate, lblTotalBarowedBooks, lblTotalBookNumber, lblTotalReturnedBooks, txtReportDate, txtReportTotalBarrowedBooks, txtReportTotalBookNum, txtReportTotalRecivedBooks, txtReportType };
            
            if (printDlg.ShowDialog() == true)
            {
                FlipTexts(textBlocks);
                    wnd.logAndReportUc.reportWindowUc.btnPrintReport.Visibility = Visibility.Hidden;
                    wnd.logAndReportUc.reportWindowUc.btnExitReportWindow.Visibility = Visibility.Hidden;

                //now print the visual to printer to fit on the one page.
                printDlg.PrintVisual(MainGrid, "Elibrary2021 Report");
            }

            FlipTextsBack(textBlocks);
            wnd.logAndReportUc.reportWindowUc.btnPrintReport.Visibility = Visibility.Visible;
            wnd.logAndReportUc.reportWindowUc.btnExitReportWindow.Visibility = Visibility.Visible;
        }

    }
}
