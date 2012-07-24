using System;
using System.Collections.Generic;
using System.Linq;

namespace Pickles.UserInterface.Mvvm
{
  public class MultiSelectableCollection<T> : NotifySelectionChangedCollection<T>
  {
    public MultiSelectableCollection()
    {
    }

    public MultiSelectableCollection(IEnumerable<T> items)
        : base(items)
    {
    }

    public IEnumerable<T> Selected { get { return this.Where(item => item.IsSelected).Select(item => item.Item); } }
  }
}