using ErrorUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WinForms47
{
    public class DetailedErrorViewModel : IErrorViewModel
    {
        private Exception _exception;
        private string[] _displayedErrorData = null;
        private CustomErrorResponse _customErrorResponse;

        public DetailedErrorViewModel(Exception ex, CustomErrorResponse customErrorResponse)
        {
            Initialise(ex, customErrorResponse);
        }

        public DetailedErrorViewModel(Exception ex, string[] displayedErrorData, CustomErrorResponse customErrorResponse)
        {
            _displayedErrorData = displayedErrorData;
            Initialise(ex, customErrorResponse);
        }

        private void Initialise(Exception ex, CustomErrorResponse customErrorResponse)
        {
            _exception = ex;
            _customErrorResponse = customErrorResponse;
            Icon = Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Warning.Handle, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(40, 40));
        }
        public string AdditionalInformation 
        { 
            get 
            {
                string aiSection = "";

                if (_displayedErrorData != null && _displayedErrorData.Any())
                {
                    try
                    {
                        aiSection = string.Format($"Error code {_displayedErrorData.First()}{Environment.NewLine}{Environment.NewLine}");
                    }
                    catch
                    {
                        //If for whatever reason this doesn't work, we don't want another exception. Use the error message without data
                    }
                }
                aiSection += $"{_exception.Message}{Environment.NewLine}{Environment.NewLine}{_exception.StackTrace}";
                return aiSection;
            } 
        }

        public string MainText => _customErrorResponse.Message;

        public ImageSource Icon { get; private set; }
    }
}
