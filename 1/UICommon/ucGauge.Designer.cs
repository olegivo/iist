namespace UICommon
{
    partial class ucGauge
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGauges.Core.Model.ScaleLabel scaleLabel2 = new DevExpress.XtraGauges.Core.Model.ScaleLabel();
            DevExpress.XtraGauges.Core.Model.LinearScaleRange linearScaleRange4 = new DevExpress.XtraGauges.Core.Model.LinearScaleRange();
            DevExpress.XtraGauges.Core.Model.LinearScaleRange linearScaleRange5 = new DevExpress.XtraGauges.Core.Model.LinearScaleRange();
            DevExpress.XtraGauges.Core.Model.LinearScaleRange linearScaleRange6 = new DevExpress.XtraGauges.Core.Model.LinearScaleRange();
            DevExpress.XtraGauges.Core.Model.ScaleIndicatorState scaleIndicatorState4 = new DevExpress.XtraGauges.Core.Model.ScaleIndicatorState();
            DevExpress.XtraGauges.Core.Model.ScaleIndicatorState scaleIndicatorState5 = new DevExpress.XtraGauges.Core.Model.ScaleIndicatorState();
            DevExpress.XtraGauges.Core.Model.ScaleIndicatorState scaleIndicatorState6 = new DevExpress.XtraGauges.Core.Model.ScaleIndicatorState();
            this.gaugeControl1 = new DevExpress.XtraGauges.Win.GaugeControl();
            this.linearGauge1 = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearGauge();
            this.linearScaleBackgroundLayerComponent1 = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleBackgroundLayerComponent();
            this.linearScaleComponent1 = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleComponent();
            this.linearScaleStateIndicatorComponent1 = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleStateIndicatorComponent();
            this.linearScaleLevelComponent1 = new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleLevelComponent();
            ((System.ComponentModel.ISupportInitialize)(this.linearGauge1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearScaleBackgroundLayerComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearScaleComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearScaleStateIndicatorComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearScaleLevelComponent1)).BeginInit();
            this.SuspendLayout();
            // 
            // gaugeControl1
            // 
            this.gaugeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gaugeControl1.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[] {
            this.linearGauge1});
            this.gaugeControl1.Location = new System.Drawing.Point(0, 0);
            this.gaugeControl1.Name = "gaugeControl1";
            this.gaugeControl1.Size = new System.Drawing.Size(204, 366);
            this.gaugeControl1.TabIndex = 5;
            this.gaugeControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gaugeControl1_MouseClick);
            // 
            // linearGauge1
            // 
            this.linearGauge1.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleBackgroundLayerComponent[] {
            this.linearScaleBackgroundLayerComponent1});
            this.linearGauge1.Bounds = new System.Drawing.Rectangle(6, 6, 192, 354);
            this.linearGauge1.Indicators.AddRange(new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleStateIndicatorComponent[] {
            this.linearScaleStateIndicatorComponent1});
            this.linearGauge1.Levels.AddRange(new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleLevelComponent[] {
            this.linearScaleLevelComponent1});
            this.linearGauge1.Name = "linearGauge1";
            this.linearGauge1.Scales.AddRange(new DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleComponent[] {
            this.linearScaleComponent1});
            // 
            // linearScaleBackgroundLayerComponent1
            // 
            this.linearScaleBackgroundLayerComponent1.LinearScale = this.linearScaleComponent1;
            this.linearScaleBackgroundLayerComponent1.Name = "bg1";
            this.linearScaleBackgroundLayerComponent1.ScaleEndPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.497F, 0.135F);
            this.linearScaleBackgroundLayerComponent1.ScaleStartPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.497F, 0.865F);
            this.linearScaleBackgroundLayerComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.Linear_Style9;
            this.linearScaleBackgroundLayerComponent1.ZOrder = 1000;
            // 
            // linearScaleComponent1
            // 
            this.linearScaleComponent1.AppearanceTickmarkText.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.linearScaleComponent1.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
            this.linearScaleComponent1.AutoRescaling = true;
            this.linearScaleComponent1.EndPoint = new DevExpress.XtraGauges.Core.Base.PointF2D(62.5F, 33F);
            scaleLabel2.AppearanceText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
            scaleLabel2.Name = "Label0";
            scaleLabel2.Position = new DevExpress.XtraGauges.Core.Base.PointF2D(25F, 30F);
            scaleLabel2.Text = "Ph1:";
            this.linearScaleComponent1.Labels.AddRange(new DevExpress.XtraGauges.Core.Model.ILabel[] {
            scaleLabel2});
            this.linearScaleComponent1.MajorTickmark.FormatString = "{0:F0}";
            this.linearScaleComponent1.MajorTickmark.ShapeOffset = 6F;
            this.linearScaleComponent1.MajorTickmark.ShapeScale = new DevExpress.XtraGauges.Core.Base.FactorF2D(1.5F, 1F);
            this.linearScaleComponent1.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Linear_Style9_3;
            this.linearScaleComponent1.MajorTickmark.TextOffset = 35F;
            this.linearScaleComponent1.MaxValue = 14F;
            this.linearScaleComponent1.MinorTickCount = 5;
            this.linearScaleComponent1.MinorTickmark.ShapeOffset = 6F;
            this.linearScaleComponent1.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Linear_Style9_1;
            this.linearScaleComponent1.MinValue = 3F;
            this.linearScaleComponent1.Name = "scale1";
            linearScaleRange4.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:LightCoral");
            linearScaleRange4.EndValue = 14F;
            linearScaleRange4.Name = "RangeHigh";
            linearScaleRange4.ShapeOffset = -20F;
            linearScaleRange4.StartThickness = 1F;
            linearScaleRange4.StartValue = 9F;
            linearScaleRange5.AppearanceRange.BorderWidth = 5F;
            linearScaleRange5.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:YellowGreen");
            linearScaleRange5.EndThickness = 1F;
            linearScaleRange5.EndValue = 9F;
            linearScaleRange5.Name = "RangeNormal";
            linearScaleRange5.ShapeOffset = -20F;
            linearScaleRange5.StartThickness = 1F;
            linearScaleRange5.StartValue = 7F;
            linearScaleRange6.AppearanceRange.BorderWidth = 1F;
            linearScaleRange6.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Coral");
            linearScaleRange6.EndThickness = 1F;
            linearScaleRange6.EndValue = 7F;
            linearScaleRange6.Name = "RangeLow";
            linearScaleRange6.ShapeOffset = -20F;
            linearScaleRange6.StartValue = 3F;
            this.linearScaleComponent1.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[] {
            linearScaleRange4,
            linearScaleRange5,
            linearScaleRange6});
            this.linearScaleComponent1.StartPoint = new DevExpress.XtraGauges.Core.Base.PointF2D(62.5F, 217F);
            this.linearScaleComponent1.Value = 13F;
            this.linearScaleComponent1.MinMaxValueChanged += new System.EventHandler(this.linearScaleComponent1_MinMaxValueChanged);
            // 
            // linearScaleStateIndicatorComponent1
            // 
            this.linearScaleStateIndicatorComponent1.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(62.5F, 232F);
            this.linearScaleStateIndicatorComponent1.IndicatorScale = this.linearScaleComponent1;
            this.linearScaleStateIndicatorComponent1.Name = "lgPh1_Indicator1";
            scaleIndicatorState4.IntervalLength = 5F;
            scaleIndicatorState4.Name = "EmergencyHigh";
            scaleIndicatorState4.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight2;
            scaleIndicatorState4.StartValue = 9F;
            scaleIndicatorState5.IntervalLength = 2F;
            scaleIndicatorState5.Name = "Normal";
            scaleIndicatorState5.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight4;
            scaleIndicatorState5.StartValue = 7F;
            scaleIndicatorState6.IntervalLength = 4F;
            scaleIndicatorState6.Name = "EmergencyLow";
            scaleIndicatorState6.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight3;
            scaleIndicatorState6.StartValue = 3F;
            this.linearScaleStateIndicatorComponent1.States.AddRange(new DevExpress.XtraGauges.Core.Model.IIndicatorState[] {
            scaleIndicatorState4,
            scaleIndicatorState5,
            scaleIndicatorState6});
            this.linearScaleStateIndicatorComponent1.ZOrder = -100;
            // 
            // linearScaleLevelComponent1
            // 
            this.linearScaleLevelComponent1.LinearScale = this.linearScaleComponent1;
            this.linearScaleLevelComponent1.Name = "level1";
            this.linearScaleLevelComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.LevelShapeSetType.Style9;
            this.linearScaleLevelComponent1.ZOrder = -50;
            // 
            // ucGauge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gaugeControl1);
            this.Name = "ucGauge";
            this.Size = new System.Drawing.Size(204, 366);
            ((System.ComponentModel.ISupportInitialize)(this.linearGauge1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearScaleBackgroundLayerComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearScaleComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearScaleStateIndicatorComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linearScaleLevelComponent1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGauges.Win.GaugeControl gaugeControl1;
        private DevExpress.XtraGauges.Win.Gauges.Linear.LinearGauge linearGauge1;
        private DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleBackgroundLayerComponent linearScaleBackgroundLayerComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleComponent linearScaleComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleStateIndicatorComponent linearScaleStateIndicatorComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Linear.LinearScaleLevelComponent linearScaleLevelComponent1;
    }
}
