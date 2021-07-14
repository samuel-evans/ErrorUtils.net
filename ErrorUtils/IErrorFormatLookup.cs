using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErrorUtils
{
    public interface IErrorFormatLookup
    {
        IErrorViewModel GetViewModel(Exception ex);

        IErrorViewModel GetViewModel(Exception ex, string[] displayedErrorData);
    }
}
