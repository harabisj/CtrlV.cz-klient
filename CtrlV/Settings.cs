using System;
using System.Windows.Forms;

namespace CtrlV
{
    public partial class Settings : Form
    {
        public static string[] deleteTypes = new string[]
        {
            "hodina", "den", "týden", "měsíc", "rok",
        };

        public Settings()
        {
            InitializeComponent();

            foreach (string type in deleteTypes)
                comboBox.Items.Add(type);

            comboBox.SelectedIndex = StorageManager.GetDeleteType() - 2;
        }

        private void comboBox_TextChanged(object sender, EventArgs e)
        {
            StorageManager.UpdateDeleteType(comboBox.SelectedIndex + 2);
        }
    }
}
