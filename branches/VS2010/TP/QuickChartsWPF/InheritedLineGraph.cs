using System;
using System.Collections.Specialized;
using System.Windows;

namespace AmCharts.Windows.QuickCharts
{
    public class InheritedLineGraph : SerialChart
    {

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
            this.Graphs.CollectionChanged += new NotifyCollectionChangedEventHandler(OnGraphsCollectionChanged);
            //base.OnApplyTemplate(); //?
            if(this.Graphs != null)
            {

                foreach (SerialGraph graph in this.Graphs)
                {
                    graph.Render();
                    if (graph.Brush == null && PresetBrushes.Count > 0)
                    {
                        graph.Brush = PresetBrushes[_graphs.IndexOf(graph) % PresetBrushes.Count];
                    }
                    base.AddGraphToCanvas(graph);
                    //base.AddIndicator(graph);
                }
            }
        }




    }
}
