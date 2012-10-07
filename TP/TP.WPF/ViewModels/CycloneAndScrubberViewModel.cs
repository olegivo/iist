using System;
using DMS.Common.Messages;

namespace TP.WPF.ViewModels
{
    public class CycloneAndScrubberViewModel : ViewModelBase
    {
        /// <summary>
        /// рН1	уровень рН в СФ1
        /// </summary>
        private double phLevel1;
        public double PhLevel_CF1
        {
            get { return phLevel1; }
            set
            {
                if (phLevel1 != value)
                {
                    phLevel1 = value;
                    OnPropertyChanged("PhLevel_CF1");
                }
            }
        }

        /// <summary>
        /// рН2	уровень рН в СФ2
        /// </summary>
        private double phLevel2;
        public double PhLevel_CF2
        {
            get { return phLevel2; }
            set
            {
                if (phLevel2 != value)
                {
                    phLevel2 = value;
                    OnPropertyChanged("PhLevel_CF2");
                }
            }
        }
        /// <summary>
        /// ДУ-10	уровень в СБ
        /// </summary>
        private double level10;
        public double Level_DU10
        {
            get { return level10; }
            set
            {
                if (level10 != value)
                {
                    level10 = value;
                    OnPropertyChanged("Level_DU10");
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
                case 10:
                    PhLevel_CF1 = value;
                    break; //рН1	уровень рН в СФ1
                case 11:
                    PhLevel_CF2 = value;
                    break; //рН2	уровень рН в СФ2
                case 17:
                    Level_DU10 = value;
                    //        ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //ДУ-10	уровень в СБ
            }
        }
    }
}
