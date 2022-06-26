using CtrlV.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CtrlV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox1.Text = UploadImage("https://webhook.site/d6bd5dd4-b8b1-45e5-a460-f1d3b10e5b39", @"C:\Users\harab\Pictures\bigmac.png");
            string response = CtrlvApi.UploadImage("https://ctrlv.cz/upload.php", @"C:\Users\harab\Pictures\bigmac.png");

            UploadResponse ur = JsonConvert.DeserializeObject<UploadResponse>(response);
            textBox1.Text = ur.link + " " + ur.token;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text.Split(' ')[0];
            string token = textBox1.Text.Split(' ')[1];

            CtrlvApi.DeleteImage(id, token);
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text.Split(' ')[0];
            string token = textBox1.Text.Split(' ')[1];

            CtrlvApi.EditImage(id, token, 3);
        }
    }
}
