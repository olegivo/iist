using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DMS.Common.Messages;
using JulMar.Windows.Mvvm;

namespace TP.WPF.ViewModels
{
    public class DiscreteIndicatorVeiwModel : ViewModel
    {

        private bool indicatorCurrentState;

        public void Init(ChannelRegistrationMessage message)
        {
            //indicatorState=message.
        }
        public double? CurrentValue
        {
            //get { return indicatorCurrentState as double; }
            set
            {
                if (value != null && value >0.9)
                {
                    indicatorCurrentState = true;

                }
                else
                {
                    indicatorCurrentState = false;
                }

            }
        }
    }
}
