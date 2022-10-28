using Microsoft.Win32;
using System.IO;
using System.Windows;


namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OnClickCopy(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(textBox.SelectedText);
        }
        private void OnClickCut(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(textBox.SelectedText);
            textBox.SelectedText = string.Empty;
        }

        private void OnClickOpen(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() ?? false)
            {
                using (var reader = new StreamReader(openFileDialog.FileName))
                {
                    textBox.Text = reader.ReadToEnd();
                    textBox.IsEnabled = true;
                }
            }
        }
        private void OnClickNew(object sender, RoutedEventArgs e)
        {
            textBox.IsEnabled = true;
            textBox.Text = "";
        }

        private void OnClickPaste(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                textBox.SelectedText = Clipboard.GetText();
            }
        }
        private void OnClickExit(object sender, RoutedEventArgs e)
        {
            Close();    
        }

        private void AlignCenter(object sender, RoutedEventArgs e)
        {
            textBox.TextAlignment = TextAlignment.Center;
        }

        private void AlignLeft(object sender, RoutedEventArgs e)
        {
            textBox.TextAlignment = TextAlignment.Left;
        }
        private void AlignRight(object sender, RoutedEventArgs e)
        {
            textBox.TextAlignment = TextAlignment.Right;
        }

        private void OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateCaretPosition();
        }
        private void UpdateCaretPosition()
        {
            int row = textBox.GetLineIndexFromCharacterIndex(textBox.CaretIndex);
            int column = textBox.CaretIndex - textBox.GetLineIndexFromCharacterIndex(row);

            currentColumn.Text = $"Col {column + 1}";
            currentLine.Text = $"Ln {row + 1}";
        }

    }
}
