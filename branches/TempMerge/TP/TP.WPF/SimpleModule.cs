using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using TP.WPF.Views;

namespace TP.WPF
{
    public class SimpleModule : IModule
    {
        private readonly IRegionViewRegistry regionViewRegistry;
        private readonly UnityContainer container;

        public SimpleModule(IRegionViewRegistry registry, UnityContainer container)
        {
            this.regionViewRegistry = registry;
            this.container = container;
        }

        public void Initialize()
        {
            //We need to register our Module in MainRegion.
            //regionViewRegistry.RegisterViewWithRegion("MainRegion", () => container.Resolve<Views.MainView));
            regionViewRegistry.RegisterViewWithRegion("DrumTypeFurnaceRegion", () => container.Resolve<ucDrumTypeFurnace>());
            regionViewRegistry.RegisterViewWithRegion("ReheatChamberRegion", () => container.Resolve<ucReheatChamber>());
            regionViewRegistry.RegisterViewWithRegion("HeatExchangerRegion", () => container.Resolve<ucHeatExchanger>());
            regionViewRegistry.RegisterViewWithRegion("CycloneAndScrubberRegion", () => container.Resolve<ucCycloneAndScrubber>());
            regionViewRegistry.RegisterViewWithRegion("FinishCleaningRegion", () => container.Resolve<ucFinishCleaning>());
            regionViewRegistry.RegisterViewWithRegion("SummaryTableRegion", () => container.Resolve<ucSummaryTable>());
        }

    }
}