using CtrlV.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CtrlV
{
    public partial class ImagePreview : Form
    {
        UploadedImage ui;

        public ImagePreview(UploadedImage ui)
        {
            InitializeComponent();

            this.ui = ui;

            pictureBox1.Image = CtrlvApi.FetchImage(ui);
        }

        private void showInBrowserButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://ctrlv.cz/" + ui.link);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            CtrlvApi.DeleteImage(ui.link, ui.token);
            StorageManager.DeleteImage(ui.link);
            Close();
        }
    }
}
