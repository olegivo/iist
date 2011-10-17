using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Oleg_ivo.WAGO.Controls.LevelEditors
{
    ///<summary>
    ///
    ///</summary>
    [TypeConverterAttribute(typeof(ExpandableObjectConverter))]
    public partial class CMContainer : Component
    {
        private Control bindingControl;
        private DataSet dataSet;

        ///<summary>
        ///
        ///</summary>
        public CMContainer()
        {
            InitializeComponent();
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="container"></param>
        public CMContainer(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        ///<summary>
        ///
        ///</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DefaultValue(null)]
        public Control BindingControl
        {
            get { return bindingControl; }
            set { bindingControl = value; }
        }

        ///<summary>
        ///
        ///</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DefaultValue(null)]
        public DataSet DataSet
        {
            get { return dataSet; }
            set { dataSet = value; }
        }

        public CurrencyManager GetCurrencyManager()
        {
            return null;
        }
    }
}
