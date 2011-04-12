using System;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraGauges.Core.Model;
using DevExpress.XtraGauges.Win.Gauges.Linear;
using DevExpress.XtraNavBar;

namespace TP
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Form1 : XtraForm
    {
        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            var items = navBarControl.Items.Cast<NavBarItem>();
            foreach (var item in items)
                item.Tag = GetGaugageByNavBarItem(item.Name);

            foreach (var item in items.Where(item => item.Name.Length <= 7))
                item.LinkClicked += nbiUsual_LinkClicked;

            foreach (NavBarItem item in items.Where(item => item.Name.EndsWith("Normal")))
                item.LinkClicked += nbiNormal_LinkClicked;

            foreach (NavBarItem item in items.Where(item => item.Name.EndsWith("Low")))
                item.LinkClicked += nbiLow_LinkClicked;

            foreach (NavBarItem item in items.Where(item => item.Name.EndsWith("High")))
                item.LinkClicked += nbiHigh_LinkClicked;

        }

        private LinearGauge GetGaugageByNavBarItem(string itemName)
        {
            string name = (new[] {"Ph1", "Ph2", "Du10"}).Where(itemName.Contains).FirstOrDefault();
            switch (name)
            {
                case "Ph1":
                    return lgPh1;
                case "Ph2":
                    return lgPh2;
                case "Du10":
                    return lgDu10;
            }

            return null;
        }

        private void nbiUsual_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            SetValue(((LinearGauge) ((NavBarItem)sender).Tag), ValueType.Usual);
        }

        private void nbiNormal_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            SetValue(((LinearGauge) ((NavBarItem)sender).Tag), ValueType.Normal);
        }

        private void nbiLow_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            SetValue(((LinearGauge) ((NavBarItem)sender).Tag), ValueType.Low);
        }

        private void nbiHigh_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            SetValue(((LinearGauge) ((NavBarItem)sender).Tag), ValueType.High);
        }

        private Random _random = new Random();

        private void SetValue(LinearGauge linearGauge, ValueType valueType)
        {
            var linearScaleComponent = linearGauge.Scales[0];
            float minValue = linearScaleComponent.MinValue;
            float maxValue = linearScaleComponent.MaxValue;

            BaseIndicatorState state = linearGauge.Indicators[0].States.Where(st => st.Name.Contains(valueType.ToString())).Cast<BaseIndicatorState>().FirstOrDefault();
            if (state != null)
            {
                minValue = state.StartValue;
                maxValue = minValue + state.IntervalLength;
            }

            linearScaleComponent.Value = _random.Next((int)minValue, (int)maxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        public enum ValueType
        {
            Usual = 0,
            Normal = 1,
            Low = 2,
            High = 4
        }

        private void nbiSmoke_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            (new XtraForm1()).ShowDialog();
        }
    }
}