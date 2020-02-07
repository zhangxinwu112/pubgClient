﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
   public class Grounp: ModelBase
    {
        //地区
        public string area;

        //创建用户id
        public int userId;

        //运行状态-1表示停止，0表示运行
        public int runState;

        public int playerTime;

        public float fenceLon;

        public float fenceLat;

        public int fenceRadius;

        public bool isDefence = false;


    }
}
