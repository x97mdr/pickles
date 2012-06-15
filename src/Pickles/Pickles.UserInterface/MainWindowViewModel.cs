using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Ninject;
using Pickles.Parser;
using System.Globalization;

namespace Pickles.UserInterface
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string picklesVersion = typeof(Feature).Assembly.GetName().Version.ToString();
        private string featureFolder;
        private string outputFolder;
        private DocumentationFormat selectedDocumentationFormat;
        private string projectName;
        private string projectVersion;
        private TestResultsFormat selectedTestResultsFormat;
        private string testResultsFile;
        private CultureInfo selectedLanguage = CultureInfo.CurrentUICulture;

        public string PicklesVersion
        {
            get { return picklesVersion; }
            set { picklesVersion = value; RaisePropertyChanged("PicklesVersion"); }
        }

        public string FeatureFolder
        {
            get { return featureFolder; }
            set { featureFolder = value; RaisePropertyChanged("FeatureFolder"); }
        }

        public string OutputFolder
        {
            get { return outputFolder; }
            set { outputFolder = value; RaisePropertyChanged("OutputFolder"); }
        }

        public DocumentationFormat SelectedDocumentationFormat
        {
            get { return selectedDocumentationFormat; }
            set { selectedDocumentationFormat = value; RaisePropertyChanged("SelectedDocumentationFormat"); }
        }

        public IEnumerable<DocumentationFormat> DocumentationFormatValues
        {
            get { return Enum.GetValues(typeof (DocumentationFormat)).Cast<DocumentationFormat>();  }
        }

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; RaisePropertyChanged("ProjectName"); }
        }

        public string ProjectVersion
        {
            get { return projectVersion; }
            set { projectVersion = value; RaisePropertyChanged("ProjectVersion"); }
        }

        public string TestResultsFile
        {
            get { return testResultsFile; }
            set { testResultsFile = value; RaisePropertyChanged("TestResultsFile"); }
        }

        public TestResultsFormat SelectedTestResultsFormat
        {
            get { return selectedTestResultsFormat; }
            set { selectedTestResultsFormat = value; RaisePropertyChanged("SelectedTestResultsFormat"); }
        }

        public IEnumerable<TestResultsFormat> TestResultsFormatValues
        {
            get { return Enum.GetValues(typeof(TestResultsFormat)).Cast<TestResultsFormat>(); }
        }

        public CultureInfo SelectedLanguage
        {
            get { return selectedLanguage; }
            set { selectedLanguage = value; RaisePropertyChanged("SelectedLanguage"); }
        }

        public IEnumerable<CultureInfo> LanguageValues
        {
            get { return CultureInfo.GetCultures(CultureTypes.NeutralCultures); }
        }

        #region Methods

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

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
                                                configuration.DocumentationFormat = selectedDocumentationFormat;
                                                configuration.SystemUnderTestName = projectName;
                                                configuration.SystemUnderTestVersion = projectVersion;
                                                configuration.TestResultsFile = new FileInfo(testResultsFile);
                                                configuration.TestResultsFormat = selectedTestResultsFormat;
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
    }
}
