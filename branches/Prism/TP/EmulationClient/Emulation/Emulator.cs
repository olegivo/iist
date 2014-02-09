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
    /// ��������
    /// </summary>
    public class Emulator
    {
        private ControlManagementUnitEmulation _controlManagementUnit;
       
        /// <summary>
        /// ������ �������� � ����������
        /// </summary>
        public ControlManagementUnitEmulation ControlManagementUnit
        {
            get { return _controlManagementUnit; }
            set
            {
                _controlManagementUnit = value;
                InitChannels();
                ControlManagementUnit.HasWriteChannel += ControlManagementUnit_HasWriteChannel;//TODO:������� �� �������
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
        private readonly GasConcentration S�2Concentration;
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

            S�2Concentration = new GasConcentration
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
                //TODO:�������������� ���������
                //throw new NotImplementedException("��� ���������� ������� ���������� ������� �����������, ���������� ��, ����������� �� ���������� ����� ������ ��� IoModule (IsInput)");
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
                    Id = 6,//T�6	����������� ����� �������� ��������
                    Description = "����������� ����� ��������",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = T6.GetOutputValue
                    
                });
                logicalChannels.Add(new LogicalChannel(inputPhysicalChannel, null, 0, 0)
                {
                    Id = 7,//T�7	����������� ����� ���������
                    Description = "����������� ����� ���������",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = T7.GetOutputValue

                });
                logicalChannels.Add(new LogicalChannel(inputPhysicalChannel, null, 0, 0)
                {
                    Id = 20,//�-�2	������������ ���� �2
                    Description = "������������ �2",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = O2Concentration.GetOutputValue
                });
                logicalChannels.Add(new LogicalChannel(inputPhysicalChannel, null, 0, 0)
                {
                    Id = 21,//�-��	������������ ���� ��
                    Description = "������������ ��",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = COConcentration.GetOutputValue
                });
                logicalChannels.Add(new LogicalChannel(inputPhysicalChannel, null, 0, 0)
                {
                    Id = 22,//�-S�2	������������ ���� S�2
                    Description = "������������ S�2",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = S�2Concentration.GetOutputValue
                });
                logicalChannels.Add(new LogicalChannel(inputPhysicalChannel, null, 0, 0)
                {
                    Id = 23,//�-NO	������������ ���� NO
                    Description = "������������ NO",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = NOConcentration.GetOutputValue
                });
                logicalChannels.Add(new LogicalChannel(inputPhysicalChannel, null, 0, 0)
                {
                    Id = 24,//�-NO2	������������ ���� NO2
                    Description = "������������ NO2",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = NO2Concentration.GetOutputValue
                });
                //TODO: ����������� ���������
                //throw new NotImplementedException("��� ���������� ������� ���������� ������� �����������, ���������� ��, ����������� �� ���������� ����� ������ ��� IoModule (IsOutput)");
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
                    Description = "�������",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000
                });
                logicalChannels.Add(new LogicalChannel(outputPhysicalChannel, null, 0, 0)
                {
                    Id = 10002,
                    Description = "���������� �������� ��������",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000
                });

                foreach (var logicalChannel in logicalChannels) logicalChannel.IsEmulationMode = true;
                ControlManagementUnit.LogicalChannels = logicalChannels;
            }
        }


        /// <summary>
        /// ��������� �������� ����������� ��������
        /// </summary>
        /// <param name="logicalChannelId"></param>
        /// <param name="value"></param>
        public void SetManagedValue(int logicalChannelId, object value)
        {
            switch (logicalChannelId)
            {
                case 10001:
                    T6.IsBurnerOn = (bool)value;//�������
                    break;
                case 10002:
                    Speed.SetSpeedValue((double)value);//���������� �������� ��������
                    break;
                default:
                    throw new ArgumentOutOfRangeException("logicalChannelId", logicalChannelId, "����������� �������� ������ ����������� ������");
            }
        }

        /// <summary>
        /// ������ �������� ��������������� ���������
        /// </summary>
        /// <param name="logicalChannelId"></param>
        public double GetControlledParameterValue(int logicalChannelId)
        {
            switch (logicalChannelId)
            {
                case 1:
                    return T6.GetOutputValue();//����������� �6
                case 2:
                    return T7.GetOutputValue();//����������� �7
                case 3:
                    return COConcentration.GetOutputValue();//������������ CO
                case 4:
                    return O2Concentration.GetOutputValue();//������������ O2
                case 5:
                    return S�2Concentration.GetOutputValue();//������������ S�2
                case 6:
                    return NOConcentration.GetOutputValue();//������������ NO
                case 7:
                    return NO2Concentration.GetOutputValue();//������������ NO2
                default:
                    throw new ArgumentOutOfRangeException("logicalChannelId", logicalChannelId, "����������� �������� ������ ����������� ������");
            }
        }
    }
}