namespace Oleg_ivo.WAGO.Forms
{
    class WriteTypeData : DescriptionAbstract
    {
        public readonly WriteType _writeType;

        public WriteTypeData(WriteType writeType, string description):base(description)
        {
            _writeType = writeType;
        }

        public WriteType WriteType
        {
            get { return _writeType; }
        }
    }
}