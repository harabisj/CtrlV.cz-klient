using CtrlV.Data;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CtrlV
{
    public partial class ImagePreview : Form
    {
        private readonly UploadedImage ui;

        public ImagePreview(UploadedImage ui)
        {
            InitializeComponent();

            this.ui = ui;

            ImgPreview.Image = CtrlvApi.FetchImage(ui);
        }

        private void showInBrowserButton_Click(object sender, EventArgs e)
        {
            Process.Start("https://ctrlv.cz/" + ui.link);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            CtrlvApi.DeleteImage(ui.link, ui.token);
            StorageManager.DeleteImage(ui.link);
            Close();
        }
    }
}
