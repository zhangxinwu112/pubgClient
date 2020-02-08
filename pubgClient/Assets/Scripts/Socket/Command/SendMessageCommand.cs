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
    public class SendMessageCommand : ICommand
    {
        private static ILog log = LogManagers.GetLogger("SendMessageCommand");
        public string Name
        {
            get
            {
                return "SendMessage";
            }
        }

        public object ExecuteCommand(string body, string[] parameter)
        {
            string content = body.ToString();
            NGUIDebug.Log(content);
            //GrounpStateProxy.Debug(content,null);
            ShowMapPoint.instacne.Show(Utils.CollectionsConvert.ToJSON(content));
            ShowMapPoint.instacne.ShowChatMesage(content);
            return null;

        }


    }
}
