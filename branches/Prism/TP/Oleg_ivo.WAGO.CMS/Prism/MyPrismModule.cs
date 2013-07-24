using Autofac;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Oleg_ivo.WAGO.CMS.View;

namespace Oleg_ivo.WAGO.CMS.Prism
{
    public class MyPrismModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IComponentContext context;

        public MyPrismModule(IRegionManager regionManager, IComponentContext context)
        {
            this.regionManager = regionManager;
            this.context = context;
        }

        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("MainRegion", typeof(MainView));
            //regionManager.RegisterViewWithRegion("DrumTypeFurnaceRegion", () => context.Resolve<ucDrumTypeFurnace>());
        }
    }
}