using System;
using DMS.Common.Messages;

namespace TP.WPF.ViewModels
{
    public class ReheatChamberViewModel : ViewModelBase
    {
        /// <summary>
        /// ДУ-1	уровень в НЕ
        /// </summary>
        private double level1;
        public double Level_DU1
        {
            get { return level1; }
            set
            {
                if (level1 != value)
                {
                    level1 = value;
                    RaisePropertyChanged("Level_DU1");
                }
            }
        }
        /// <summary>
        /// ДУ-4	уровень в РЕ
        /// </summary>
        private double level4;
        public double Level_DU4
        {
            get { return level4; }
            set
            {
                if (level4 != value)
                {
                    level4 = value;
                    RaisePropertyChanged("Level_DU4");
                }
            }
        }

        /// <summary>
        /// ДУ-11	уровень в РТ
        /// </summary>
        private double level11;
        public double Level_DU11
        {
            get { return level11; }
            set
            {
                if (level11 != value)
                {
                    level11 = value;
                    RaisePropertyChanged("Level_DU11");
                }
            }
        }


        /// <summary>
        /// После чтения канала
        /// </summary>
        /// <param name="message"></param>
        public override void OnReadChannel(InternalLogicalChannelDataMessage message)
        {
            base.OnReadChannel(message);

            var value = Convert.ToDouble(message.Value);
            var channelId = message.LogicalChannelId;

            switch (channelId)
            {
                case 14:
                    //BUG: В 14й канале должна быть ЛИБО температура, ЛИБО уровень! (проверить)
                    //-??-Temperature = value * 100;
                    //this.Temperature=value*100;
                    Level_DU11 = value;
                    //ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //ДУ-11	уровень в РТ
                case 15:
                    Level_DU1 = value;
                    //ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //ДУ-1	уровень в НЕ
                case 16:
                    Level_DU4 = value;
                    //ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //ДУ-4	уровень в РЕ
            }

        }
    }
}