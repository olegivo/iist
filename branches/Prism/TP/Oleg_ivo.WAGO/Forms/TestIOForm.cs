using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Autofac;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.WAGO.Forms
{
    ///<summary>
    ///
    ///</summary>
    public partial class TestIOForm : Form
    {
        private readonly FieldBusNode _fieldBusNode;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusNode"></param>
        /// <param name="context"></param>
        public TestIOForm(FieldBusNode fieldBusNode, IComponentContext context)
        {
            _fieldBusNode = fieldBusNode;
            InitializeComponent();
            plcManager = context.Resolve<IPlcManager>();

            Init();
        }

        ///<summary>
        ///
        ///</summary>
        public IFieldBusNodeAccessor FieldBusNode
        {
            get { return _fieldBusNode; }
        }

        private void btnSyncW2R_Click(object sender, EventArgs e)
        {
            tbReadAddress.Text = tbWriteAddress.Text;
        }

        private void btnSyncR2W_Click(object sender, EventArgs e)
        {
            tbWriteAddress.Text = tbReadAddress.Text;
        }

        ///<summary>
        ///
        ///</summary>
        public ushort ReadAddress
        {
            get { return Convert.ToUInt16(tbReadAddress.Text); }
        }

        ///<summary>
        ///
        ///</summary>
        public ushort WriteAddress
        {
            get { return Convert.ToUInt16(tbWriteAddress.Text); }
        }

        ///<summary>
        ///
        ///</summary>
        public Array ReadValue
        {
            set
            {
                string s = "";
                Int64 readAddress = ReadAddress;
                Int64 increment = (ReadType == ReadType.ReadCoils) ? 1 : 8;
                foreach (object item in value)
                {
                    s += string.Format("јдрес: {0} «начение: {1}{2}", readAddress, Convert.ToInt64(item), Environment.NewLine);
                    readAddress += increment;
                }
                
                tbReadValue.Text = s;
            }
        }

        ///<summary>
        ///
        ///</summary>
        public ushort WriteValue
        {
            get { return Convert.ToUInt16(tbWriteValue.Text); }
        }

        ///<summary>
        ///
        ///</summary>
        public ushort Numbers
        {
            get { return tbNumbers.Text.Length > 0 ? Convert.ToUInt16(tbNumbers.Text) : Convert.ToUInt16(1); }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            ArrayList result = new ArrayList();

            switch (ReadType)
            {
                case ReadType.ReadCoils:
                    result.AddRange(FieldBusNode.ReadCoils(ReadAddress, Numbers));
                    break;
                case ReadType.ReadInputRegisters:
                    result.AddRange(FieldBusNode.ReadInputRegisters(ReadAddress, Numbers));
                    break;
                case ReadType.ReadHoldingRegister:
                    result.AddRange(FieldBusNode.ReadHoldingRegisters(ReadAddress, Numbers));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (result.Count > 0) ReadValue = result.ToArray(result[0].GetType());
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            switch (WriteType)
            {
                case WriteType.WriteSingleCoil:
                    if (WriteCoils != null && WriteCoils.Length>0)
                        FieldBusNode.WriteSingleCoil(WriteAddress, WriteCoils[0]);
                    break;
                case WriteType.WriteCoils:
                    if (WriteCoils != null && WriteCoils.Length > 0)
                        FieldBusNode.WriteMultipleCoils(WriteAddress, WriteCoils);
                    break;
                case WriteType.WriteSingleRegister:
                    if (WriteRegisters != null && WriteRegisters.Length > 0)
                        FieldBusNode.WriteSingleRegister(WriteAddress, WriteRegisters[0]);
                    break;
                case WriteType.WriteRegisters:
                    if (WriteRegisters != null && WriteRegisters.Length > 0)
                        FieldBusNode.WriteMultipleRegisters(WriteAddress, WriteRegisters);
                    break;
            }
        }

        private bool[] WriteCoils
        {
            get
            {
                List<bool> list = new List<bool>();
                foreach (string s in tbWriteValue.Text.Split(Environment.NewLine.ToCharArray()))
                {
                    int b;
                    if (int.TryParse(s, out b) && (b == 0 || b == 1)) list.Add(b == 1);
                }
                return list.ToArray();
            }
        }

        private ushort[] WriteRegisters
        {
            get
            {
                List<ushort> list = new List<ushort>();
                foreach (string s in tbWriteValue.Text.Split(Environment.NewLine.ToCharArray()))
                {
                    ushort b;
                    if (ushort.TryParse(s, out b)) list.Add(b);
                }
                return list.ToArray();
            }
        }

        private void Init()
        {
            statusText.Text = string.Format("{0}. {1}", FieldBusNode.FieldBusAccessor,
                               FieldBusNode.FieldBusNodeAddress);

            List<ReadTypeData> readList = new List<ReadTypeData>();
            readList.AddRange(new ReadTypeData[]
                                  {
                                      new ReadTypeData(ReadType.ReadCoils, "ячейки"), 
                                      new ReadTypeData(ReadType.ReadHoldingRegister, "–егистры"), 
                                      new ReadTypeData(ReadType.ReadInputRegisters, "¬ходные регистры") 
                                    });

            cbReadType.DataSource = readList;
            cbReadType.DisplayMember = "Description";
            cbReadType.ValueMember = "ReadType";

            List<WriteTypeData> writeList = new List<WriteTypeData>();
            writeList.AddRange(new WriteTypeData[]
                                  {
                                      new WriteTypeData(WriteType.WriteSingleCoil, "ќдну €чейку"), 
                                      new WriteTypeData(WriteType.WriteCoils, "ячейки"),
                                      new WriteTypeData(WriteType.WriteSingleRegister, "ќдин регистр"),
                                      new WriteTypeData(WriteType.WriteRegisters, "–егистры")
                                    });

            cbWriteType.DataSource = writeList;
            cbWriteType.DisplayMember = "Description";
            cbWriteType.ValueMember = "WriteType";
        }

        private ReadType ReadType
        {
            get { return (ReadType) cbReadType.SelectedValue; }
        }

        private WriteType WriteType
        {
            get { return (WriteType)cbWriteType.SelectedValue; }
        }

    }


    enum WriteType
    {
        [Description("WriteSingleCoil")]
        WriteSingleCoil = 1,
        [Description("WriteCoils")]
        WriteCoils = 2,
        [Description("WriteSingleRegister")]
        WriteSingleRegister = 3,
        [Description("WriteRegisters")]
        WriteRegisters = 4
    }
}