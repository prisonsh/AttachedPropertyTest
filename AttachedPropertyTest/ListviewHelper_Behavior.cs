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
using System.Windows.Interactivity;

namespace AttachedPropertyTest
{
    public class ListviewHelper_Behavior : Behavior<ListBox>
    {
        public static readonly DependencyProperty SelectedItemsProperty
            = DependencyProperty.Register("SelectedItems",
                                                                  typeof(IList),
                                                                  typeof(ListviewHelper_Behavior),
                                                                  new UIPropertyMetadata(null, SelectedItemsChanged));

        protected override void OnAttached()
        {
            base.OnAttached();
            if (SelectedItems != null)
            {
                AssociatedObject.SelectedItems.Clear();
                AssociatedObject.SelectionChanged += CollectionChangedFromSource;
            }
            foreach (var item in SelectedItems)
            {
                AssociatedObject.SelectedItems.Add(item);
            }
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (this.AssociatedObject != null)
                AssociatedObject.SelectionChanged -= CollectionChangedFromSource;
        }

        public IList SelectedItems
        {
            get => (IList)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }
        public static void SelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as ListviewHelper_Behavior;

            if (e.OldValue is INotifyCollectionChanged)
                (e.OldValue as INotifyCollectionChanged).CollectionChanged -= behavior.CollectionChangedFromTarget;
            if (e.NewValue is INotifyCollectionChanged)
                (e.NewValue as INotifyCollectionChanged).CollectionChanged += behavior.CollectionChangedFromTarget;
        }
        private static bool _isUpdatingFromSource;  // View(Source)에서 업데이트
        private static bool _isUpdatingFromTarget;  // VIewModel(Target)에서 업데이트

        private void CollectionChangedFromTarget(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_isUpdatingFromSource)
                return;

            _isUpdatingFromTarget = true;
            try
            {
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    AssociatedObject.SelectedItems.Clear();
                    return;
                }
                foreach (var item in e.OldItems)
                {
                    AssociatedObject.SelectedItems.Remove(item);
                }
                foreach (var item in e.NewItems)
                {
                    AssociatedObject.SelectedItems.Remove(item);
                }
            }
            finally
            {
                _isUpdatingFromTarget = false;
            }
        }
        private void CollectionChangedFromSource(object sender, SelectionChangedEventArgs e)
        {
            if (_isUpdatingFromTarget)
                return;

            _isUpdatingFromSource = true;
            try
            {
                foreach (var item in e.RemovedItems)
                {
                    AssociatedObject.SelectedItems.Remove(item);
                }
                foreach (var item in e.AddedItems)
                {
                    AssociatedObject.SelectedItems.Add(item);
                }
            }
            finally
            {
                _isUpdatingFromSource = false;
            }
        }
    }

    public class ListviewHelper_Behavior2 : Behavior<ListBox>
    {
        public static readonly DependencyProperty SelectedItemsProperty
            = DependencyProperty.Register("SelectedItems",
                                                                  typeof(INotifyCollectionChanged),
                                                                  typeof(ListviewHelper_Behavior2),
                                                                  new UIPropertyMetadata(null, SelectedItemsChanged));

        protected override void OnAttached()
        {
            base.OnAttached();
            if (SelectedItems != null)
            {
                AssociatedObject.SelectedItems.Clear();
                AssociatedObject.SelectionChanged += CollectionChangedFromSource;
            }
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (this.AssociatedObject != null)
                AssociatedObject.SelectionChanged -= CollectionChangedFromSource;
        }

        public INotifyCollectionChanged SelectedItems
        {
            get => (INotifyCollectionChanged)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }
        public static void SelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as ListviewHelper_Behavior2;

            if (e.OldValue is INotifyCollectionChanged)
                (e.OldValue as INotifyCollectionChanged).CollectionChanged -= behavior.CollectionChangedFromTarget;
            if (e.NewValue is INotifyCollectionChanged)
                (e.NewValue as INotifyCollectionChanged).CollectionChanged += behavior.CollectionChangedFromTarget;
        }
        private static bool _isUpdatingFromSource;  // View(Source)에서 업데이트
        private static bool _isUpdatingFromTarget;  // VIewModel(Target)에서 업데이트

        private void CollectionChangedFromTarget(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_isUpdatingFromSource)
                return;

            _isUpdatingFromTarget = true;
            try
            {
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    AssociatedObject.SelectedItems.Clear();
                    return;
                }
                foreach (var item in e.OldItems)
                {
                    AssociatedObject.SelectedItems.Remove(item);
                }
                foreach (var item in e.NewItems)
                {
                    AssociatedObject.SelectedItems.Remove(item);
                }
            }
            finally
            {
                _isUpdatingFromTarget = false;
            }
        }
        private void CollectionChangedFromSource(object sender, SelectionChangedEventArgs e)
        {
            if (_isUpdatingFromTarget)
                return;

            _isUpdatingFromSource = true;
            try
            {
                foreach (var item in e.RemovedItems)
                {
                    AssociatedObject.SelectedItems.Remove(item);
                }
                foreach (var item in e.AddedItems)
                {
                    AssociatedObject.SelectedItems.Add(item);
                }
            }
            finally
            {
                _isUpdatingFromSource = false;
            }
        }
    }
}
