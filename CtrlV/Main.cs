using CtrlV.Data;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CtrlV
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            /*Properties.Settings.Default.UploadedImages = "{images: []}";
            Properties.Settings.Default.Save();*/

            ReloadImages();
        }

        private void ReloadImages()
        {
            imageList.Images.Clear();
            ImagesList.Items.Clear();

            foreach (UploadedImage ui in StorageManager.LoadImages())
            {
                imageList.Images.Add(ui.link, CtrlvApi.FetchImage(ui));
                ImagesList.Items.Add(ui.link, ui.link);
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsImage())
            {
                notifyIcon.ShowBalloonTip(
                   2,
                   "Trochu mimo, bráško",
                   "Nemáš tam obrázek :((",
                   ToolTipIcon.None
                );
                return;
            }

            try
            {
                ImageConverter ic = new ImageConverter();
                Image i = Clipboard.GetImage();
                byte[] bytes = (byte[])ic.ConvertTo(i, typeof(byte[]));

                string response = CtrlvApi.UploadImage(bytes);
                UploadResponse ur = JsonConvert.DeserializeObject<UploadResponse>(response);

                Clipboard.SetText("https://ctrlv.cz/" + ur.link);
                StorageManager.StoreImage(ur);

                if (StorageManager.GetDeleteType() != 4)
                    CtrlvApi.EditImage(ur.link, ur.token, StorageManager.GetDeleteType());

                ReloadImages();

                notifyIcon.ShowBalloonTip(
                    1,
                    "Máš to tam, bráško",
                    "Ukládám na nastavenou dobu " + Settings.deleteTypes[StorageManager.GetDeleteType()],
                    ToolTipIcon.None
                );
            }
            catch (Exception ex)
            {
                notifyIcon.ShowBalloonTip(
                    2,
                    "Chyba!",
                    $"Došlo k chybě ({ex.Message})",
                    ToolTipIcon.None
                );
            }
        }

        private void ImagesList_DoubleClick(object sender, EventArgs e)
        {
            if (ImagesList.SelectedItems.Count != 1)
                return;

            new ImagePreview(StorageManager.GetImage(ImagesList.SelectedItems[0].Text)).ShowDialog();
            ReloadImages();
        }

        private void galleryMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            new Settings().ShowDialog();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
