using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Ninject;
using Pickles.Parser;
using System.Globalization;
using System.ComponentModel;

namespace Pickles.UserInterface
{
  public class MainWindowViewModel : NotifyPropertyChanged
  {
    private readonly MultiSelectableCollection<DocumentationFormat> documentationFormats;
    private readonly SelectableCollection<TestResultsFormat> testResultsFormats;
    private readonly RelayCommand browseForFeatureFolderCommand;
    private readonly RelayCommand browseForOutputFolderCommand;
    private readonly RelayCommand browseForTestResultsFileCommand;
    private readonly RelayCommand generateCommand;

    private readonly RelayCommand openOutputDirectory;

    private string picklesVersion = typeof(Feature).Assembly.GetName().Version.ToString();
    private string featureFolder;
    private string outputFolder;
    private string projectName;
    private string projectVersion;
    private string testResultsFile;
    private CultureInfo selectedLanguage;
    private bool includeTests;
    private bool isRunning;

    private bool isFeatureDirectoryValid;

    private bool isOutputDirectoryValid;

    private bool isProjectNameValid;

    private bool isProjectVersionValid;

    private bool isTestResultsFileValid;

    private bool isTestResultsFormatValid;

    private bool isLanguageValid = true;

    private readonly CultureInfo[] neutralCultures;

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
      openOutputDirectory = new RelayCommand(DoOpenOutputDirectory, CanOpenOutputDirectory);

      this.PropertyChanged += MainWindowViewModel_PropertyChanged;
      neutralCultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures);
      selectedLanguage = CultureInfo.GetCultureInfo("en");
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
      get
      {
        return neutralCultures;
      }
    }

    public bool IncludeTests
    {
      get { return includeTests; }
      set { includeTests = value; RaisePropertyChanged(() => IncludeTests); }
    }

    public ICommand GeneratePickles
    {
      get
      {
        return generateCommand;
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

    public RelayCommand OpenOutputDirectory
    {
      get { return openOutputDirectory; }
    }

    public bool IsRunning
    {
      get
      {

        return isRunning;
      }
      set { isRunning = value; this.RaisePropertyChanged(() => this.IsRunning); }
    }

    public bool IsFeatureDirectoryValid
    {
      get { return isFeatureDirectoryValid; }
      set { isFeatureDirectoryValid = value; this.RaisePropertyChanged(() => this.IsFeatureDirectoryValid); }
    }

    public bool IsOutputDirectoryValid
    {
      get { return isOutputDirectoryValid; }
      set { isOutputDirectoryValid = value; this.RaisePropertyChanged(() => this.IsOutputDirectoryValid); }
    }

    public bool IsProjectNameValid
    {
      get { return isProjectNameValid; }
      set { isProjectNameValid = value; this.RaisePropertyChanged(() => this.IsProjectNameValid); }
    }

    public bool IsProjectVersionValid
    {
      get { return isProjectVersionValid; }
      set { isProjectVersionValid = value; this.RaisePropertyChanged(() => this.IsProjectVersionValid); }
    }

    public bool IsTestResultsFileValid
    {
      get { return isTestResultsFileValid; }
      set { isTestResultsFileValid = value; this.RaisePropertyChanged(() => this.IsTestResultsFileValid); }
    }

    public bool IsTestResultsFormatValid
    {
      get { return isTestResultsFormatValid; }
      set { isTestResultsFormatValid = value; this.RaisePropertyChanged(() => this.IsTestResultsFormatValid); }
    }

    public bool IsLanguageValid
    {
      get { return isLanguageValid; }
      set { isLanguageValid = value; this.RaisePropertyChanged(() => this.IsLanguageValid); }
    }

    public void LoadFromSettings()
    {
      string tempFeatureFolder = Properties.Settings.Default.FeatureFolder;
      string tempOutputFolder = Properties.Settings.Default.OutputFolder;
      string tempTestResultsFile = Properties.Settings.Default.TestResultsFile;

      this.FeatureFolder = tempFeatureFolder;
      this.OutputFolder = tempOutputFolder;
      this.TestResultsFile = tempTestResultsFile;
    }

    void MainWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case "FeatureFolder":
          {
            if (Directory.Exists(this.featureFolder))
            {
              Properties.Settings.Default.FeatureFolder = this.featureFolder;
              Properties.Settings.Default.Save();

              this.IsFeatureDirectoryValid = true;
            }
            else
            {
              this.IsFeatureDirectoryValid = false;
            }

            break;
          }

        case "OutputFolder":
          {
            if (Directory.Exists(this.outputFolder))
            {
              Properties.Settings.Default.OutputFolder = this.outputFolder;
              Properties.Settings.Default.Save();

              this.IsOutputDirectoryValid = true;
            }
            else
            {
              this.IsOutputDirectoryValid = false;
            }

            this.openOutputDirectory.RaiseCanExecuteChanged();

            break;
          }

        case "TestResultsFile":
          {
            if (File.Exists(this.testResultsFile))
            {
              Properties.Settings.Default.TestResultsFile = this.testResultsFile;
              Properties.Settings.Default.Save();

              this.IsTestResultsFileValid = true;
            }
            else
            {
              this.IsTestResultsFileValid = false;
            }

            break;
          }

        case "ProjectName":
          {
            this.IsProjectNameValid = !string.IsNullOrWhiteSpace(this.projectName);
            break;
          }

        case "ProjectVersion":
          {
            this.IsProjectVersionValid = !string.IsNullOrWhiteSpace(this.projectVersion);
            break;
          }

        case "IsRunning":
        case "IsFeatureDirectoryValid":
        case "IsOutputDirectoryValid":
        case "IsProjectNameValid":
        case "isProjectVersionValid":
        case "IsTestResultsFileValid":
        case "IsTestResultsFormatValid":
        case "IsLanguageValid":
        case "IncludeTests":
          {
            this.generateCommand.RaiseCanExecuteChanged();
            break;
          }
      }
    }

    private bool CanGenerate()
    {
      return !isRunning
        && isFeatureDirectoryValid
        && isOutputDirectoryValid
        && isProjectNameValid
        && isProjectVersionValid
        && (isTestResultsFileValid || !includeTests)
        && (isTestResultsFormatValid || !includeTests)
        && isLanguageValid;
    }

    private void DoGenerate()
    {
      IsRunning = true;

      var backgroundWorker = new BackgroundWorker();

      backgroundWorker.DoWork += (sender, args) => DoWork();
      backgroundWorker.RunWorkerCompleted += (sender, args) =>
                                               {
                                                 this.IsRunning = false;
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

    private void DoBrowseForTestResultsFile()
    {
      var dlg = new Ookii.Dialogs.Wpf.VistaOpenFileDialog();
      var result = dlg.ShowDialog();
      if (result == true) TestResultsFile = dlg.FileName;
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

    private void DoOpenOutputDirectory()
    {
      Process.Start(this.outputFolder);
    }

    private bool CanOpenOutputDirectory()
    {
      return this.isOutputDirectoryValid;
    }
  }
}
