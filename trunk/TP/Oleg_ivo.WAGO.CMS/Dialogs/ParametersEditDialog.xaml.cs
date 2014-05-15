using System.Windows;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using UICommon.WPF.Dialogs;

namespace Oleg_ivo.WAGO.CMS.Dialogs
{
    /// <summary>
    /// Interaction logic for PolynomDialog.xaml
    /// </summary>
    public partial class ParametersEditDialog : Window, IModalWindow<ParametersEditDialogViewModel>
    {
        public ParametersEditDialog()
        {
            InitializeComponent();
        }

        [Dependency]
        public ParametersEditDialogViewModel ViewModel
        {
            get { return (ParametersEditDialogViewModel)DataContext; }
            set { DataContext = value; }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}