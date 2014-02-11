using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc.Ports
{
    ///<summary>
    ///
    ///</summary>
    public class ucDevicePortCombobox:ucPairCombobox
    {
        private FieldBusType _fieldBusType;
        
        [Browsable(false),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IFieldBusFactory FieldBusFactory { get; set; }

        /// <summary>
        /// Создать элементы
        /// </summary>
        /// <returns></returns>
        protected override List<ValueDescriptionPair> CreateItems()
        {
            List<ValueDescriptionPair> list = new List<ValueDescriptionPair>();

            if (FieldBusType!=FieldBusType.Unknown)
            {
                var ports = FieldBusFactory.FindPorts(FieldBusType.RS485);
                if (ports != null)
                    list.AddRange(ports.Select(port => new ValueDescriptionPair(port, port.ToString())));
            }

            return list;
        }

        ///<summary>
        ///
        ///</summary>
        public FieldBusType FieldBusType
        {
            get { return _fieldBusType; }
            set
            {
                if (FieldBusType!=value)
                {
                    _fieldBusType = value;
                    OnFieldBusTypeChanged();
                }
            }
        }

        /// <summary>
        /// Событие смены типа шины
        /// </summary>
        public event EventHandler FieldBusTypeChanged;

        private void OnFieldBusTypeChanged()
        {
            if (FieldBusTypeChanged!=null) FieldBusTypeChanged(this, EventArgs.Empty);
            InitItems();
            Enabled = FieldBusType != FieldBusType.Unknown && DataSource != null && Items.Count > 0;
        }
    }
}