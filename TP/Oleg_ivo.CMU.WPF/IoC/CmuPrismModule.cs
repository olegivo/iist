using Autofac;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Oleg_ivo.CMU.WPF.Views;

namespace Oleg_ivo.CMU.WPF.IoC
{
    public class CmuPrismModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IComponentContext context;

        public CmuPrismModule(IRegionManager regionManager, IComponentContext context)
        {
            this.regionManager = regionManager;
            this.context = context;
        }

        public void Initialize()
        {
            //We need to register our Module in MainRegion.
            regionManager.RegisterViewWithRegion("MainRegion", () => context.Resolve<MainView>());
            //regionManager.RegisterViewWithRegion("DrumTypeFurnaceRegion", () => context.Resolve<ucDrumTypeFurnace>());
            //regionManager.RegisterViewWithRegion("ReheatChamberRegion", () => context.Resolve<ucReheatChamber>());
            //regionManager.RegisterViewWithRegion("HeatExchangerRegion", () => context.Resolve<ucHeatExchanger>());
            //regionManager.RegisterViewWithRegion("CycloneAndScrubberRegion", () => context.Resolve<ucCycloneAndScrubber>());
            //regionManager.RegisterViewWithRegion("FinishCleaningRegion", () => context.Resolve<ucFinishCleaning>());
            //regionManager.RegisterViewWithRegion("SummaryTableRegion", () => context.Resolve<ucSummaryTable>());
        }

    }
}