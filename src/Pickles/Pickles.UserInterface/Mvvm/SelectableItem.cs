//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SelectableItem.cs" company="PicklesDoc">
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

using GalaSoft.MvvmLight;

namespace PicklesDoc.Pickles.UserInterface.Mvvm
{
    public class SelectableItem<T> : ObservableObject
    {
        private bool isSelected;

        public SelectableItem(T item, bool isSelected)
        {
            this.Item = item;
            this.isSelected = isSelected;
        }

        public SelectableItem(T item)
            : this(item, false)
        {
        }

        public T Item { get; }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                this.isSelected = value;
                this.RaisePropertyChanged(() => this.IsSelected);
            }
        }
    }
}
