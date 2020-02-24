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
    public class UpdatePlayerStateCommand : ICommand
    {
        private static ILog log = LogManagers.GetLogger("UpdatePlayerStateCommand");
        public string Name
        {
            get
            {
                return "UpdatePlayerState";
            }
        }

        public object ExecuteCommand(string body, string[] parameter)
        {
            string content = body.ToString();

           // NGUIDebug.Log("刷新");
            EventMgr.Instance.SendEvent(EventName.UPDATE_PLAYER_STATE, null);
            
            return null;

        }


    }
}
