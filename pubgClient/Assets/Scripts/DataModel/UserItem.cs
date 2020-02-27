using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class UserItem: ModelBase
    {

        public string telephone;
        public string image;
        // 0 玩家
        //1 管理员
        // 2 道具
        public int type;

        public int runState = -1;

        public bool isLeader = false;

    }
}
