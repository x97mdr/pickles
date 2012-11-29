using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace PicklesDoc.Pickles.UserInterface.Mvvm
{
    /// <summary>
    /// An implementation of <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <typeparam name="TValue">The type of the class that is raising the event.</typeparam>
        /// <param name="selector">Contains the propery expression of the propery that changed.</param>
        protected void RaisePropertyChanged<TValue>(Expression<Func<TValue>> selector)
        {
            this.PropertyChanged.Raise(this, selector);
        }
    }
}