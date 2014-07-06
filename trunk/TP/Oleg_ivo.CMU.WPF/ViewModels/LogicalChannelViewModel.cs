using System.Collections.Generic;
using System.Linq;
using DMS.Common.Messages;
using GalaSoft.MvvmLight;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.CMU.WPF.ViewModels
{
    public class LogicalChannelViewModel : ViewModelBase
    {
        public LogicalChannelViewModel(LogicalChannel logicalChannel)
        {
            LogicalChannel = logicalChannel;
        }

        private LogicalChannel logicalChannel;

        public LogicalChannel LogicalChannel
        {
            get { return logicalChannel; }
            set
            {
                if (logicalChannel == value) return;
                logicalChannel = value;
                RaisePropertyChanged(() => LogicalChannel);
            }
        }

        private bool isRegistered;

        public bool IsRegistered
        {
            get { return isRegistered; }
            set
            {
                if (isRegistered == value) return;
                isRegistered = value;
                RaisePropertyChanged(() => IsRegistered);
                RaisePropertyChanged(() => IsNotRegistered);
            }
        }

        public bool IsNotRegistered
        {
            get { return !IsRegistered; }
        }

        public string DisplayText
        {
            get { return string.Format("{0}", LogicalChannel.ToString()); }
        }

        public ChannelRegistrationMessage GetRegistrationMessage(string regNameFrom, RegistrationMode registrationMode)
        {
            var registrationMessage = new ChannelRegistrationMessage(regNameFrom,
                                                                     null,
                                                                     registrationMode,
                                                                     DataMode.Read | DataMode.Write,
                                                                     LogicalChannel.Id)
            {
                Description = LogicalChannel.Description,
                MinValue = LogicalChannel.MinValue,
                MaxValue = LogicalChannel.MaxValue,
                MinNormalValue = LogicalChannel.MinNormalValue,
                MaxNormalValue = LogicalChannel.MaxNormalValue,
                IsDiscrete = LogicalChannel.IsDiscrete
            };

            return registrationMessage;
        }
    }

    public static class LogicalChannelViewModelExtensions
    {
        public static LogicalChannelViewModel Find(this IEnumerable<LogicalChannelViewModel> source,
            int logicalChannelId)
        {
            return source.SingleOrDefault(model => model.LogicalChannel.Id == logicalChannelId);
        }
    }
}