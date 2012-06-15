using System;

namespace Pickles.UserInterface
{
  public class SelectableItem<T> : NotifyPropertyChanged
  {
    private readonly T item;

    private bool isSelected;

    public SelectableItem(T item, bool isSelected)
    {
      this.item = item;
      this.isSelected = isSelected;
    }

    public SelectableItem(T item)
      : this(item, false)
    {
    }

    public T Item
    {
      get { return item; }
    }

    public bool IsSelected
    {
      get { return isSelected; }
      set { isSelected = value; this.RaisePropertyChanged(() => this.IsSelected);
      }
    }
  }
}