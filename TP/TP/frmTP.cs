using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace TP
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frmTP : XtraForm
    {
        private GalleryDropDown skinGallery;
        private BarManager manager
        {
            get { return barManager1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public frmTP()
        {
            InitializeComponent();
            InitMenu();
        }

        private void InitMenu()
        {
            skinGallery = new GalleryDropDown();
            SkinHelper.InitSkinGalleryDropDown(skinGallery);
            skinGallery.Manager = manager;
            miSkin.DropDownControl = skinGallery;
            miSkin.ActAsDropDown = true;
            skinGallery.GalleryItemCheckedChanged += skinGallery_GalleryItemCheckedChanged;
            //miLookAndFeel = new BarSubItem(manager, "&Look and Feel");
            //miLookAndFeel.Popup += OnPopupLookAndFeel;

            //SkinManager.Default.Skins;
        }

        void skinGallery_GalleryItemCheckedChanged(object sender, GalleryItemEventArgs e)
        {
            if (e.Item.Checked)
            {
                SuspendRedraw();
                LookAndFeel.SetSkinStyle(e.Item.Caption);
                ResumeRedraw();
            }
        }

        private void frmTP_Load(object sender, EventArgs e)
        {
            channelController1.InitProvider("HighLevelClient");
            channelController1.NeedProtocol += channelController1_NeedProtocol;
            //channelController1.Form = this;
        }

        void channelController1_NeedProtocol(object sender, EventArgs e)
        {
            Protocol(sender);
        }

        private void sbRegister_Click(object sender, EventArgs e)
        {
            channelController1.Register();
        }

        private void sbUnregister_Click(object sender, EventArgs e)
        {
            channelController1.Unregister();
        }

        private void Protocol(object sender)
        {
            if (sender is double || sender is string)
            {
                string s = string.Format("{0}\t{1}{2}", DateTime.Now, sender, Environment.NewLine);

                SetText(textBox1, s);
            }
        }

        private delegate void StDelegate(TextBox info, string s);
        private void SetText(TextBox info, string s)
        {
            if (info.InvokeRequired)
            {
                StDelegate ddd = SetText;
                info.Invoke(ddd, new object[] { info, s });
            }
            else
            {
                info.AppendText(s);
            }
        }

        //private void OnPopupLookAndFeel(object sender, EventArgs e)
        //{
        //    BarSubItem item = sender as BarSubItem;
        //    if ((item != null) && (LookAndFeel != null))
        //    {
        //        foreach (BarItemLink link in item.ItemLinks)
        //        {
        //            CheckBarItemWithStyle style = link.Item as CheckBarItemWithStyle;
        //            if (style != null)
        //            {
        //                if (style.LookAndFeelStyle == LookAndFeelStyle.Skin)
        //                {
        //                    //style.Checked = this.UsingXP && !this.Mixed;
        //                }
        //                else
        //                {
        //                    style.Checked = AvailableStyle(style.LookAndFeelStyle);
        //                }
        //            }
        //        }
        //    }
        //}

        //private bool AvailableStyle(LookAndFeelStyle style)
        //{
        //    return LookAndFeel.Style == style;
        //}

    }

    //internal class CheckBarItemWithStyle : BarCheckItem
    //{
    //    public LookAndFeelStyle LookAndFeelStyle { get; set; }
    //}
}