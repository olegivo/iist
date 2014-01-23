using System;
using DMS.Common.Messages;

namespace TP.WPF.ViewModels
{
    public class DrumTypeFurnaceViewModel : ViewModelBase
    {
        /// <summary>
        /// TП1	температура в циклонной вихревой топке
        /// </summary>
        private double temperature1;
        public double Temperature_TC1
        {
            get { return temperature1; }
            set
            {
                if (temperature1 != value)
                {
                    temperature1 = value;
                    OnPropertyChanged("Temperature_TC1");
                }
            }
        }
        /// <summary>
        /// TП2	температура в загрузочной системе
        /// </summary>
        private double temperature2;
        public double Temperature_TC2
        {
            get { return temperature2; }
            set
            {
                if (temperature2 != value)
                {
                    temperature2 = value;
                    OnPropertyChanged("Temperature_TC2");
                }
            }
        }
        /// <summary>
        /// S	скорость вращения печи
        /// </summary>
        private double speed;
        public double Speed_S
        {
            get { return speed; }
            set
            {
                if (speed != value)
                {
                    speed = value;
                    OnPropertyChanged("Speed_S");
                }
            }
        }
        /// <summary>
        /// TС8	температура воды в системе охлаждения
        /// </summary>
        private double temperature8;
        public double Temperature_TC8
        {
            get { return temperature8; }
            set
            {
                if (temperature8 != value)
                {
                    temperature8 = value;
                    OnPropertyChanged("Temperature_TC8");
                }
            }
        }
        /// <summary>
        /// ДУ-9	уровень отходов в бункере
        /// </summary>
        private double level9;
        public double Level_DU9
        {
            get { return level9; }
            set
            {
                if (level9 != value)
                {
                    level9 = value;
                    OnPropertyChanged("Level_DU9");
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
            double value = Convert.ToDouble(message.Value);
            int channelId = message.LogicalChannelId;

            switch (channelId)
            {
                case 1:
                    Temperature_TC1 = value;
                    break; //TП1	температура в циклонной вихревой топке
                case 2:
                    Temperature_TC2 = value;
                    break; //TП2	температура в загрузочной системе
                case 8:
                    Temperature_TC8 = value;
                    break; //TС8	температура воды в системе охлаждения
                case 12:
                    Speed_S = value;
                    break; //S	скорость вращения печи
                case 13:
                    Level_DU9 = value;
                    //        ucChart1.AddDataChart(channelId, Convert.ToInt32(value));
                    break; //ДУ-9	уровень отходов в бункере
            }
        }
    }
}
