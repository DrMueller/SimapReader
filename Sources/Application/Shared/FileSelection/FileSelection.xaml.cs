using System.Windows;
using System.Windows.Forms;

namespace Mmu.SimapReader.Shared.FileSelection
{
    /// <summary>
    ///     Interaction logic for FileSelection.xaml
    /// </summary>
    public partial class FileSelection
    {
        public static readonly DependencyProperty SelectedFileProperty =
            DependencyProperty.Register(nameof(SelectedFile), typeof(string), typeof(FileSelection));

        public FileSelection()
        {
            InitializeComponent();
        }

        public string SelectedFile
        {
            get => (string)GetValue(SelectedFileProperty);
            set => SetValue(SelectedFileProperty, value);
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new OpenFileDialog();

            dialog.Filter = "ZIP Files (*.zip)|";

            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SelectedFile = dialog.FileName;
            }
        }
    }
}