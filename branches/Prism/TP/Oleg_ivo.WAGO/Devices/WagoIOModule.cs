using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Devices.Modules;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.WAGO.Meta;

namespace Oleg_ivo.WAGO.Devices
{
    ///<summary>
    /// ������ �����-������ WAGO
    ///</summary>
    public class WagoIOModule : IOModule, IWagoMetaContainer
    {

        #region fields
        private WagoIOModuleMeta _meta;

        #endregion

        public WagoIOModule(ILogicalChannelsFactory logicalChannelsFactory) : base(logicalChannelsFactory)
        {
        }

        #region properties

        ///<summary>
        /// ����-��������
        ///</summary>
        public WagoMeta Meta
        {
            get { return _meta; }
            set { _meta = value as WagoIOModuleMeta; }
        }

        ///<summary>
        /// ����-�������� ������
        ///</summary>
        public WagoIOModuleMeta ModuleMeta
        {
            get { return Meta as WagoIOModuleMeta; }
        }

        #endregion

        #region constructors

        #endregion

        ///<summary>
        ///Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</summary>
        ///
        ///<returns>
        ///A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("{0}", Meta);
        }

        ///<summary>
        /// ����������� ������
        ///</summary>
        public override ushort Size
        {
            get { return ModuleMeta !=null ? ModuleMeta.Size : (ushort) 0; }
        }

        ///<summary>
        /// ����� �� ���� ��� ������
        ///</summary>
        public override int WriteAddress
        {
            get { return ModuleMeta.WriteAddress; }
            set { ModuleMeta.WriteAddress = value; }
        }

        ///<summary>
        /// ����� �� ���� ��� ������
        ///</summary>
        public override int ReadAddress
        {
            get { return ModuleMeta.ReadAddress; }
            set { ModuleMeta.ReadAddress = (ushort) value; }
        }

        /// <summary>
        /// ������ ����������
        /// </summary>
        public override bool IsAnalog
        {
            get { return ModuleMeta != null ? ModuleMeta.IsAnalog : false; }
        }

        /// <summary>
        /// ������ ����������
        /// </summary>
        public override bool IsDiscrete
        {
            get { return ModuleMeta != null ? ModuleMeta.IsDiscrete : false; }
        }

        ///<summary>
        /// ������ �����
        ///</summary>
        public override bool IsInput
        {
            get { return ModuleMeta != null ? ModuleMeta.IsInput : false; }
        }

        /// <summary>
        /// ������ ������
        /// </summary>
        public override bool IsOutput
        {
            get { return ModuleMeta != null && ModuleMeta.IsOutput; }
        }

        /// <summary>
        /// ��������� ���������� ������ �� ��������� ��� ������� ����������� ������
        /// </summary>
        public override LogicalChannelCollection BuildDefaultLogicalChannels()
        {
            return LogicalChannelsFactory.BuildDefaultLogicalChannels(PhysicalChannel);
        }
    }
}