using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dqc = Windows.System.DispatcherQueueController.CreateOnDedicatedThread();
            var dq = dqc.DispatcherQueue;

            bool isQueued = dq.TryEnqueue(
                async () =>
                {
                    Debug.WriteLine("entered async method");
                    Windows.ApplicationModel.DataTransfer.DataPackageView content = Windows.ApplicationModel.DataTransfer.Clipboard.GetContent();
                    Debug.WriteLine("After Clipboard.GetContent");
                    Windows.Foundation.IAsyncOperation<string> getTextOperation = content.GetTextAsync();
                    Debug.WriteLine("After GetTextAsync");
                    string s = await getTextOperation;
                    //string s = getTextOperation.AsTask().GetAwaiter().GetResult();
                    Debug.WriteLine(s);
                    this.clipboardResultTextBox.Text = s;
                    Debug.WriteLine("after setting text box");
                });


            // use wpf dispatcher to get over to the UI thread?
            //Action updateOutputText = () =>
            //{
            //        Debug.WriteLine("entered async method");
            //        Windows.ApplicationModel.DataTransfer.DataPackageView content = Windows.ApplicationModel.DataTransfer.Clipboard.GetContent();
            //        Debug.WriteLine("After Clipboard.GetContent");
            //        Windows.Foundation.IAsyncOperation<string> getTextOperation = content.GetTextAsync();
            //        Debug.WriteLine("After GetTextAsync");
            //        //string s = await getTextOperation;
            //        string s = getTextOperation.AsTask().GetAwaiter().GetResult();
            //        Debug.WriteLine(s);
            //        this.clipboardResultTextBox.Text = s;
            //        Debug.WriteLine("after setting text box");
            //    };
            //this.Dispatcher.BeginInvoke(updateOutputText);
        }
    }
}
