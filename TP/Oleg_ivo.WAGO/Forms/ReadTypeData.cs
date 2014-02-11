namespace Oleg_ivo.WAGO.Forms
{
    class ReadTypeData : DescriptionAbstract
    {
        public readonly ReadType _readType;

        public ReadTypeData(ReadType readType, string description):base(description)
        {
            _readType = readType;
        }

        public ReadType ReadType
        {
            get { return _readType; }
        }
    }
}