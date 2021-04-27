using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AttachedPropertyTest
{
    public class ClickItemToUserModelConverter : IEventArgsConverter
    {
        public object Convert(object value, object parameter)
        {
            var e = value as MouseEventArgs;
            var s = parameter as ListBox;
            //Debug.WriteLine($"-------------------------");
            foreach (var item in s.Items)
            {
                var element = s.ItemContainerGenerator.ContainerFromItem(item);
                if (element == null)
                    continue;

                Rect bound =  VisualTreeHelper.GetDescendantBounds(element as ListViewItem);
                Point pos = e.GetPosition((IInputElement)element);
                //Debug.WriteLine($"{((UserModel)item).Name} Position x:{pos.X} y:{pos.Y} / bound : {bound.Width},{bound.Height}");
                if (bound.Contains(pos))
                    return item;
            }
            return null;
        }
    }
}
