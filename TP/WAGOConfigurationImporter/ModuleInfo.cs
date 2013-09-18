using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WAGOConfigurationImporter
{
    public enum SignalType
    {
        Analog,
        Descrete
    };

    public enum IOType
    {
        Input,
        Output
    };

    /// <summary>
    /// Класс описывающий положение модулей и состав контроллера
    /// </summary>
    public class ModuleInfo
    {
        public string ModuleName { get; set; }
        public string ModuleNo { get; set; }
        public int PhysicalPosition { get; set; }
        public int ChannelsCount { get; set; }
        public SignalType? SignalType { get; set; }
        public IOType? IoType { get; set; }
    }

    //public class Modules : IEnumerator, IEnumerable
    //{
    //    private ModuleInfo[] modules;
    //    private int position = -1;
    //    public bool MoveNext()
    //    {
    //        position++;
    //        return (position < modules.Length);
    //    }

    //    public void Reset()
    //    {
    //        position = 0;
    //    }

    //    public object Current { get { return modules[position]; } }
    //    public IEnumerator GetEnumerator()
    //    {
    //        return (IEnumerator) this;
    //    }

    //}

}
