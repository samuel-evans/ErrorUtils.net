using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace ErrorUtils
{
    public class InlineProperty
    {
        public static IEnumerable<Inline> GetInlines(DependencyObject dependencyObject)
        {
            return (IEnumerable<Inline>)dependencyObject.GetValue(InlinesProperty);
        }

        public static void SetInlines(DependencyObject dependencyObject, IEnumerable<Inline> value)
        {
            dependencyObject.SetValue(InlinesProperty, value);
        }

        public static DependencyProperty InlinesProperty =
            DependencyProperty.RegisterAttached("Inlines", typeof(IEnumerable<Inline>), typeof(InlineProperty), new FrameworkPropertyMetadata(OnInlinesPropertyChanged));

        private static void OnInlinesPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (!(dependencyObject is TextBlock textBlock))
                return;
            textBlock.Inlines.Clear();
            if (!(eventArgs.NewValue is IEnumerable<Inline> newInlines))
                return;
            textBlock.Inlines.AddRange(newInlines);
        }


    }
}
