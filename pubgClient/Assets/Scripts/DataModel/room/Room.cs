﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Room: ModelBase
    {
 
        public int grounpId;

   
        public string checkCode;

        public int userCount;

        public bool isCurrentUser = false;

        public int userId;

        public int runState;



    }
}
