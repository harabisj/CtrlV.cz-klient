using System.Windows;

namespace CtrlV2
{
    /// <summary>
    /// Interakční logika pro SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            SaveTime.Items.Clear();
            foreach (var type in App.DeleteTypes)
                SaveTime.Items.Add(type);

            SaveTime.SelectedIndex = App.Storage.DeleteType;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SaveTime.SelectedIndex >= 0)
            {
                App.Storage.DeleteType = SaveTime.SelectedIndex;
                Close();
            }
        }
    }
}
