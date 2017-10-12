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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfUSBDetection
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
            HwndSource hwndSource = HwndSource.FromHwnd(Process.GetCurrentProcess().MainWindowHandle);
            if (hwndSource != null)
            {
                IntPtr windowHandle = hwndSource.Handle;
                hwndSource.AddHook(UsbNotificationHandler);
                USBDetector.RegisterUsbDeviceNotification(windowHandle);
            }
            USBButton.IsEnabled = false;
        }

        private IntPtr UsbNotificationHandler(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            if (msg == USBDetector.UsbDevicechange)
            {
                switch ((int)wparam)
                {
                    case USBDetector.UsbDeviceRemoved:
                        MessageBox.Show("USB Removed");
                        break;
                    case USBDetector.NewUsbDeviceConnected:
                        MessageBox.Show("New USB Detected");
                        break;
                }
            }

            handled = false;
            return IntPtr.Zero;
        }
    }
}
