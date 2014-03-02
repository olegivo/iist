using System;
using DMS.Common.Messages;

namespace TP.WPF.ViewModels
{
    public class ReheatChamberViewModel : ViewModelBase
    {
        /// <summary>
        /// ��-1	������� � ��
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
        /// ��-4	������� � ��
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
        /// ��-11	������� � ��
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
        /// ����� ������ ������
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
                    //BUG: � 14� ������ ������ ���� ���� �����������, ���� �������! (���������)
                    //-??-Temperature = value * 100;
                    //this.Temperature=value*100;
                    Level_DU11 = value;
                    //ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //��-11	������� � ��
                case 15:
                    Level_DU1 = value;
                    //ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //��-1	������� � ��
                case 16:
                    Level_DU4 = value;
                    //ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //��-4	������� � ��
            }

        }
    }
}