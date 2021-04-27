using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AttachedPropertyTest
{
    public class ListViewHelper_Attached
    {

        public static readonly DependencyProperty SelectedItemsProperty
            = DependencyProperty.RegisterAttached("SelectedItems",
                                                                  typeof(INotifyCollectionChanged),
                                                                  typeof(ListViewHelper_Attached),
                                                                  new  PropertyMetadata(null, SelectedItemsChanged));

        public static void SetSelectedItems(DependencyObject d, INotifyCollectionChanged value)
        {
            d.SetValue(SelectedItemsProperty, value);
        }
        public static INotifyCollectionChanged GetSelectedItems(DependencyObject d)
        {
            return (INotifyCollectionChanged)d.GetValue(SelectedItemsProperty);
        }

        public static void SelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = d as ListBox;
        }
    }
}
