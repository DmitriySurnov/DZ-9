using Microsoft.Win32;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private new readonly Dictionary<string, MenuItem> Language = new Dictionary<string, MenuItem>();
        public MainWindow()
        {
            InitializeComponent();
            Language.Add("en", PM_1_3_2);
            Language.Add("ru", PM_1_3_1);
            Language.Add("es", PM_1_3_3);
            Language.Add("fr", PM_1_3_4);
            Language_Changed(Thread.CurrentThread.CurrentUICulture.Parent.ToString());
        }

        private void Language_Changed(string s)
        {
            Title = Leng.Title;
            PM_1.Header = Leng.PM_1;
            PM_2.Header = Leng.PM_2;
            PM_3.Header = Leng.PM_3;
            PM_1_1.Header = Leng.PM_1_1;
            PM_1_2.Header = Leng.PM_1_2;
            PM_1_3.Header = Leng.PM_1_3;
            PM_1_4.Header = Leng.PM_1_4;
            PM_2_1.Header = Leng.PM_2_1;
            PM_2_2.Header = Leng.PM_2_2;
            PM_2_3.Header = Leng.PM_2_3;
            PM_3_1.Header = Leng.PM_3_1;
            PM_3_1_1.Header = Leng.PM_3_1_1;
            PM_3_1_2.Header = Leng.PM_3_1_2;
            PM_3_1_3.Header = Leng.PM_3_1_3;
            PM_1_3_1.Header = Leng.PM_1_3_1;
            PM_1_3_2.Header = Leng.PM_1_3_2;
            PM_1_3_3.Header = Leng.PM_1_3_3;
            PM_1_3_4.Header = Leng.PM_1_3_4;
            Column.Content = Leng.Column;
            Line.Content = Leng.Line;
            foreach (var el in Language)
                el.Value.IsEnabled = (el.Key != s) ? true : false;
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

            currentColumn.Text = $"{column + 1}";
            currentLine.Text = $"{row + 1}";
        }

        private void PM_1_3_Click(object sender, RoutedEventArgs e)
        {
            string s = "";
            foreach (var el in Language)
                if ((MenuItem)sender == el.Value)
                    s = el.Key;
            if (s.Length == 0)
                return;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(s);
            Language_Changed(s);
        }
    }
}
