using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pickles.UserInterface
{
  public class SelectableCollection<T> : ObservableCollection<SelectableItem<T>>
  {
    public SelectableCollection()
    {
    }

    public SelectableCollection(IEnumerable<T> items)
    {
      foreach (var item in items)
      {
        this.Add(new SelectableItem<T>(item));
      }
    }

    public T Selected { get { return this.Single(item => item.IsSelected).Item; } }
  }
}