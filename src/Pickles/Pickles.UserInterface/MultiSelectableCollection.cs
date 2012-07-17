using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pickles.UserInterface
{
  public class MultiSelectableCollection<T> : ObservableCollection<SelectableItem<T>>
  {
    public MultiSelectableCollection()
    {
    }

    public MultiSelectableCollection(IEnumerable<T> items)
    {
      foreach (var item in items)
      {
        this.Add(new SelectableItem<T>(item));
      }
    }

    public IEnumerable<T> Selected { get { return this.Where(item => item.IsSelected).Select(item => item.Item); } }
  }
}