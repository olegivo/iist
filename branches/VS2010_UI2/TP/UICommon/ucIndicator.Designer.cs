namespace UICommon
{
    partial class ucIndicator
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
            DevExpress.XtraGauges.Core.Model.ScaleLabel scaleLabel1 = new DevExpress.XtraGauges.Core.Model.ScaleLabel();
            DevExpress.XtraGauges.Core.Model.LinearScaleRange linearScaleRange1 = new DevExpress.XtraGauges.Core.Model.LinearScaleRange();
            DevExpress.XtraGauges.Core.Model.LinearScaleRange linearScaleRange2 = new DevExpress.XtraGauges.Core.Model.LinearScaleRange();
            DevExpress.XtraGauges.Core.Model.LinearScaleRange linearScaleRange3 = new DevExpress.XtraGauges.Core.Model.LinearScaleRange();
            DevExpress.XtraGauges.Core.Model.ScaleIndicatorState scaleIndicatorState1 = new DevExpress.XtraGauges.Core.Model.ScaleIndicatorState();
            DevExpress.XtraGauges.Core.Model.ScaleIndicatorState scaleIndicatorState2 = new DevExpress.XtraGauges.Core.Model.ScaleIndicatorState();
            DevExpress.XtraGauges.Core.Model.ScaleIndicatorState scaleIndicatorState3 = new DevExpress.XtraGauges.Core.Model.ScaleIndicatorState();
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
            this.linearScaleComponent1.EndPoint = new DevExpress.XtraGauges.Core.Base.PointF2D(62.5F, 33F);
            scaleLabel1.AppearanceText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
            scaleLabel1.Name = "Label0";
            scaleLabel1.Position = new DevExpress.XtraGauges.Core.Base.PointF2D(25F, 30F);
            scaleLabel1.Text = "Ph1:";
            this.linearScaleComponent1.Labels.AddRange(new DevExpress.XtraGauges.Core.Model.ILabel[] {
            scaleLabel1});
            this.linearScaleComponent1.MajorTickmark.FormatString = "{0:F0}";
            this.linearScaleComponent1.MajorTickmark.ShapeOffset = 6F;
            this.linearScaleComponent1.MajorTickmark.ShapeScale = new DevExpress.XtraGauges.Core.Base.FactorF2D(1.5F, 1F);
            this.linearScaleComponent1.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Linear_Style9_3;
            this.linearScaleComponent1.MajorTickmark.TextOffset = 35F;
            this.linearScaleComponent1.MaxValue = 10F;
            this.linearScaleComponent1.MinorTickCount = 5;
            this.linearScaleComponent1.MinorTickmark.ShapeOffset = 6F;
            this.linearScaleComponent1.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Linear_Style9_1;
            this.linearScaleComponent1.Name = "scale1";
            linearScaleRange1.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:LightCoral");
            linearScaleRange1.EndValue = 10F;
            linearScaleRange1.Name = "RangeHigh";
            linearScaleRange1.ShapeOffset = -20F;
            linearScaleRange1.StartThickness = 1F;
            linearScaleRange1.StartValue = 8F;
            linearScaleRange2.AppearanceRange.BorderWidth = 5F;
            linearScaleRange2.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:YellowGreen");
            linearScaleRange2.EndThickness = 1F;
            linearScaleRange2.EndValue = 8F;
            linearScaleRange2.Name = "RangeNormal";
            linearScaleRange2.ShapeOffset = -20F;
            linearScaleRange2.StartThickness = 1F;
            linearScaleRange2.StartValue = 4F;
            linearScaleRange3.AppearanceRange.BorderWidth = 1F;
            linearScaleRange3.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Coral");
            linearScaleRange3.EndThickness = 1F;
            linearScaleRange3.EndValue = 4F;
            linearScaleRange3.Name = "RangeLow";
            linearScaleRange3.ShapeOffset = -20F;
            this.linearScaleComponent1.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[] {
            linearScaleRange1,
            linearScaleRange2,
            linearScaleRange3});
            this.linearScaleComponent1.RescalingBestValues = true;
            this.linearScaleComponent1.StartPoint = new DevExpress.XtraGauges.Core.Base.PointF2D(62.5F, 217F);
            this.linearScaleComponent1.Value = 10F;
            this.linearScaleComponent1.MinMaxValueChanged += new System.EventHandler(this.linearScaleComponent1_MinMaxValueChanged);
            // 
            // linearScaleStateIndicatorComponent1
            // 
            this.linearScaleStateIndicatorComponent1.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(62.5F, 232F);
            this.linearScaleStateIndicatorComponent1.IndicatorScale = this.linearScaleComponent1;
            this.linearScaleStateIndicatorComponent1.Name = "lgPh1_Indicator1";
            scaleIndicatorState1.IntervalLength = 5F;
            scaleIndicatorState1.Name = "EmergencyHigh";
            scaleIndicatorState1.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight2;
            scaleIndicatorState1.StartValue = 9F;
            scaleIndicatorState2.IntervalLength = 2F;
            scaleIndicatorState2.Name = "Normal";
            scaleIndicatorState2.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight4;
            scaleIndicatorState2.StartValue = 7F;
            scaleIndicatorState3.IntervalLength = 4F;
            scaleIndicatorState3.Name = "EmergencyLow";
            scaleIndicatorState3.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight3;
            scaleIndicatorState3.StartValue = 3F;
            this.linearScaleStateIndicatorComponent1.States.AddRange(new DevExpress.XtraGauges.Core.Model.IIndicatorState[] {
            scaleIndicatorState1,
            scaleIndicatorState2,
            scaleIndicatorState3});
            this.linearScaleStateIndicatorComponent1.ZOrder = -100;
            // 
            // linearScaleLevelComponent1
            // 
            this.linearScaleLevelComponent1.LinearScale = this.linearScaleComponent1;
            this.linearScaleLevelComponent1.Name = "level1";
            this.linearScaleLevelComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.LevelShapeSetType.Style9;
            this.linearScaleLevelComponent1.ZOrder = -50;
            // 
            // ucIndicator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gaugeControl1);
            this.Name = "ucIndicator";
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
