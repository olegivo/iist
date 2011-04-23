using System;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
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
            //channelController1.Form = this;
        }

        private void spinEdit3_EditValueChanged(object sender, EventArgs e)
        {

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