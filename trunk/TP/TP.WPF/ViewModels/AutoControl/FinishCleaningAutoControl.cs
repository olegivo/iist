namespace TP.WPF.ViewModels.AutoControl
{
    /// <summary>
    /// Класс для автоматического управления 
    /// оборотами дымососа и состоянием горелки 
    /// модели представления финишной очистки
    /// </summary>
    class FinishCleaningAutoControl : AutoControl<FinishCleaningViewModel>
    {
        public FinishCleaningAutoControl(FinishCleaningViewModel controlledViewModel)
            : base(controlledViewModel)
        {}

        /// <summary>
        /// Метод, реализующий логику автоматического управления
        /// </summary>
        protected override void DoAutoControl()
        {
            int oborot = 0;
            int gorelka = 0;
            const double deltaV = 10;

            if (ControlledViewModel.GasConcentration_CO > 3000)
            { oborot = oborot + 1; }

            if (ControlledViewModel.GasConcentration_O2 < 5)
            { oborot = oborot + 1; }

            if (ControlledViewModel.GasConcentration_O2 > 21)
            { oborot = oborot - 1; }

            if (ControlledViewModel.GasConcentration_SO2 > 600)
            { oborot = oborot + 1; }

            if (ControlledViewModel.GasConcentration_NO > 700)
            { oborot = oborot + 1; }

            if (ControlledViewModel.GasConcentration_NO2 > 750)
            { oborot = oborot + 1; }

            if (ControlledViewModel.Temperature_TC6 < 120)
            { gorelka = gorelka + 1; }

            if (ControlledViewModel.Temperature_TC6 > 180)
            { gorelka = gorelka - 1; }

            if (ControlledViewModel.Temperature_TC7 < 160)
            { gorelka = gorelka + 1; }

            if (oborot > 0)
            { ControlledViewModel.V = ControlledViewModel.V + deltaV; }

            if (oborot < 0)
            { ControlledViewModel.V = ControlledViewModel.V - deltaV; }

            if (gorelka > 0)
            { ControlledViewModel.BurnerStatus = true; }

            if (gorelka < 0)
            { ControlledViewModel.BurnerStatus = false; }

        }

    }
}