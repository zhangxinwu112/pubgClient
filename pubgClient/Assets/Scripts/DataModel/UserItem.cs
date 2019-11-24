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
        // 0 超级管理员
        //1 普通管理员
        // 2 道具
        // 3 普通玩家
        public int type;
    }
}
