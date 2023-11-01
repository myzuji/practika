using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace office
{
    internal class TemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;

            if (item is Wall)
                return element.FindResource("WallTemplate") as DataTemplate;

            if (item is Cabinet)
                return element.FindResource("CabinetTemplate") as DataTemplate;

            return null;
        }
    }
}
