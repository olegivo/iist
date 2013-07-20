using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using NLog;

namespace Oleg_ivo.LowLevelClient
{
    class CustomErrorHandler : IErrorHandler
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public bool HandleError(Exception error)
        {
            try
            {
                Log.Debug(error);
                //MyServiceLogging.Log(error);
            }
            catch
            {
                
            }
            finally
            {
                //return false; // �� ������������� ����� ����������� ��������� ����������
            }

            return false;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            
        }
    }
}