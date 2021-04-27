using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace AttachedPropertyTest
{
    public class ListviewHelper_Behavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty SelectedItemsProperty
            = DependencyProperty.RegisterAttached("SelectedItems",
                                                                  typeof(INotifyCollectionChanged),
                                                                  typeof(ListviewHelper_Behavior),
                                                                  new PropertyMetadata(null, SelectedItemsChanged));

         
        public INotifyCollectionChanged SelectedItems
        {
            get => (INotifyCollectionChanged)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }
        public static void SelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
