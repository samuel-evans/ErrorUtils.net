using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErrorUtils
{
    public class DefaultErrorFormatLookup : IErrorFormatLookup
    {
        public IErrorViewModel GetViewModel(Exception ex)
        {
            return new BasicErrorViewModel(ex);
        }

        public IErrorViewModel GetViewModel(Exception ex, string[] displayedErrorData)
        {
            return new BasicErrorViewModel(ex, displayedErrorData);
        }
    }
}
