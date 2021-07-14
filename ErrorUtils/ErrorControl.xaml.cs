using System.Windows.Controls;

namespace ErrorUtils
{
    public partial class ErrorControl : UserControl
    {
        private IErrorViewModel _errorViewModel;

        public ErrorControl(IErrorViewModel errorViewModel)
        {
            InitializeComponent();

            _errorViewModel = errorViewModel;
            DataContext = _errorViewModel;
        }
    }
}
