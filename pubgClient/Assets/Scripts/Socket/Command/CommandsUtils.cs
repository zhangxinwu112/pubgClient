
using Core.Common.Logging;
using System;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;
using Core.Server.Command;

namespace Br.Core.Server
{
    class CommandsUtils
    {
        private static ILog log = LogManagers.GetLogger("CommandsUtils");
        private  Dictionary <string, ICommand> commandsMap = new Dictionary<string, ICommand>();

        public void Add(ICommand command) {
            
            if(!commandsMap.ContainsKey(command.Name))
            {
                commandsMap.Add(command.Name, command);
            }
        }

        public void Exec(string key, string body, string[] parameter) {

            if(string.IsNullOrEmpty(key))
            {
                return;
            }

            ICommand command = null;
            if (!commandsMap.TryGetValue(key, out command)) {
                
                command = new UndefinedCommand();
            }
            try
            {
                if (command != null)
                {
                    try
                    {
                        object data = command.ExecuteCommand(body, parameter);
                        //server.Response(serverRequstData.RequestInfo, data);
                    }
                    catch {
                       // log.Error("执行命令[" + serverRequstData.Action + "]出错");
                    }
                }
                else
                {
                    log.Warn("not find [" + key + "]");
                }
            }
            catch (CommandExecException commandExecException)
            {
               // server.Response(serverRequstData.RequestInfo, commandExecException.BillData, commandExecException.ErrorCode, commandExecException.Message);
            }
            catch (Exception exception)
            {
                //server.Response(serverRequstData.RequestInfo, null, "1", exception.Message);
            }
        }

        public void RegCommands(List<ICommand> commands)
        {
            if (commands != null)
            {
                commands.ForEach((command) =>
                {
                    Add(command);
                });
            }
        }
    }


}
