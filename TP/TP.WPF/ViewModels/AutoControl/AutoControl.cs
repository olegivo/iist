using System.Timers;

namespace TP.WPF.ViewModels.AutoControl
{
    /// <summary>
    /// Класс для автоматического управления свойствами модели представления
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    public abstract class AutoControl<VM> where VM : ViewModelBase
    {
        protected VM ControlledViewModel { get; set; }
        private readonly Timer tmrAutoControlMode;
        private bool isAutomaticControl;

        protected AutoControl(VM controlledViewModel)
        {
            ControlledViewModel = controlledViewModel;
            tmrAutoControlMode = new Timer { Interval = 5000 };
            tmrAutoControlMode.Elapsed += tmrAutoControlMode_Elapsed;
        }

        private void tmrAutoControlMode_Elapsed(object sender, ElapsedEventArgs e)
        {
            DoAutoControl();
        }

        /// <summary>
        /// Метод, реализующий логику автоматического управления
        /// </summary>
        protected abstract void DoAutoControl();

        public bool IsAutomaticControl
        {
            get { return isAutomaticControl; }
            set
            {
                if (isAutomaticControl != value)
                {
                    isAutomaticControl = value;
                    ApplyAutoControlMode();
                }
            }
        }

        private void ApplyAutoControlMode()
        {
            if (IsAutomaticControl)
                tmrAutoControlMode.Start();
            else
                tmrAutoControlMode.Stop();
        }
    }
}