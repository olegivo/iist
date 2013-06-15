namespace Oleg_ivo.WAGO.Forms
{
    class DescriptionAbstract
    {
        private readonly string _description;

        public DescriptionAbstract(string description)
        {
            _description = description;
        }

        public string Description
        {
            get { return _description; }
        }
    }
}