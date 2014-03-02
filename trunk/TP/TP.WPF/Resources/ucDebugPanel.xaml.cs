using System.Drawing;
using System.Windows.Controls;

namespace TP.WPF.Resources
{
    /// <summary>
    /// Interaction logic for ucDebugPanel.xaml
    /// </summary>
    public partial class ucDebugPanel : UserControl
    {
        public ucDebugPanel()
        {
            InitializeComponent();
        }

        private void tbDebugInfo_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbDebugInfo.ScrollToEnd();
        }
    }
}
