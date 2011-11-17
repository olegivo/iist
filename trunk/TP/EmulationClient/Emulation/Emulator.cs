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
                //�������������� ���������
                logicalChannels.Add(new InputLogicalChannel(null, 0, 0)
                {
                    Id = 1,
                    Description = "�����������",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = T6.GetOutputValue
                    
                });
                logicalChannels.Add(new InputLogicalChannel(null, 0, 0)
                {
                    Id = 2,
                    Description = "������������",
                    PollPeriod = TimeSpan.FromMilliseconds(500),
                    MinValue = 0,
                    MaxValue = 1000,
                    GetValueEmulationAltDelegate = COConcentration.GetOutputValue
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
                    return T6.GetOutputValue();//�����������
                case 2:
                    return COConcentration.GetOutputValue();//������������
                default:
                    throw new ArgumentOutOfRangeException("logicalChannelId", logicalChannelId, "����������� �������� ������ ����������� ������");
            }
        }
    }
}