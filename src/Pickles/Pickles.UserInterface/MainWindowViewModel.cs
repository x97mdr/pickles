using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Ninject;
using Pickles.Parser;
using System.Globalization;
using System.ComponentModel;

namespace Pickles.UserInterface
{
    public class MainWindowViewModel : NotifyPropertyChanged, IDataErrorInfo
    {
      private readonly MultiSelectableCollection<DocumentationFormat> documentationFormats;
        private readonly SelectableCollection<TestResultsFormat> testResultsFormats;
        private string picklesVersion = typeof(Feature).Assembly.GetName().Version.ToString();
        private string featureFolder;
        private string outputFolder;
        private string projectName;
        private string projectVersion;
        private string testResultsFile;
        private CultureInfo selectedLanguage = CultureInfo.CurrentUICulture;

      private bool includeTests;

      private readonly RelayCommand browseForFeatureFolderCommand;

      private readonly RelayCommand browseForOutputFolderCommand;

      private readonly RelayCommand browseForTestResultsFileCommand;

      private readonly RelayCommand generateCommand;

      private bool isRunning;

      public MainWindowViewModel()
        {
          documentationFormats = new MultiSelectableCollection<DocumentationFormat>(Enum.GetValues(typeof(DocumentationFormat)).Cast<DocumentationFormat>());
            documentationFormats.First().IsSelected = true;

            testResultsFormats = new SelectableCollection<TestResultsFormat>(Enum.GetValues(typeof(TestResultsFormat)).Cast<TestResultsFormat>());
            documentationFormats.First().IsSelected = true;

            browseForFeatureFolderCommand = new RelayCommand(DoBrowseForFeature);

        browseForOutputFolderCommand = new RelayCommand(DoBrowseForOutputFolder);
        browseForTestResultsFileCommand = new RelayCommand(DoBrowseForTestResultsFile);
        generateCommand = new RelayCommand(DoGenerate, CanGenerate);
        }

        public string PicklesVersion
        {
            get { return picklesVersion; }
            set { picklesVersion = value; RaisePropertyChanged(() => PicklesVersion); }
        }

        public string FeatureFolder
        {
            get { return featureFolder; }
            set { featureFolder = value; RaisePropertyChanged(() => FeatureFolder); }
        }

        public string OutputFolder
        {
            get { return outputFolder; }
            set { outputFolder = value; RaisePropertyChanged(() => OutputFolder); }
        }

        public MultiSelectableCollection<DocumentationFormat> DocumentationFormatValues
        {
            get { return documentationFormats; }
        }

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; RaisePropertyChanged(() => ProjectName); }
        }

        public string ProjectVersion
        {
            get { return projectVersion; }
            set { projectVersion = value; RaisePropertyChanged(() => ProjectVersion); }
        }

        public string TestResultsFile
        {
            get { return testResultsFile; }
            set { testResultsFile = value; RaisePropertyChanged(() => TestResultsFile); }
        }

        public SelectableCollection<TestResultsFormat> TestResultsFormatValues
        {
            get { return testResultsFormats; }
        }

        public CultureInfo SelectedLanguage
        {
            get { return selectedLanguage; }
            set { selectedLanguage = value; RaisePropertyChanged(() => SelectedLanguage); }
        }

        public IEnumerable<CultureInfo> LanguageValues
        {
            get { return CultureInfo.GetCultures(CultureTypes.NeutralCultures); }
        }

      private bool CanGenerate()
        {
            return !isRunning;
        }

      public bool IncludeTests
      {
        get { return includeTests; }
        set { includeTests = value; RaisePropertyChanged(() => IncludeTests); }
      }

      #region Commands

        public ICommand GeneratePickles
        {
            get
            {
                return generateCommand;
            }
        }

      private void DoGenerate()
      {
        IsRunning = true;
        this.generateCommand.RaiseCanExecuteChanged();

        var backgroundWorker = new BackgroundWorker();

        backgroundWorker.DoWork += (sender, args) => DoWork();
        backgroundWorker.RunWorkerCompleted += (sender, args) =>
                                                 {
                                                   this.IsRunning = false;
                                                   this.generateCommand.RaiseCanExecuteChanged();
                                                 };
        backgroundWorker.RunWorkerAsync();
      }

      private void DoWork()
      {
        var kernel = new StandardKernel(new PicklesModule());
        var configuration = kernel.Get<Configuration>();

        configuration.FeatureFolder = new DirectoryInfo(featureFolder);
        configuration.OutputFolder = new DirectoryInfo(outputFolder);
        configuration.SystemUnderTestName = projectName;
        configuration.SystemUnderTestVersion = projectVersion;
        configuration.TestResultsFile = this.IncludeTests ? new FileInfo(testResultsFile) : null;
        configuration.TestResultsFormat = this.IncludeTests ? testResultsFormats.Selected : default(TestResultsFormat);
        configuration.Language = selectedLanguage != null ? selectedLanguage.TwoLetterISOLanguageName : CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        foreach (DocumentationFormat documentationFormat in documentationFormats.Selected)
        {
          configuration.DocumentationFormat = documentationFormat;
          var runner = kernel.Get<Runner>();
          runner.Run(kernel);
        }
      }

      public ICommand BrowseForFeatureFolder
        {
            get
            {
              return browseForFeatureFolderCommand;
            }
        }

      public ICommand BrowseForOutputFolder
        {
            get
            {
                return browseForOutputFolderCommand;
            }
        }

      public ICommand BrowseForTestResultsFile
        {
            get
            {
                return browseForTestResultsFileCommand;
            }
        }

      private void DoBrowseForTestResultsFile()
      {
        var dlg = new Ookii.Dialogs.Wpf.VistaOpenFileDialog();
        var result = dlg.ShowDialog();
        if (result == true) OutputFolder = dlg.FileName;
      }

      private void DoBrowseForFeature()
      {
        var dlg = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
        var result = dlg.ShowDialog();
        if (result == true) FeatureFolder = dlg.SelectedPath;
      }

      private void DoBrowseForOutputFolder()
      {
        var dlg = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
        var result = dlg.ShowDialog();
        if (result == true) OutputFolder = dlg.SelectedPath;
      }

      #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

      public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

      public bool IsRunning
      {
        get
        {
          
          return isRunning;
        }
        set { isRunning = value; this.RaisePropertyChanged(() => this.IsRunning); }
      }
    }
}
