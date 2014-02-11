using System.Windows.Forms;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.WAGO.Controls.LevelEditors
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PolynomEditForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public PolynomEditForm()
        {
            InitializeComponent();
        }

        private Polynom _polynom;

        /// <summary>
        /// 
        /// </summary>
        public Polynom Polynom
        {
            get { return _polynom; }
            set
            {
                _polynom = value;
                if (Polynom !=null)
                {
                    //преобразование полинома в датасет:
                    foreach (DtsPolynom.PolynomCoefficientsRow row in Polynom.PowerCoefficients.PolynomCoefficients)
                        dtsPolynom1.PolynomCoefficients.ImportRow(row);
                }
            }
        }

        private void PolynomEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            dataGridView1.EndEdit();
            dtsPolynom1.AcceptChanges();
            Polynom.PowerCoefficients.PolynomCoefficients.Clear();
            //преобразование датасета в полином:
            foreach (DtsPolynom.PolynomCoefficientsRow row in dtsPolynom1.PolynomCoefficients)
                Polynom.PowerCoefficients.PolynomCoefficients.ImportRow(row);
        }
    }
}
