using System;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using Oleg_ivo.Tools.ExceptionCatcher;
using TP;

namespace WindowsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#pragma warning disable 168
            ExceptionHandler exceptionHandler = new ExceptionHandler();
#pragma warning restore 168

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");

            //Application.Run(new frmTest());
            Application.Run(new frmTP());
            //Application.Run(new Form1());
        }
    }
}