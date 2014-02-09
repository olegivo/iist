using System;
using System.Collections.Generic;
using DMS.Common.Events;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.Devices.Modules;
using Oleg_ivo.Plc.Entities;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;
using Oleg_ivo.WAGO;
using Oleg_ivo.WAGO.Devices;
using Oleg_ivo.WAGO.Factory;
using Oleg_ivo.WAGO.Meta;
using FieldBusNode = Oleg_ivo.Plc.FieldBus.FieldBusNodes.FieldBusNode;
using LogicalChannel = Oleg_ivo.Plc.Channels.LogicalChannel;
using PhysicalChannel = Oleg_ivo.Plc.Channels.PhysicalChannel;

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
                ControlManagementUnit.HasWriteChannel += ControlManagementUnit_HasWriteChannel;//TODO:отписка от события
            }
        }

        private void ControlManagementUnit_HasWriteChannel(object sender, DataEventArgs e)
        {
            SetManagedValue(e.Message.LogicalChannelId, e.Message.Value);
        }

        private readonly Temperature T6;
        private readonly Temperature T7;
        private readonly Speed Speed;
        private readonly GasConcentration COConcentration;
        private readonly GasConcentration O2Concentration;
        private readonly GasConcentration SО2Concentration;
        private readonly GasConcentration NOConcentration;
        private readonly GasConcentration NO2Concentration;
        private ILogicalChannelsFactory logicalChannelFactory;
        private IDistributedMeasurementInformationSystem dmis;

        /// <summary>
        /// 
        /// </summary>
        public Emulator(ILogicalChannelsFactory logicalChannelFactory, IDistributedMeasurementInformationSystem dmis)
        {
            this.dmis = Enforce.ArgumentNotNull(dmis,"dmis");
            this.logicalChannelFactory = Enforce.ArgumentNotNull(logicalChannelFactory, "logicalChannelFactory");
            T6 = new Temperature
                                 {
                                     GetTemperature =
                                     passedSeconds =>
                                     Math.Abs((Math.Sin(0.01 * passedSeconds)) * 100 + 130)
                                 };
            T7 = new Temperature
                                {
                                    GetTemperature =
                                    passedSeconds =>
                                    Math.Abs((Math.Sin(0.01 * passedSeconds)) * 100 + 150)
                                };

            Speed = new Speed();

            COConcentration = new GasConcentration
                                  {
                                      GetTemperature = () => T6.GetOutputValue(),
                                      GetSpeed = () => Speed.GetOutputValue(),
                                      GetConcentration =
                                          (speed, temperature, passedSeconds) =>
                                          Math.Abs(Math.Sin(0.011*passedSeconds))*3000 + 1000
                                          + (temperature > 150 ? (30*temperature - 4500) : 0)
                                          + (-31.25*speed + 1250)
                                  };

            O2Concentration = new GasConcentration
                                  {
                                      GetTemperature = () => T6.GetOutputValue(),
                                      GetSpeed = () => Speed.GetOutputValue(),
                                      GetConcentration =
                                          (speed, temperature, passedSeconds) =>
                                          (Math.Cos(0.021 * passedSeconds)) * 10 + 13
                                          + (0.183 * temperature - 25)
                                          + (0.833 * speed - 21.33)
                                  };

            SО2Concentration = new GasConcentration
                                   {
                                       GetTemperature = () => T6.GetOutputValue(),
                                       GetSpeed = () => Speed.GetOutputValue(),
                                       GetConcentration =
                                          (speed, temperature, passedSeconds) =>
                                          (Math.Sin(0.010 * passedSeconds)) * 500 + 200
                                          + (8.33 * temperature - 1300)
                                          + (-16.67 * speed + 366.67)
                                   };

            NOConcentration = new GasConcentration
                                  {
                                      GetTemperature = () => T6.GetOutputValue(),
                                      GetSpeed = () => Speed.GetOutputValue(),
                                      GetConcentration =
                                          (speed, temperature, passedSeconds) =>
                                          (Math.Sin(0.01 * passedSeconds)) * 500 + 300
                                          + (10 * temperature - 900)
                                          + (-20 * speed + 500)
                                  };

            NO2Concentration = new GasConcentration
                                   {
                                       GetTemperature = () => T6.GetOutputValue(),
                                       GetSpeed = () => Speed.GetOutputValue(),
                                       GetConcentration =
                                          (speed, temperature, passedSeconds) =>
                                          (Math.Sin(0.011 * passedSeconds)) * 300 + 450
                                          + (10 * temperature - 1050)
                                          + (-23.3 * speed + 533.3)
                                   };
        }

        private void InitChannels()
        {
            if (ControlManagementUnit != null)
            {
                var logicalChannels = new List<LogicalChannel>();
                //TODO:контролируемые параметры
                //throw new NotImplementedException("Для построения каналов необходимо вызвать конструктор, содержащий ФК, указывающий на конкретный режим работы его IoModule (IsInput)");
                var fieldBusNode = new FieldBusNode(
                    new FieldBusManager(new FieldBus(), dmis),
                    new Oleg_ivo.Plc.Entities.FieldBusNode());
                var inputPhysicalChannel = new PhysicalChannel(null, 
                    logicalChannelFactory, 
                    fieldBusNode, 
                    new WagoIOModule(logicalChannelFactory)
                    {
                        Meta = new WagoIOModuleMeta(true, false, true, false, 0, 0, 0)
                    }, 
                    0, 
                    0);
                logicalChannels.Add(new LogicalChannel(inputPhysicalChannel, null, 0, 0)
                {
                    Id = 6,//TС6	температура перед рукавным фильтром
                    Description = "Температура перед фильтром",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = T6.GetOutputValue
                    
                });
                logicalChannels.Add(new LogicalChannel(inputPhysicalChannel, null, 0, 0)
                {
                    Id = 7,//TС7	температура перед дымососом
                    Description = "Температура перед дымососом",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = T7.GetOutputValue

                });
                logicalChannels.Add(new LogicalChannel(inputPhysicalChannel, null, 0, 0)
                {
                    Id = 20,//Г-О2	концентрация газа О2
                    Description = "Концентрация О2",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = O2Concentration.GetOutputValue
                });
                logicalChannels.Add(new LogicalChannel(inputPhysicalChannel, null, 0, 0)
                {
                    Id = 21,//Г-СО	концентрация газа СО
                    Description = "Концентрация СО",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = COConcentration.GetOutputValue
                });
                logicalChannels.Add(new LogicalChannel(inputPhysicalChannel, null, 0, 0)
                {
                    Id = 22,//Г-SО2	концентрация газа SО2
                    Description = "Концентрация SО2",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = SО2Concentration.GetOutputValue
                });
                logicalChannels.Add(new LogicalChannel(inputPhysicalChannel, null, 0, 0)
                {
                    Id = 23,//Г-NO	концентрация газа NO
                    Description = "Концентрация NO",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = NOConcentration.GetOutputValue
                });
                logicalChannels.Add(new LogicalChannel(inputPhysicalChannel, null, 0, 0)
                {
                    Id = 24,//Г-NO2	концентрация газа NO2
                    Description = "Концентрация NO2",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = NO2Concentration.GetOutputValue
                });
                //TODO: управляемые параметры
                //throw new NotImplementedException("Для построения каналов необходимо вызвать конструктор, содержащий ФК, указывающий на конкретный режим работы его IoModule (IsOutput)");
                PhysicalChannel outputPhysicalChannel = new PhysicalChannel(null,
                    logicalChannelFactory,
                    fieldBusNode,
                    new WagoIOModule(logicalChannelFactory)
                    {
                        Meta = new WagoIOModuleMeta(true, false, false, true, 0, 0, 0)
                    },
                    0,
                    0);
                logicalChannels.Add(new LogicalChannel(outputPhysicalChannel, null, 0, 0)
                {
                    Id = 10001,
                    Description = "Горелка",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000
                });
                logicalChannels.Add(new LogicalChannel(outputPhysicalChannel, null, 0, 0)
                {
                    Id = 10002,
                    Description = "Количество оборотов дымососа",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000
                });

                foreach (var logicalChannel in logicalChannels) logicalChannel.IsEmulationMode = true;
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
                    return T6.GetOutputValue();//Температура Т6
                case 2:
                    return T7.GetOutputValue();//Температура Т7
                case 3:
                    return COConcentration.GetOutputValue();//Концентрация CO
                case 4:
                    return O2Concentration.GetOutputValue();//Концентрация O2
                case 5:
                    return SО2Concentration.GetOutputValue();//Концентрация SО2
                case 6:
                    return NOConcentration.GetOutputValue();//Концентрация NO
                case 7:
                    return NO2Concentration.GetOutputValue();//Концентрация NO2
                default:
                    throw new ArgumentOutOfRangeException("logicalChannelId", logicalChannelId, "Неожиданное значение номера логического канала");
            }
        }
    }
}