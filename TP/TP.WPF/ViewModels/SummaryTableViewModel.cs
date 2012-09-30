using System;
using System.Collections.Generic;
using JulMar.Windows.Mvvm;
using System.Linq;
using System.Text;

namespace TP.WPF.ViewModels
{

    public class SummaryTableViewModel
    {
        private Dictionary<int, float> _summaryTable = new Dictionary<int, float>();
        
        public void Add(int chanalNumber, float chanelValue)
        {
            _summaryTable.Add(chanalNumber,chanelValue);
        }
 
    
    }

}
