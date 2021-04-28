using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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
            if (!(d is ListBox)) return;

            ListBox associatedObject = d as ListBox;
            NotifyCollectionChangedEventHandler handler = (sender, args) =>
            {
                if (associatedObject != null)
                {
                    if (_isUpdatingFromSource)
                        return;

                    try
                    {
                        _isUpdatingFromTarget = true;
                        IList selectedItems = associatedObject.SelectedItems;
                        if (args.Action == NotifyCollectionChangedAction.Reset)
                        {
                            selectedItems.Clear();
                            return;
                        }
                        if (args.OldItems != null)
                        {
                            foreach (var item in args.OldItems)
                            {
                                if (selectedItems.Contains(item))
                                {
                                    selectedItems.Remove(item);
                                }
                            }
                        }

                        if (args.NewItems != null)
                        {
                            foreach (var item in args.NewItems)
                            {
                                if (!selectedItems.Contains(item))
                                {
                                    selectedItems.Add(item);
                                }
                            }
                        }
                    }
                    finally
                    {
                        _isUpdatingFromTarget = false;
                    }
                }
            };

            if (associatedObject !=null && e.OldValue == null)
            {
                //초기 Attached상태를 위한듯...
                associatedObject.SelectionChanged += CollectionChangedFromSource; 
            }

            if(e.OldValue is INotifyCollectionChanged)
                (e.OldValue as INotifyCollectionChanged).CollectionChanged -= handler;
            if (e.NewValue is INotifyCollectionChanged)
                (e.NewValue as INotifyCollectionChanged).CollectionChanged += handler; ;
        }

        private static bool _isUpdatingFromSource;  // View(Source)에서 업데이트
        private static bool _isUpdatingFromTarget;  // VIewModel(Target)에서 업데이트

        private static void CollectionChangedFromTarget(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_isUpdatingFromSource)
                return;

            _isUpdatingFromTarget = true;
            try
            {
                if (sender is ListBox)
                {
                    var associatedObject = sender as ListBox;
                    var selectedItems = associatedObject.SelectedItems;

                    if (e.Action == NotifyCollectionChangedAction.Reset)
                    {
                        selectedItems.Clear();
                        return;
                    }
                    foreach (var item in e.OldItems)
                    {
                        selectedItems.Remove(item);
                    }
                    foreach (var item in e.NewItems)
                    {
                        selectedItems.Remove(item);
                    }
                }
            }
            finally
            {
                _isUpdatingFromTarget = false;
            }
        }
        private static void CollectionChangedFromSource(object sender, SelectionChangedEventArgs e)
        {
            if (_isUpdatingFromTarget)
                return;

            _isUpdatingFromSource = true;
            try
            {
                DependencyObject d = sender as DependencyObject;
                if (sender is ListBox)
                {
                    var associatedObject = sender as ListBox;
                    var selectedItems = associatedObject.SelectedItems;

                    foreach (var item in e.RemovedItems)
                    {
                        selectedItems.Remove(item);
                    }
                    foreach (var item in e.AddedItems)
                    {
                        selectedItems.Add(item);
                    }
                }
            }
            finally
            {
                _isUpdatingFromSource = false;
            }
        }
    }
}
