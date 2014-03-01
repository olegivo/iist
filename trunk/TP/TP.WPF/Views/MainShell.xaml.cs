using System;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Markup;
using MixModes.Synergy.VisualFramework.Windows;
using TP.WPF.Views;

namespace TP.WPF
{
	/// <summary>
	/// Interaction logic for MainShell.xaml
	/// </summary>
	public partial class MainShell
	{
	    private const string ViewNameSpaceName = "TP.WPF.Views";
		public MainShell()
		{
			this.InitializeComponent();
			this.GenerateLayout();
            this.GenereteDebugPanel();
		    this.GenerateIndicatorsPanel();
		}

        private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

	    private void GenerateLayout()
	    {
            //Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), ViewNameSpaceName);
            //for (int i = 0; i < typelist.Length; i++)
            //{

            //    if (typelist[i].Name.StartsWith("uc"))
            //    {

            //        Type type = Type.GetType(ViewNameSpaceName + "." + typelist[i].Name, true);
            //        object instance = Activator.CreateInstance(type);

            //        DockPane pane = new DockPane();
            //        pane.Width = 600;
            //        pane.Height = 400;
            //        pane.MinHeight = 240;
            //        pane.MinWidth = 320;
            //        pane.DockPaneState = DockPaneState.Content;
            //        pane.Header = GetPropValue(instance, "Name");
            //        pane.Content = instance;//new Views.ucDrumTypeFurnace();
            //        pane.DockPaneState = DockPaneState.Content;
            //        WindowsManager.AddFloatingWindow(pane);
            //    }
            //}

            //ТЕСТ
            //TODO:var pn = new Views.subDrumTypeFurnance();
            //var pn = new Resources.ucIndicatorGroupFurnance();
            DockPane pane = new DockPane
            {
                Width = 600,
                Height = 400,
                DockPaneState = DockPaneState.Content,
                /*TODO:Header = GetPropValue(pn, "Name"),
                Content = pn*/
            };
	        pane.DockPaneState = DockPaneState.Content;
            WindowsManager.AddFloatingWindow(pane);

	    }

	    private void GenereteDebugPanel()
	    {
            DockPane pane = new DockPane();
	        pane.Height = 80;
	        pane.Header = "Панель отладочной информации";
	        pane.Content = new Resources.ucDebugPanel();
            WindowsManager.AddAutoHideWindow(pane,Dock.Bottom);
	    }

	    private void GenerateIndicatorsPanel()
	    {
            DockPane pane = new DockPane();
	        pane.Width = 180;
	        pane.Header = "Индикаторы";
	        pane.Content = new Resources.ucIndicatorGroupFurnance();
            WindowsManager.AddPinnedWindow(pane,Dock.Right);

	    }
	}
}