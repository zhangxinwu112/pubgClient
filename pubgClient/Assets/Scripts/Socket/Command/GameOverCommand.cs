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
    public class GameOverCommand : ICommand
    {
        private static ILog log = LogManagers.GetLogger("GameOverCommand");
        public string Name
        {
            get
            {
                return "GameOver";
            }
        }

        public object ExecuteCommand(string body, string[] parameter)
        {
            string content = body.ToString();

            //NGUIDebug.Log("game over");
            //EventMgr.Instance.SendEvent(EventName.UPDATE_PLAYER_STATE, null);
            if(ShowMapPoint.instacne!=null)
            {
                ShowMapPoint.instacne.GameOver();
            }
            
            return null;

        }


    }
}
