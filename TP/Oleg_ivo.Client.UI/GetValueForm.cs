using System.Windows.Forms;

namespace Oleg_ivo.HighLevelClient.UI
{
    public partial class GetValueForm : Form
    {
        public GetValueForm()
        {
            InitializeComponent();
        }

        public string Value
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
    }
}
