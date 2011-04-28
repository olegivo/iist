﻿using System;
using System.Windows.Forms;
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
            skinGallery.Manager = barManager1;
            miSkin.DropDownControl = skinGallery;
            miSkin.ActAsDropDown = true;
            skinGallery.GalleryItemCheckedChanged += skinGallery_GalleryItemCheckedChanged;
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
            channelController1.HasReadChannel += channelController1_HasReadChannel;
        }

        void channelController1_HasReadChannel(object sender, Oleg_ivo.Client.CallbackHandler.DataEventArgs e)
        {
            float value = Convert.ToSingle(e.Message.Value);
            int channelId = e.Message.LogicalChannelId;
            switch (channelId)
            {
                case 1:
                    break; //TП1	температура в циклонной вихревой топке
                case 2:
                    break; //TП2	температура в загрузочной системе
                case 3:
                    break; //TП3	температура в камере дожигания
                case 4:
                    break; //TР4	температура в теплообменнике ТО1
                case 5:
                    break; //TР5	температура в теплообменнике ТО2
                case 6:
                    break; //TС6	температура перед рукавным фильтром
                case 7:
                    break; //TС7	температура перед дымососом
                case 8:
                    break; //TС8	температура воды в системе охлаждения
                case 9:
                    break; //Р	разрежение в камере дожигания
                case 10:
                    ucCyclonAndScrubber1.Ph1 = value;
                    break; //рН1	уровень рН в СФ1
                case 11:
                    ucCyclonAndScrubber1.Ph2 = value;
                    break; //рН2	уровень рН в СФ2
                case 12:
                    break; //S	скорость вращения печи
                case 13:
                    break; //ДУ-9	уровень отходов в бункере
                case 14:
                    break; //ДУ-11	уровень в РТ
                case 15:
                    break; //ДУ-1	уровень в НЕ
                case 16:
                    break; //ДУ-4	уровень в РЕ
                case 17:
                    ucCyclonAndScrubber1.Level10 = value;
                    break; //ДУ-10	уровень в СБ
                case 18:
                    break; //Г-О2	концентрация газа О2
                case 19:
                    break; //Г-СО	концентрация газа СО
                case 20:
                    break; //Г-О2	концентрация газа О2
                case 21:
                    break; //Г-СО	концентрация газа СО
                case 22:
                    break; //Г-SО2	концентрация газа SО2
                case 23:
                    break; //Г-NО	концентрация газа NО
                case 24:
                    break; //Г-NО2	концентрация газа NО2
            }
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

    }

}