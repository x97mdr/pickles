using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Pickles.UserInterface.Mvvm
{
    public abstract class NotifySelectionChangedCollection<T> : ObservableCollection<SelectableItem<T>>
    {
        protected NotifySelectionChangedCollection()
        {
        }

        protected NotifySelectionChangedCollection(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                this.Add(new SelectableItem<T>(item));
            }
        }

        public event EventHandler SelectionChanged;

        protected override void InsertItem(int index, SelectableItem<T> item)
        {
            base.InsertItem(index, item);

            item.PropertyChanged += ItemOnPropertyChanged;
        }

        protected override void SetItem(int index, SelectableItem<T> item)
        {
            this[index].PropertyChanged -= ItemOnPropertyChanged;
            base.SetItem(index, item);
            item.PropertyChanged += ItemOnPropertyChanged;
        }

        protected override void RemoveItem(int index)
        {
            this[index].PropertyChanged -= ItemOnPropertyChanged;
            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            foreach (var item in Items)
            {
                item.PropertyChanged -= ItemOnPropertyChanged;
            }

            base.ClearItems();
        }

        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch (propertyChangedEventArgs.PropertyName)
            {
                case "IsSelected":
                    {
                        this.SelectionChanged.Raise(this, EventArgs.Empty);
                        break;
                    }
            }
        }
    }
}