using System;
using System.Collections.Generic;
using DMS.Common.Events;
using Oleg_ivo.Plc.Channels;

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
                ControlManagementUnit.HasWriteChannel += ControlManagementUnit_HasWriteChannel;
            }
        }

        private void ControlManagementUnit_HasWriteChannel(object sender, DataEventArgs e)
        {
            SetManagedValue(e.Message.LogicalChannelId, e.Message.Value);
        }

        private Temperature T6;
        private Temperature T7;
        private Speed Speed;
        private GasConcentration COConcentration;
        private GasConcentration O2Concentration;
        private GasConcentration S�2Concentration;
        private GasConcentration NOConcentration;
        private GasConcentration NO2Concentration;

        /// <summary>
        /// 
        /// </summary>
        public Emulator()
        {
            T6 = new Temperature();
            T7 = new Temperature();
            Speed = new Speed();

            COConcentration = new GasConcentration
                                  {
                                      GetTemperature = () => T6.GetOutputValue(),
                                      GetSpeed = () => Speed.GetOutputValue(),
                                      GetConcentration =
                                          (speed, temperature, passedSeconds) =>
                                          Math.Abs(Math.Sin(0.005*passedSeconds))*50 + 3500
                                          + (temperature > 150 ? (30*temperature - 4500) : 0)
                                          + (-31.25*speed + 1250)
                                  };

            O2Concentration = new GasConcentration
                                  {
                                      GetTemperature = () => T6.GetOutputValue(),
                                      GetSpeed = () => Speed.GetOutputValue()
                                  };

            S�2Concentration = new GasConcentration
                                   {
                                       GetTemperature = () => T6.GetOutputValue(),
                                       GetSpeed = () => Speed.GetOutputValue()
                                   };

            NOConcentration = new GasConcentration
                                  {
                                      GetTemperature = () => T6.GetOutputValue(),
                                      GetSpeed = () => Speed.GetOutputValue()
                                  };

            NO2Concentration = new GasConcentration
                                   {
                                       GetTemperature = () => T6.GetOutputValue(),
                                       GetSpeed = () => Speed.GetOutputValue()
                                   };
        }

        private void InitChannels()
        {
            if (ControlManagementUnit != null)
            {
                List<LogicalChannel> logicalChannels = new List<LogicalChannel>();
                //�������������� ���������
                logicalChannels.Add(new InputLogicalChannel(null, 0, 0)
                {
                    Id = 6,//T�6	����������� ����� �������� ��������
                    Description = "����������� ����� ��������",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = T6.GetOutputValue
                    
                });
                logicalChannels.Add(new InputLogicalChannel(null, 0, 0)
                {
                    Id = 7,//T�7	����������� ����� ���������
                    Description = "����������� ����� ���������",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = T7.GetOutputValue

                });
                logicalChannels.Add(new InputLogicalChannel(null, 0, 0)
                {
                    Id = 20,//�-�2	������������ ���� �2
                    Description = "������������ �2",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = O2Concentration.GetOutputValue
                });
                logicalChannels.Add(new InputLogicalChannel(null, 0, 0)
                {
                    Id = 21,//�-��	������������ ���� ��
                    Description = "������������ ��",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = COConcentration.GetOutputValue
                });
                logicalChannels.Add(new InputLogicalChannel(null, 0, 0)
                {
                    Id = 22,//�-S�2	������������ ���� S�2
                    Description = "������������ S�2",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = S�2Concentration.GetOutputValue
                });
                logicalChannels.Add(new InputLogicalChannel(null, 0, 0)
                {
                    Id = 23,//�-NO	������������ ���� NO
                    Description = "������������ NO",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = NOConcentration.GetOutputValue
                });
                logicalChannels.Add(new InputLogicalChannel(null, 0, 0)
                {
                    Id = 24,//�-NO2	������������ ���� NO2
                    Description = "������������ NO2",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = NO2Concentration.GetOutputValue
                });
                //����������� ���������
                logicalChannels.Add(new OutputLogicalChannel(null, 0, 0)
                {
                    Id = 10001,
                    Description = "�������",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000
                });
                logicalChannels.Add(new OutputLogicalChannel(null, 0, 0)
                {
                    Id = 10002,
                    Description = "���������� �������� ��������",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000
                });

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