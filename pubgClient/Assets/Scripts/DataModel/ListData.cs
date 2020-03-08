using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ListData
    {

        private static List<Grounp> grounpList;
        public  static  void SetGameListData(List<Grounp> _grounpList)
        {
            grounpList = _grounpList;
        }
        public static Grounp FindGrounpByKey(string id)
        {
            if (grounpList != null && grounpList.Count > 0)
            {
                Grounp grounp = grounpList.Find(c => c.id.ToString().Equals(id));//返回指定条件的元素
                return grounp;
            }

            return null;
        }

        public static int GetGrounpIndex(int id)
        {
            if (grounpList != null && grounpList.Count > 0)
            {
                for(int i=0;i< grounpList.Count;i++)
                {
                    if(grounpList[i].id == id)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }


        private static List<Room> roomList;
        public static void SetRoomListData(List<Room> _roomList)
        {
            roomList = _roomList;
        }

        public  static Room FindRoomByKey(string id)
        {
            if (roomList != null && roomList.Count > 0)
            {
                Room room = roomList.Find(c => c.id.ToString().Equals(id));//返回指定条件的元素
                return room;
            }

            return null;
        }

        /// <summary>
        /// 获取选择的game对象索引
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public static int GetIndexGameSelect(string gameId)
        {
            if(grounpList!=null)
            {
                for(int i=0;i< grounpList.Count;i++)
                {
                    if(grounpList[i].id.ToString().Equals( gameId))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

      

       
    }
}
