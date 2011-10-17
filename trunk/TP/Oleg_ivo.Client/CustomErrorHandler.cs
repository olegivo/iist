using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Oleg_ivo.HighLevelClient
{
    class CustomErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error)
        {
            try
            {
                Console.WriteLine(error);
                //MyServiceLogging.Log(error);
            }
            catch
            {
                
            }
            finally
            {
                //return false; // не останавливать вызов расширенной обработки исключений
            }

            return false;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            
        }
    }
}