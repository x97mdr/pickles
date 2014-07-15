using System;

using GalaSoft.MvvmLight;

namespace PicklesDoc.Pickles.UserInterface.Mvvm
{
  public class SelectableItem<T> : ObservableObject
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
      get { return this.item; }
    }

    public bool IsSelected
    {
      get { return this.isSelected; }
      set { this.isSelected = value; this.RaisePropertyChanged(() => this.IsSelected);
      }
    }
  }
}