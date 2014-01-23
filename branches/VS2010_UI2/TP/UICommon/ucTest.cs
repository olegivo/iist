using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace UICommon
{
    public partial class ucTest : XtraUserControl
    {
        public ucTest()
        {
            InitializeComponent();

            radioGroup1.Properties.Items.Add(new RadioGroupItem(Orientation.Horizontal, "Горизонтально"));
            radioGroup1.Properties.Items.Add(new RadioGroupItem(Orientation.Vertical, "Вертикально"));
            radioGroup1.SelectedIndex = 0;

            textEditBindingSource.Add(textEdit1);
            radioGroupBindingSource.Add(radioGroup1);
            spinEditBindingSourceMax.Add(seMax);
            spinEditBindingSourceAllowedMax.Add(seAllowedMax);
            spinEditBindingSourceAllowedMin.Add(seAllowedMin);
            spinEditBindingSourceMin.Add(seMin);
            spinEditBindingSourceEditValue.Add(seValue);
            textEditBindingSourceCaption.Add(teCaption);
        }

        private void ucTest_Load(object sender, System.EventArgs e)
        {
//rangeTrackBarControl1.
        }
    }
}
