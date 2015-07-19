using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TatoebaParser;

namespace WPFLayout.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly SortedDictionary<string, string> _langLangCodeDict = new SortedDictionary<string, string>();
        private string _sentenceFilePath;
        private string _linkFilePath;
        private string _outputFilePath;

        public MainWindow()
        {
            Init();
        }

        public void Init()
        {
            InitializeComponent();
            SetListBoxLanguages();
            DeactivateComponents();
            SetBindings();
        }

        private void SetBindings()
        {
            //Bindings for the Nested MenuItem's New and Exit
            var nBinding = new CommandBinding { Command = ApplicationCommands.New };
            nBinding.Executed += DoNew_Executed;
            nBinding.CanExecute += DoNew_CanExecute;
            this.CommandBindings.Add(nBinding);
            var cBinding = new CommandBinding { Command = ApplicationCommands.Close };
            cBinding.Executed += DoClose_Executed;
            cBinding.CanExecute += DoClose_CanExecute;
            this.CommandBindings.Add(cBinding);

            //Bindings for the Nested MenuItem's Help and About
            var hBinding = new CommandBinding { Command = ApplicationCommands.Help };
            hBinding.Executed += DoHelp_Executed;
            hBinding.CanExecute += DoHelp_CanExecute;
            this.CommandBindings.Add(hBinding);
            var aBinding = new CommandBinding { Command = ApplicationCommands.Properties };
            aBinding.Executed += DoAbout_Executed;
            aBinding.CanExecute += DoAbout_CanExecute;
            this.CommandBindings.Add(aBinding);
        }

        private void DoAbout_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void DoHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var helpWindow = new HelpWindow();
            helpWindow.Show();
        }

        private void DoAbout_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DoHelp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DoClose_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DoClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        //Event handlers for the Nested MenuItem's New and Open.
        public void DoNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _sentenceFilePath = null;
            _linkFilePath = null;
            _outputFilePath = null;
            SentenceFileLabel.Content = "";
            LinkFileLabel.Content = "";
            OutputFileNameLabel.Content = "";
            Init();
        }
        public void DoNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SetListBoxLanguages()
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                          .Except(CultureInfo.GetCultures(CultureTypes.SpecificCultures));
            foreach (var culture in cultures)
            {
                try
                {
                    if (culture.Name == CultureInfo.InvariantCulture.Name)
                        continue;
                    var langEnglishName = culture.EnglishName;
                    if (langEnglishName.Length > 15)
                        langEnglishName = langEnglishName.Substring(0, 14) + " ...";
                    _langLangCodeDict.Add(langEnglishName, culture.ThreeLetterISOLanguageName);
                }
                catch
                {
                    continue;
                }
            }
            SourceLangListBox.ItemsSource = _langLangCodeDict.Keys;
            DestLangListBox.ItemsSource = _langLangCodeDict.Keys;
        }

        private void DeactivateComponents()
        {
            LinkFileButton.IsEnabled = false;
            SourceLangListBox.IsEnabled = false;
            DestLangListBox.IsEnabled = false;
            SaveAsButton.IsEnabled = false;
            GenerateOutputButton.IsEnabled = false;
            DoNotAllowDuplicateRadio.IsEnabled = false;
            AllowDuplicateRadio.IsEnabled = false;
            OnSameLineRadio.IsEnabled = false;
            OnDifferentLinesRadio.IsEnabled = false;
        }

        private void sentenceFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog and set filter for file extension and default file extension 
            var dlg = new Microsoft.Win32.OpenFileDialog { DefaultExt = ".csv", Filter = "CSV Files (*.csv)|*.csv" };
            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result != true) return;
            // Open document 
            var filename = dlg.FileName;
            if (!TatoebaParser.Helpers.ReadHelpers.IsSentenceFileCorrect(filename))
            {
                MessageBox.Show("The sentence file cannot be accepted cause it has an invalid number of columns", "Invalid file", new MessageBoxButton(), MessageBoxImage.Error);
                return;
            }
            if (filename.Length > 20)
            {
                SentenceFileLabel.Content = filename.Substring(0, 16) + "..." + filename.Substring((filename.Length - 16), 16);
            }
            else
            {
                SentenceFileLabel.Content = filename;
            }
            SentenceFileLabel.ToolTip = filename;
            LinkFileButton.IsEnabled = true;
            _sentenceFilePath = filename;
        }

        private void linkFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog and set filter for file extension and default file extension 
            var dlg = new Microsoft.Win32.OpenFileDialog { DefaultExt = ".csv", Filter = "CSV Files (*.csv)|*.csv" };
            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();
            // Get the selected file name and display in a TextBox 
            if (result != true) return;
            // Open document 
            var filename = dlg.FileName;
            if (!TatoebaParser.Helpers.ReadHelpers.IsLinkFileCorrect(filename))
            {
                MessageBox.Show("The link file cannot be accepted cause it has an invalid number of columns", "Invalid file", new MessageBoxButton(), MessageBoxImage.Error);
                return;
            }
            if (filename.Length > 20)
            {
                LinkFileLabel.Content = filename.Substring(0, 16) + "..." + filename.Substring((filename.Length - 16), 16);
            }
            else
            {
                LinkFileLabel.Content = filename;
            }
            LinkFileLabel.ToolTip = filename;
            SourceLangListBox.IsEnabled = true;
            _linkFilePath = filename;
        }

        private void SourceLangListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lbi = e.Source as ListBox;

            if (lbi != null && lbi.SelectedItem != null)
            {
                DestLangListBox.IsEnabled = true;
            }
        }
        private async void generateOutputButton_Click(object sender, RoutedEventArgs e)
        {
            var sourceLang = _langLangCodeDict.FirstOrDefault(x => x.Key.Equals(SourceLangListBox.SelectedItem.ToString())).Value; var destLang = _langLangCodeDict.FirstOrDefault(x => x.Key.Equals(DestLangListBox.SelectedItem.ToString())).Value;
            var duplicatesEnabled = AllowDuplicateRadio.IsChecked != null && (AllowDuplicateRadio.IsChecked.Value ? true :
                false);
            var sameSourceSameLine = OnSameLineRadio.IsChecked != null && (OnSameLineRadio.IsChecked.Value ? true :
                false);
            var tatoebaParser = new TatoebaParserClass(_sentenceFilePath, _linkFilePath, _outputFilePath, sourceLang, destLang, duplicatesEnabled, sameSourceSameLine);
            LoadingGifImage.Visibility = Visibility.Visible;
            GenerateOutputButton.IsEnabled = false;

            var task = tatoebaParser.Run(ProcessCompleteCallback);
            await task;
        }

        private void DestLangListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lbi = e.Source as ListBox;

            if (lbi == null || lbi.SelectedItem == null) return;
            SaveAsButton.IsEnabled = true;
            DoNotAllowDuplicateRadio.IsEnabled = true;
            AllowDuplicateRadio.IsEnabled = true;
            OnSameLineRadio.IsEnabled = true;
            OnDifferentLinesRadio.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "SentencesWithTranslations",
                DefaultExt = ".tsv",
                Filter = "Tab separated values (.tsv)|*.tsv"
            };

            // Show save file dialog box
            var result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result != true) return;
            // Save document
            var filename = dlg.FileName;
            if (filename.Length > 20)
            {
                OutputFileNameLabel.Content = filename.Substring(0, 16) + "..." + filename.Substring((filename.Length - 16), 16);
            }
            else
            {
                OutputFileNameLabel.Content = filename;
            }
            OutputFileNameLabel.ToolTip = filename;
            GenerateOutputButton.IsEnabled = true;
            _outputFilePath = filename;
        }

        public void ProcessCompleteCallback()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                GenerateOutputButton.IsEnabled = true;
                LoadingGifImage.Visibility = Visibility.Hidden;
                CommandManager.InvalidateRequerySuggested();
            });
            MessageBox.Show("Process completed.", "Information", new MessageBoxButton(), MessageBoxImage.Information);
        }
    }
}
