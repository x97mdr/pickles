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
        private readonly SelectableCollection<DocumentationFormat> documentationFormats;
        private readonly SelectableCollection<TestResultsFormat> testResultsFormats;
        private string picklesVersion = typeof(Feature).Assembly.GetName().Version.ToString();
        private string featureFolder;
        private string outputFolder;
        private string projectName;
        private string projectVersion;
        private string testResultsFile;
        private CultureInfo selectedLanguage = CultureInfo.CurrentUICulture;

        public MainWindowViewModel()
        {
            this.documentationFormats = new SelectableCollection<DocumentationFormat>(Enum.GetValues(typeof(DocumentationFormat)).Cast<DocumentationFormat>());
            this.documentationFormats.First().IsSelected = true;

            this.testResultsFormats = new SelectableCollection<TestResultsFormat>(Enum.GetValues(typeof(TestResultsFormat)).Cast<TestResultsFormat>());
            this.documentationFormats.First().IsSelected = true;
        }

        public string PicklesVersion
        {
            get { return picklesVersion; }
            set { picklesVersion = value; this.RaisePropertyChanged(() => this.PicklesVersion); }
        }

        public string FeatureFolder
        {
            get { return featureFolder; }
            set { featureFolder = value; RaisePropertyChanged(() => this.FeatureFolder); }
        }

        public string OutputFolder
        {
            get { return outputFolder; }
            set { outputFolder = value; RaisePropertyChanged(() => this.OutputFolder); }
        }

        public SelectableCollection<DocumentationFormat> DocumentationFormatValues
        {
            get { return this.documentationFormats; }
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
            get { return this.testResultsFormats; }
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

        #region Commands

        public ICommand GeneratePickles
        {
            get
            {
                return new RelayCommand(() =>
                                            {
                                                var kernel = new StandardKernel(new PicklesModule());
                                                var configuration = kernel.Get<Configuration>();

                                                configuration.FeatureFolder = new DirectoryInfo(featureFolder);
                                                configuration.OutputFolder = new DirectoryInfo(outputFolder);
                                                configuration.DocumentationFormat = this.documentationFormats.Selected;
                                                configuration.SystemUnderTestName = projectName;
                                                configuration.SystemUnderTestVersion = projectVersion;
                                                configuration.TestResultsFile = testResultsFile != null ? new FileInfo(testResultsFile) : null;
                                                configuration.TestResultsFormat = this.testResultsFormats.Selected;
                                                configuration.Language = selectedLanguage != null ? selectedLanguage.TwoLetterISOLanguageName : CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

                                                var runner = kernel.Get<Runner>();
                                                runner.Run(kernel);
                                            });
            }
        }

        public ICommand BrowseForFeatureFolder
        {
            get
            {
                return new RelayCommand(() =>
                                            {
                                                var dlg = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
                                                var result = dlg.ShowDialog();
                                                if (result == true) FeatureFolder = dlg.SelectedPath;
                                            });
            }
        }

        public ICommand BrowseForOutputFolder
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var dlg = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
                    var result = dlg.ShowDialog();
                    if (result == true) OutputFolder = dlg.SelectedPath;
                });
            }
        }

        public ICommand BrowseForTestResultsFile
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var dlg = new Ookii.Dialogs.Wpf.VistaOpenFileDialog();
                    var result = dlg.ShowDialog();
                    if (result == true) OutputFolder = dlg.FileName;
                });
            }
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
    }
}
