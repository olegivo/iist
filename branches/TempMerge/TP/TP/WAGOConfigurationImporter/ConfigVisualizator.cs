using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WAGOConfigurationImporter.Properties;

namespace WAGOConfigurationImporter
{
    public partial class ConfigVisualizator : UserControl
    {
        public event EventHandler<SelectedModuleEventArgs> wagoModuleSelected;
        public ConfigVisualizator()
        {
            InitializeComponent();
        }

        public void OnWagoModuleSelected(SelectedModuleEventArgs e)
        {
            //Raise the wagoModuleSelected event.
            if (wagoModuleSelected != null)
                wagoModuleSelected(this, e);
        }
        
        //Makes this property bindable.
        private List<ModuleInfo> _wConfiguration;
        [Bindable(true)]
        public List<ModuleInfo> WConfiguration
        {
            get
            {
                return _wConfiguration;
            }
            set
            {
                if (value != null && value.Count>0)
                {
                    InitializePictureBox(value);
                    this.BackgroundImage = null;
                }
                    
            }
        }
        private void InitializePictureBox(List<ModuleInfo> moduleInformation)
        {
            const int paddingLeft = 30;
            const int paddingTop = 30;
            for (int i = 0; i < moduleInformation.Count; i++)
            {
                var PictureBox1 = new PictureBox
                    {
                        Location = new Point(i*31 + paddingLeft, 0 + paddingTop),
                        Size = new Size(31, 270),
                        TabStop = false
                    };
                int i1 = i;//без переменной - i всегда = 8;
                PictureBox1.MouseClick += delegate
                    {
                        OnWagoModuleSelected(new SelectedModuleEventArgs() { SelectedModule = moduleInformation[i1] });
                    };
                PictureBox1.MouseEnter += delegate { PictureBox1.Size = new Size(45, 287); };
                PictureBox1.MouseLeave += delegate { PictureBox1.Size = new Size(31, 270); };
                switch (moduleInformation[i].ModuleNo)
                {
                        //контроллеры
                    case "0750-0841":
                        PictureBox1.Image = Resources._750_000; break;
                    case "0750-0815":
                        PictureBox1.Image = Resources._750_000; break;
                        //модули
                    case "0750-0465":
                        PictureBox1.Image = Resources._750_465; break;
                    case "0750-04xx":
                        PictureBox1.Image = Resources._750_4xx; break;
                    case "0750-0554":
                        PictureBox1.Image = Resources._750_554; break;
                    case "0750-05xx":
                        PictureBox1.Image = Resources._750_5xx; break;
                    case "0750-0552":
                        PictureBox1.Image = Resources._750_552; break;
                    case "0750-0492":
                        PictureBox1.Image = Resources._750_492; break;
                    default:
                        PictureBox1.Image = Resources._750_600; break;
                }
                PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                PictureBox1.BorderStyle = BorderStyle.None;
                Controls.Add(PictureBox1);
            }


        }
    }
    public class SelectedModuleEventArgs : EventArgs
    {
        public ModuleInfo SelectedModule { get; set; }
    }
}
