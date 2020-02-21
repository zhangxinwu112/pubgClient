using Core.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Server.Command;

namespace Socket.Command
{
    [SCommand]
    public class ShowMessageCommand : ICommand
    {
        private static ILog log = LogManagers.GetLogger("ShowMessageCommand");
        public string Name
        {
            get
            {
                return "ShowMessage";
            }
        }

        public object ExecuteCommand(string body, string[] parameter)
        {
            string message = body.ToString();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("message", message);
            EventMgr.Instance.SendEvent(EventName.SHOW_MESSAGE, dic);
            
            return null;

        }


    }
}
