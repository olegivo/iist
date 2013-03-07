using Oleg_ivo.Plc;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.WAGO.Factory;


namespace Oleg_ivo.WAGO
{
    ///<summary>
    /// Настройки распределённой системы
    ///</summary>
    public class DistributedSystemSettings : DistributedSystemSettingsBase
    {
        ///<summary>
        ///
        ///</summary>
        protected override void InitLoadOptions()
        {
            base.InitLoadOptions();

            FieldBusDAC fieldBusDAC = new FieldBusDAC();
            //RS-485
            var fieldBusType = FieldBusType.RS485;
            var fieldBusLoadOptions = new FieldBusLoadOptions(fieldBusType)
                                            {
                                              FieldBusNodeAddresses =
                                                  fieldBusDAC.GetAddresses(fieldBusType),
                                              FieldNodesLevel =
                                                  new MeasurementSystemLevelLoadOptions
                                                      {
                                                          ComputeCurrentConfiguration = false,
                                                          LoadSavedConfiguration = true
                                                      },
                                              PhysicalChannelsLevel =
                                                  new MeasurementSystemLevelLoadOptions
                                                      {
                                                          ComputeCurrentConfiguration = false,
                                                          LoadSavedConfiguration = true
                                                      },
                                              LogicalChannelsLevel =
                                                  new MeasurementSystemLevelLoadOptions
                                                      {
                                                          ComputeCurrentConfiguration = false,
                                                          LoadSavedConfiguration = true
                                                      }
                                            };
            FieldBusLoadOptions.Add(fieldBusType, fieldBusLoadOptions); 

            //Ethernet
            fieldBusType = FieldBusType.Ethernet;
            fieldBusLoadOptions = new FieldBusLoadOptions(fieldBusType)
                                            {
                                                FieldBusNodeAddresses =
                                                    fieldBusDAC.GetAddresses(fieldBusType),
                                                FieldNodesLevel =
                                                    new MeasurementSystemLevelLoadOptions
                                                    {
                                                        ComputeCurrentConfiguration = 
#if IIST
                                                        true
#else
                                                        false
#endif
                                                        ,
                                                        LoadSavedConfiguration = true
                                                    },
                                                PhysicalChannelsLevel =
                                                    new MeasurementSystemLevelLoadOptions
                                                    {
                                                        ComputeCurrentConfiguration = 
#if IIST
                                                        true
#else
                                                        false
#endif
                                                        ,
                                                        LoadSavedConfiguration = true
                                                    },
                                                LogicalChannelsLevel =
                                                    new MeasurementSystemLevelLoadOptions
                                                    {
                                                        ComputeCurrentConfiguration = 
#if IIST
                                                        true
#else
                                                        false
#endif
                                                        ,
                                                        LoadSavedConfiguration = true
                                                    }
                                            };
            FieldBusLoadOptions.Add(fieldBusType, fieldBusLoadOptions);
        }


    }
}