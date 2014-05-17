﻿using System;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using MixModes.Synergy.VisualFramework.Windows;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using TP.WPF.ViewModels;

namespace TP.WPF.Views
{
	/// <summary>
	/// Interaction logic for MainShell.xaml
    /// Документация и примеры по графической библиотеки http://www.codeproject.com/Articles/275814/Introducing-MixModes-Synergy
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

        [Dependency(Required = true)]
	    public MainViewModel ViewModel
	    {
	        get { return (MainViewModel) DataContext; }
	        set { DataContext = value; }
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

            //
            
            
            var pn = new subDrumTypeFurnance();
            var pane = new DockPane
            {

                Width = 600,
                Height = 400,
                DockPaneState = DockPaneState.Content,
                //Header = GetPropValue(pn, "Name"),
                Header = "Барабанная печь",
                Content = pn
            };
            WindowsManager.AddFloatingWindow(pane);

            //TODO: Разобраться с причиной "обрыва" связи между данными и представлением в развернумо режиме.
	        //WindowsManager.DocumentContainer.AddDocument(pane);


	    }

	    private void GenereteDebugPanel()
	    {
            var pane = new DockPane
            {
                Height = 80,
                Header = "Панель отладочной информации",
                Content = new Resources.ucDebugPanel()
            };
	        WindowsManager.AddAutoHideWindow(pane,Dock.Bottom);
	    }

	    private void GenerateIndicatorsPanel()
	    {
            var pane = new DockPane
            {
                Width = 180,
                Header = "Индикаторы",
                Content = new Resources.ucIndicatorGroupFurnance()
            };
	        WindowsManager.AddPinnedWindow(pane,Dock.Right);

	    }
	}
}