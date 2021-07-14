using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace ErrorUtils
{
    internal class StringToInlineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string toConvert))
                return null;
            TextBlock textBlock;
            try
            {
                textBlock = ParseXAMLToTextBlock(toConvert);
                AttachHandlerEventToHyperlinks(textBlock);
            }
            catch
            {
                textBlock = new TextBlock();
                textBlock.Inlines.Add(toConvert);
            }

            return textBlock.Inlines.ToList();
        }

        private static TextBlock ParseXAMLToTextBlock(string xaml)
        {
            var parserContext = new System.Windows.Markup.ParserContext();
            parserContext.XmlnsDictionary[""] = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
            parserContext.XmlnsDictionary["x"] = "http://schemas.microsoft.com/winfx/2006/xaml";
            return System.Windows.Markup.XamlReader.Parse(xaml, parserContext) as TextBlock;
        }

        private static void AttachHandlerEventToHyperlinks(TextBlock textBlock)
        {
            foreach (Inline inline in textBlock.Inlines)
            {
                if (inline is Hyperlink link)
                {
                    link.Click += Hyperlink_Click;
                }
            }
        }

        private static void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Hyperlink hyperLink))
                return;
            Process.Start(hyperLink.NavigateUri.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
