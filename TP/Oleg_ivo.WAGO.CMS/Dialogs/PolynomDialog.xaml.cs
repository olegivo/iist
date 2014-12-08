using System.Windows;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Base.WPF.Dialogs;

namespace Oleg_ivo.WAGO.CMS.Dialogs
{
    /// <summary>
    /// Interaction logic for PolynomDialog.xaml
    /// </summary>
    public partial class PolynomDialog : IModalWindow<PolynomDialogViewModel>
    {
        public PolynomDialog()
        {
            InitializeComponent();
        }

        [Dependency]
        public PolynomDialogViewModel ViewModel
        {
            get { return (PolynomDialogViewModel) DataContext; }
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
