using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGauges.Core.Model;

namespace UICommon
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucIndicator : XtraUserControl
    {
        private float _allowedMinValue;
        private float _allowedMaxValue;

        /// <summary>
        /// 
        /// </summary>
        public ucIndicator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Текущее значение шкалы
        /// </summary>
        public float EditValue
        {
            get { return linearScaleComponent1.Value; }
            set
            {
                if (linearScaleComponent1.Value != value)
                {
                    linearScaleComponent1.Value = value;
                    RefreshStateRange();
                }
            }
        }

        /// <summary>
        /// Минимальное значение шкалы
        /// </summary>
        public float MinValue
        {
            get { return linearScaleComponent1.MinValue; }
            set
            {
                if (linearScaleComponent1.MinValue != value)
                {
                    linearScaleComponent1.MinValue = value;
                    RefreshStateRange();
                }
                //if (AllowedMinValue < MinValue) AllowedMinValue = MinValue;
                //if (AllowedMaxValue < MinValue) AllowedMaxValue = MinValue;
            }
        }

        /// <summary>
        /// Максимальное значение шкалы
        /// </summary>
        public float MaxValue
        {
            get { return linearScaleComponent1.MaxValue; }
            set
            {
                if (linearScaleComponent1.MaxValue != value)
                {
                    linearScaleComponent1.MaxValue = value;
                    RefreshStateRange();
                }
                //if (AllowedMinValue > MaxValue) AllowedMinValue = MaxValue;
                //if (AllowedMaxValue > MaxValue) AllowedMaxValue = MaxValue;
            }
        }

        /// <summary>
        /// Количество делений шкалы
        /// </summary>
        public int TickCount
        {
            get { return _tickCount; }
            set
            {
                if(_tickCount!=value)
                {
                    _tickCount = value;
                    Refresh();
                }
            }
        }

        /// <summary>
        /// Минимальное нормальное (неаварийное) значение шкалы
        /// </summary>
        public float AllowedMinValue
        {
            get { return _allowedMinValue; }
            set
            {
                if (_allowedMinValue != value)
                {
                    _allowedMinValue = value;
                    RefreshStateRange();
                }
            }
        }

        /// <summary>
        /// Максимальное нормальное (неаварийное) значение шкалы
        /// </summary>
        public float AllowedMaxValue
        {
            get { return _allowedMaxValue; }
            set
            {
                if (_allowedMaxValue != value)
                {
                    _allowedMaxValue = value;
                    RefreshStateRange();
                }
            }
        }

        /// <summary>
        /// Текст
        /// </summary>
        public string Caption
        {
            get { return linearScaleComponent1.Labels[0].Text; }
            set { linearScaleComponent1.Labels[0].Text = value; }
        }

        private void RefreshStateRange()
        {
            ScaleIndicatorState stateHigh = (ScaleIndicatorState) linearScaleStateIndicatorComponent1.States["EmergencyHigh"];
            ScaleIndicatorState stateNormal = (ScaleIndicatorState) linearScaleStateIndicatorComponent1.States["Normal"];
            ScaleIndicatorState stateLow = (ScaleIndicatorState) linearScaleStateIndicatorComponent1.States["EmergencyLow"];

            IRange rangeHigh = linearScaleComponent1.Ranges["RangeHigh"];
            IRange rangeNormal = linearScaleComponent1.Ranges["RangeNormal"];
            IRange rangeLow = linearScaleComponent1.Ranges["RangeLow"];

            rangeLow.StartValue = stateLow.StartValue = MinValue;
            stateLow.IntervalLength = AllowedMinValue - MinValue;
            rangeNormal.StartValue = rangeLow.EndValue = stateNormal.StartValue = AllowedMinValue;
            stateNormal.IntervalLength = AllowedMaxValue - AllowedMinValue;
            rangeHigh.StartValue = rangeNormal.EndValue = stateHigh.StartValue = AllowedMaxValue;
            stateHigh.IntervalLength = MaxValue - AllowedMaxValue;
            rangeHigh.EndValue = MaxValue;


            //linearScaleComponent1.AutoRescaling = false;

            //IScaleIndicatorState state = null;
            //if (EditValue > MinValue)
            //{
            //    if (EditValue > AllowedMinValue)
            //    {
            //        if (EditValue > AllowedMaxValue)
            //        {
            //            state = stateHigh;
            //        }
            //        else
            //        {
            //            state = stateNormal;
            //        }
            //    }
            //    else
            //    {
            //        state = stateLow;
            //    }
            //}

            //foreach (IScaleIndicatorState state1 in linearScaleStateIndicatorComponent1.States)
            //{
            //    //state1.Name
            //    float num = Math.Min(state1.StartValue, state1.StartValue + state1.IntervalLength);
            //    float num2 = Math.Max(state1.StartValue, state1.StartValue + state1.IntervalLength);
            //    if ((EditValue >= num) && (EditValue <= num2))
            //    {
            //        //return linearScaleStateIndicatorComponent1.States.IndexOf(state1);
            //    }
            //}

            //linearScaleStateIndicatorComponent1.StateIndex = linearScaleStateIndicatorComponent1.States.IndexOf(state);
            
            //Console.Clear();
            Console.WriteLine("High: {0} - {1}", rangeHigh.StartValue, rangeHigh.EndValue);
            Console.WriteLine("Norm: {0} - {1}", rangeNormal.StartValue, rangeNormal.EndValue);
            Console.WriteLine("Low:  {0} - {1}", rangeLow.StartValue, rangeLow.EndValue);
            Console.WriteLine("State: {0}", linearScaleStateIndicatorComponent1.State.Name);
            Console.WriteLine();
        }

        private void linearScaleComponent1_MinMaxValueChanged(object sender, EventArgs e)
        {
            float range = MaxValue - MinValue;

            if (range > 0)
            {
				//todo: tick count?!
                int i = Convert.ToInt32(range);
                //if (i > 10) i = 10;
                linearScaleComponent1.MajorTickCount = i;                
            }
        }

        private bool enlarged = false;
        private int _tickCount;

        private void gaugeControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left && !enlarged)
            {
                DockStyle dock = Dock;
                Control parent = Parent;

                Form form = new Form();
                Point p = MousePosition;

                parent.SuspendLayout();

                form.SuspendLayout();
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(p.X - Width/2, p.Y - Height/2);
                Dock = DockStyle.Fill;
                form.Controls.Add(this);
                form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                form.Size = new Size(160, 300);
                form.ResumeLayout(false);

                enlarged = true;
                form.ShowDialog();
                enlarged = false;

                parent.Controls.Add(this);
                Dock = dock;
                parent.ResumeLayout(false);
            }
        }
    }
}
