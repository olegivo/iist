using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Data;

namespace AmCharts.Windows.QuickCharts
{
    public class InheritedLineGraph : SerialChart
    {

        public  InheritedLineGraph()
        {
            this._graphs.CollectionChanged += new NotifyCollectionChangedEventHandler(OnGraphsCollectionChanged);
        }


        public override void OnApplyTemplate()
        {

            if (Graphs != null)
            {

                base._graphs = Graphs;
                base.OnApplyTemplate();              
                RenderGraphs();


            }


        }


        public static readonly DependencyProperty ChartChannelsProperty =
            DependencyProperty.Register("Graphs", typeof(DiscreetClearObservableCollection<SerialGraph>), typeof(InheritedLineGraph), new PropertyMetadata(default(DiscreetClearObservableCollection<SerialGraph>)));

        public override DiscreetClearObservableCollection<SerialGraph> Graphs
        {
            get
            {
                return (DiscreetClearObservableCollection<SerialGraph>) GetValue(ChartChannelsProperty);
            }
            set
            {
                SetValue(ChartChannelsProperty, value);
            }
        }

        //TODO: Разобраться с необходимыми методами, необходимых для рендеринга  см. OnGraphsCollectionChanged
        private void RenderGraphs()
        {
            if(this.Graphs != null)
            {

                foreach (SerialGraph graph in this.Graphs)
                {
                    graph.Render();
                    graph.ValueMemberPathChanged += new EventHandler<DataPathEventArgs>(OnGraphValueMemberPathChanged);
                    if (graph.Brush == null && PresetBrushes.Count > 0)
                    {
                        graph.Brush = PresetBrushes[_graphs.IndexOf(graph) % PresetBrushes.Count];
                    }
                    base.AddGraphToCanvas(graph);
                    base.AddIndicator(graph);
                }
            }
        }




    }
}
