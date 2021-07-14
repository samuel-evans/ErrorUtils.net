using System.Windows.Media;

namespace ErrorUtils
{
    public interface IErrorViewModel
    {
        string AdditionalInformation { get; }
        string MainText { get; }
        ImageSource Icon { get; }
    }
}