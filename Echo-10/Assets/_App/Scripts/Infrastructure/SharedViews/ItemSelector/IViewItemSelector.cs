using System;
using System.Collections.Generic;

namespace _App.Scripts.Infrastructure.SharedViews.ItemSelector
{
    public interface IViewItemSelector<T>
    {
        event Action<T> OnItemSelected;
        void UpdateItems(IEnumerable<T> items);
        void Clear();
    }
}