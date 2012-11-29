using System;
using System.Collections.Generic;
using System.Linq;

namespace PicklesDoc.Pickles.UserInterface.Mvvm
{
    public class SelectableCollection<T> : NotifySelectionChangedCollection<T>
    {
        public SelectableCollection()
        {
        }

        public SelectableCollection(IEnumerable<T> items)
            : base(items)
        {
        }

        /// <summary>
        /// Returns the first selected item, or the default value of <typeparamref name="T"/> if no item is selected.
        /// </summary>
        public T Selected
        {
            get
            {
                SelectableItem<T> selected = this.FirstOrDefault(it => it.IsSelected);
                return selected != null ? selected.Item : default(T);
            }
        }
    }
}