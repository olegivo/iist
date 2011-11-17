using System;
using System.Collections.Generic;
using DMS.Common.Events;
using Oleg_ivo.Plc.Channels;

namespace EmulationClient.Emulation
{
    /// <summary>
    /// Эмулятор
    /// </summary>
    public class Emulator
    {
        private ControlManagementUnitEmulation _controlManagementUnit;
       
        /// <summary>
        /// Модуль контроля и управления
        /// </summary>
        public ControlManagementUnitEmulation ControlManagementUnit
        {
            get { return _controlManagementUnit; }
            set
            {
                _controlManagementUnit = value;
                InitChannels();
                ControlManagementUnit.HasWriteChannel += ControlManagementUnit_HasWriteChannel;
            }
        }

        private void ControlManagementUnit_HasWriteChannel(object sender, DataEventArgs e)
        {
            SetManagedValue(e.Message.LogicalChannelId, e.Message.Value);
        }

        private Temperature T6;
        private Speed Speed;
        private GasConcentration COConcentration;

        /// <summary>
        /// 
        /// </summary>
        public Emulator()
        {
            T6 = new Temperature();
            Speed = new Speed();

            COConcentration = new GasConcentration();
            COConcentration.GetTemperature = new GetDoubleValueDelegate(() => T6.GetOutputValue());
            COConcentration.GetSpeed = new GetDoubleValueDelegate(() => Speed.GetOutputValue());
        }

        private void InitChannels()
        {
            if (ControlManagementUnit != null)
            {
                List<LogicalChannel> logicalChannels = new List<LogicalChannel>();
                //контролируемые параметры
                logicalChannels.Add(new InputLogicalChannel(null, 0, 0)
                {
                    Id = 1,
                    Description = "Температура",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = T6.GetOutputValue
                    
                });
                logicalChannels.Add(new InputLogicalChannel(null, 0, 0)
                {
                    Id = 2,
                    Description = "Концентрация",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = COConcentration.GetOutputValue
                });

                //управляемые параметры
                logicalChannels.Add(new OutputLogicalChannel(null, 0, 0)
                {
                    Id = 10001,
                    Description = "Горелка",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000
                });
                logicalChannels.Add(new OutputLogicalChannel(null, 0, 0)
                {
                    Id = 10002,
                    Description = "Количество оборотов дымососа",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000
                });

                ControlManagementUnit.LogicalChannels = logicalChannels;
                
            }
        }


        /// <summary>
        /// Установка значения управляемой величины
        /// </summary>
        /// <param name="logicalChannelId"></param>
        /// <param name="value"></param>
        public void SetManagedValue(int logicalChannelId, object value)
        {
            switch (logicalChannelId)
            {
                case 10001:
                    T6.IsBurnerOn = (bool)value;//Горелка
                    break;
                case 10002:
                    Speed.SetSpeedValue((double)value);//Количество оборотов дымососа
                    break;
                default:
                    throw new ArgumentOutOfRangeException("logicalChannelId", logicalChannelId, "Неожиданное значение номера логического канала");
            }
        }

        /// <summary>
        /// Чтение значения контролируемого параметра
        /// </summary>
        /// <param name="logicalChannelId"></param>
        public double GetControlledParameterValue(int logicalChannelId)
        {
            switch (logicalChannelId)
            {
                case 1:
                    return T6.GetOutputValue();//Температура
                case 2:
                    return COConcentration.GetOutputValue();//Концентрация
                default:
                    throw new ArgumentOutOfRangeException("logicalChannelId", logicalChannelId, "Неожиданное значение номера логического канала");
            }
        }
    }
}