//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MainWindow.cs" company="PicklesDoc">
//  Copyright 2011 Jeffrey Cameron
//  Copyright 2012-present PicklesDoc team and community contributors
//
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Shell;

using PicklesDoc.Pickles.UserInterface.ViewModel;

namespace PicklesDoc.Pickles.UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private MainViewModel ViewModel
        {
          get { return this.DataContext as MainViewModel; }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel.LoadFromSettings();
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            this.ViewModel.SaveToSettings();
        }

        private void MainWindow_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
          var oldVm = e.NewValue as MainViewModel;

          if (oldVm != null)
          {
            oldVm.PropertyChanged -= this.ViewModelOnPropertyChanged;
          }

          var newVm = e.NewValue as MainViewModel;

          if (newVm != null)
          {
            newVm.PropertyChanged += this.ViewModelOnPropertyChanged;
          }
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
          switch (propertyChangedEventArgs.PropertyName)
          {
            case "IsRunning":
              {
                this.progressIndicator.Visibility = this.ViewModel.IsRunning ? Visibility.Visible : Visibility.Hidden;
                this.taskBarItemInfo.ProgressState = this.ViewModel.IsRunning ? TaskbarItemProgressState.Indeterminate : TaskbarItemProgressState.None;
                break;
              }
          }
        }
    }
}
