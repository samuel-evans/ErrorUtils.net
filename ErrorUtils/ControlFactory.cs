using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ErrorUtils
{
    public class ControlFactory
    {

        private static readonly object padlock = new object();
        private static ControlFactory instance = null;

        public static ControlFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new ControlFactory();
                        }
                    }
                }
                return instance;
            }
        }

        private IErrorFormatLookup _errorLookup = new DefaultErrorFormatLookup();

        public void SetErrorLookup(IErrorFormatLookup errorLookup)
        {
            _errorLookup = errorLookup;
        }

        private ControlFactory()
        {
            
        }

        public UserControl GetErrorControl(Exception ex)
        {
            return new ErrorControl(_errorLookup.GetViewModel(ex));
        }

        public UserControl GetErrorControl(Exception ex, string[] displayedErrorData)
        {
            return new ErrorControl(_errorLookup.GetViewModel(ex, displayedErrorData));
        }

    }
}
