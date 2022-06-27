using CtrlV2.Data;
using System;
using System.Diagnostics;
using System.Windows;

namespace CtrlV2
{
    /// <summary>
    /// Interakční logika pro PreviewWindow.xaml
    /// </summary>
    public partial class PreviewWindow : Window
    {
        ImageData data;

        public PreviewWindow(ImageData data)
        {
            InitializeComponent();
            this.data = data;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (data.Image is null)
            {
                data.Image = await App.API.FetchImage(data);
            }

            ImageView.Source = data.Image;
        }

        private void ShowBrowser_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://ctrlv.cz/" + data.Link) { UseShellExecute = true });
        }

        private async void DeleteImage_Click(object sender, RoutedEventArgs e)
        {
            if (await App.API.DeleteImage(data.Link, data.Token))
            {
                App.Storage.Remove(data);
                Close();
            }
        }
    }
}
