using CtrlV2.Data;
using Microsoft.Toolkit.Uwp.Notifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CtrlV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        private const int GWL_STYLE = -16;
        private const int WS_MAXIMIZEBOX = 0x10000;
        private const int WS_MINIMIZEBOX = 0x20000;

        bool close = false;
        readonly CtrlvApi api;
        readonly StorageManager storage;
        
        public MainWindow()
        {
            InitializeComponent();
            SourceInitialized += MainWindow_SourceInitialized;
            api = App.API;
            storage = App.Storage;
            storage.StorageChanged += Storage_StorageChanged;
            storage.LoadImages();
        }

        private void MainWindow_SourceInitialized(object? sender, EventArgs e)
        {
            IntPtr handle = new WindowInteropHelper(this).Handle;
            SetWindowLong(handle, GWL_STYLE, GetWindowLong(handle, GWL_STYLE) & ~WS_MAXIMIZEBOX & ~WS_MINIMIZEBOX);
        }

        private async void Storage_StorageChanged()
        {
            ImagesList.Items.Clear();
            foreach (var image in storage)
            {
                if (image.Image is null)
                    image.Image = await App.API.FetchImage(image);

                ImagesList.Items.Add(image);
            }
        }

        private void MenuItemGallery_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Visible;
        }

        private void MenuItemSettings_Click(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().ShowDialog();
        }

        private void MenuItemClose_Click(object sender, RoutedEventArgs e)
        {
            close = true;
            Close();
            Environment.Exit(0);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!close)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private async void TaskbarIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                try
                {
                    PngBitmapEncoder png = new PngBitmapEncoder();
                    byte[] buffer;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        png.Frames.Add(BitmapFrame.Create(Clipboard.GetImage()));
                        png.Save(ms);
                        buffer = ms.ToArray();
                    }

                    string response = await api.UploadImage(buffer);
                    UploadResponse ur = JsonConvert.DeserializeObject<UploadResponse>(response);

                    Clipboard.SetText("https://ctrlv.cz/" + ur.Link);
                    ImageData data = new ImageData(ur);
                    storage.Add(data);

                    if (storage.DeleteType != 4)
                        await api.EditImage(ur.Link, ur.Token, storage.DeleteType);

                    ToastNotificationManagerCompat.OnActivated += args =>
                    {
                        if (args.Argument == "CtrlVopenUrl")
                        {
                            Process.Start(new ProcessStartInfo("https://ctrlv.cz/" + ur.Link) { UseShellExecute = true });
                        }
                    };

                    new ToastContentBuilder()
                        .AddText("Máš to tam, bráško", AdaptiveTextStyle.Caption)
                        .AddText("Ukládám na nastavenou dobu - " + App.DeleteTypes[storage.DeleteType], AdaptiveTextStyle.BodySubtle)
                        .AddButton(new ToastButton("Otevřít", "CtrlVopenUrl"))
                        .Show();
                }
                catch (Exception ex)
                {
                    new ToastContentBuilder()
                        .AddText("Chyba!", AdaptiveTextStyle.Caption)
                        .AddText("Došlo k chybě: " + ex.Message, AdaptiveTextStyle.Caption)
                        .Show();
                }
            }

            else
            {
                new ToastContentBuilder()
                        .AddText("Bráško...", AdaptiveTextStyle.Caption)
                        .AddText("Musí to být fotka :(", AdaptiveTextStyle.BodySubtle)
                        .Show();
            }
        }

        private void ImagesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ImagesList.SelectedIndex >= 0)
            {
                new PreviewWindow((ImageData)ImagesList.SelectedItem).ShowDialog();
            }
        }
    }
}
