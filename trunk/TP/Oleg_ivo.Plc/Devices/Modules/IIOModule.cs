namespace Oleg_ivo.Plc.Devices.Modules
{
    ///<summary>
    /// Интерфейс модуля ввода-вывода
    ///</summary>
    public interface IIOModule
    {
        ///<summary>
        /// Разрядность
        ///</summary>
        ushort Size { get; }

        /// <summary>
        /// Модуль аналоговый
        /// </summary>
        bool IsAnalog { get; }

        /// <summary>
        /// Модуль дискретный
        /// </summary>
        bool IsDiscrete { get; }

        /// <summary>
        /// Модуль ввода
        /// </summary>
        bool IsInput { get; }

        /// <summary>
        /// Модуль вывода
        /// </summary>
        bool IsOutput { get; }
    }
}