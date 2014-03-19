using System;

namespace UICommon.WPF.Dialogs
{
    public interface IModalWindow<TViewModel>
    {
        bool? DialogResult { get; set; }
        event EventHandler Closed;
        void Show();
        bool? ShowDialog();
        TViewModel ViewModel { get; set; }
        string Title { get; set; }
        void Close();
    }
}
