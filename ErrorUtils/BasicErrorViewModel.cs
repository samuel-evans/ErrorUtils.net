using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ErrorUtils
{
    public sealed class BasicErrorViewModel : IErrorViewModel
    {
        private Exception _exception;
        private string[] _displayedErrorData = null;

        public BasicErrorViewModel(Exception ex)
        {
            Initialise(ex);
        }

        public BasicErrorViewModel(Exception ex, string[] displayedErrorData)
        {
            _displayedErrorData = displayedErrorData;
            Initialise(ex);
        }

        private void Initialise(Exception ex)
        {
            _exception = ex;
            Icon = Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Error.Handle, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(40, 40));
        }

        public string MainText 
        {
            get
            {
                if (_displayedErrorData != null && _displayedErrorData.Any())
                {
                    try
                    {
                        return string.Format("An error has been encountered. The details have been logged, please notify support, quoting the reference {0}", _displayedErrorData);
                    }
                    catch
                    {
                        //If for whatever reason this doesn't work, we don't want another exception. Use the error message without data
                    }
                }
                return "An error has been encountered. The details have been logged, please notify support";
            }
        }

        public String AdditionalInformation { get { return "<TextBlock>Text in <Run Foreground=\"Red\">red</Run> <Hyperlink NavigateUri=\"www.google.com\">Standard Search</Hyperlink></TextBlock>"; } }

        public ImageSource Icon { get; private set; }
    }
}
