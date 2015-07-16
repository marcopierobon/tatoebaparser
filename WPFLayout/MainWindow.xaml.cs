using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TatoebaParser;
using TatoebaParser.Helpers;

namespace WPFLayout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SortedDictionary<string, string> langLangCodeDict = new SortedDictionary<string, string>();
        private BackgroundWorker _worker;
        private string _progress;

        public MainWindow()
        {
            InitializeComponent();
            SetListBoxLanguages();
            DeactivateComponents();
        }

        private void SetListBoxLanguages()
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                          .Except(CultureInfo.GetCultures(CultureTypes.SpecificCultures));
            foreach (var culture in cultures)
            {
                try
                {
                    if(culture.Name == CultureInfo.InvariantCulture.Name)
                        continue;
                    var langEnglishName = culture.EnglishName;
                    if (langEnglishName.Length > 15)
                        langEnglishName = langEnglishName.Substring(0, 14) + " ...";
                    langLangCodeDict.Add(langEnglishName, culture.ThreeLetterISOLanguageName);
                }
                catch
                {
                    continue;
                }
            }
            SourceLangListBox.ItemsSource = langLangCodeDict.Keys;
            DestLangListBox.ItemsSource = langLangCodeDict.Keys;
        }

        private void DeactivateComponents()
        {
            linkFileButton.IsEnabled = false;
            SourceLangListBox.IsEnabled = false;
            DestLangListBox.IsEnabled = false;
            generateOutputButton.IsEnabled = false;
        }

        private void sentenceFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            var dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv";


            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                // Open document 
                string filename = dlg.FileName;
                sentenceFileLabel.Content = filename;
                linkFileButton.IsEnabled = true;
            }
        }

        private void linkFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            var dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv";


            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                // Open document 
                string filename = dlg.FileName;
                linkFileLabel.Content = filename;
                SourceLangListBox.IsEnabled = true;
            }
        }

        private void SourceLangListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lbi = e.Source as ListBox;

            if (lbi != null && lbi.SelectedItem != null)
            {
                DestLangListBox.IsEnabled = true;
            }
        }
        private async void  generateOutputButton_Click(object sender, RoutedEventArgs e)
        {
            var sourceLang = langLangCodeDict.FirstOrDefault(x => x.Key.Equals(SourceLangListBox.SelectedItem.ToString())).Value; var destLang = langLangCodeDict.FirstOrDefault(x => x.Key.Equals(DestLangListBox.SelectedItem.ToString())).Value;

            var tatoebaParser = new TatoebaParserClass(sentenceFileLabel.Content.ToString(), linkFileLabel.Content.ToString(), sourceLang, destLang);
            Task<int> task =  tatoebaParser.Run();

            await task;
        }

        private void DestLangListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lbi = e.Source as ListBox;

            if (lbi != null && lbi.SelectedItem != null)
            {
                generateOutputButton.IsEnabled = true;
            }
        }
    }
}
