// This file is part of Mystery Dungeon eXtended.

// Copyright (C) 2015 Pikablu, MDX Contributors, PMU Staff

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.


using System;
using System.Collections.Generic;
using System.Text;
using Server.Players;
using Server.Maps;
using Server.Network;
using Server.Items;

namespace Server
{
    class CommandHandler
    {
        public CommandHandler()
        {
        }

        public void ProcessCommand(int activeLine, string command)
        {
            ThreadManager.StartOnThreadParams(new System.Threading.WaitCallback(ProcessCommandCallback), new ParamObject(activeLine, command));
        }

        public void ProcessCommandCallback(object obj)
        {
            int activeLine = (int)((ParamObject)obj).Param[0];
            string command = (string)((ParamObject)obj).Param[1];
            Command fullCommand = Server.CommandProcessor.ParseCommand(command);
            string fullArgs = CommandProcessor.JoinArgs(fullCommand.CommandArgs.ToArray());
            switch (fullCommand.CommandArgs[0].ToLower())
            {
                case "/help":
                    {
                        DisplayHelp();
                    }
                    break;
                case "/clear":
                    {
                    }
                    break;
                case "/global":
                    {
                        Messenger.GlobalMsg(fullArgs, Text.White);
                        ServerConsole.WriteLine("Global: " + fullArgs);
                    }
                    break;
                case "/masskick":
                    {
                        foreach (Client i in ClientManager.GetClients())
                        {
                            if (i.IsPlaying() && Ranks.IsDisallowed(i, Enums.Rank.Monitor))
                            {
                                Messenger.GlobalMsg(i.Player.Name + " has been kicked by the server!", Text.White);
                                Messenger.AlertMsg(i, "You have been kicked by the server!");
                            }
                        }
                        ServerConsole.WriteLine("Everyone has been kicked.");
                    }
                    break;
                case "/dumpstats":
                    {
                        Statistics.PacketStatistics.DumpStatistics();
                        ServerConsole.WriteLine("Packet statistics dumped to database.");
                    }
                    break;
                case "/clearstats":
                    {
                        Statistics.PacketStatistics.ClearStatistics();
                        ServerConsole.WriteLine("Packet statistics cleared.");
                    }
                    break;
                case "/masswarp":
                    {
                        if (fullCommand.CommandArgs.Count == 4)
                        {
                            int map = fullCommand.CommandArgs[1].ToInt(-1);
                            int x = fullCommand.CommandArgs[2].ToInt(-1);
                            int y = fullCommand.CommandArgs[3].ToInt(-1);
                            if (map <= 0)
                            {
                                ServerConsole.WriteLine("Invalid Map.");
                                break;
                            }
                            else if (x == -1)
                            {
                                ServerConsole.WriteLine("Invalid X coordinate.");
                                break;
                            }
                            else if (y == -1)
                            {
                                ServerConsole.WriteLine("Invalid Y coordinate.");
                                break;
                            }
                            // TODO: Mass Warp
                            //if (x > MapManager.Maps[map].MaxX) {
                            //    ServerConsole.WriteLine("Invalid X coordinate.");
                            //    break;
                            //}
                            //if (y > MapManager.Maps[map].MaxY) {
                            //    ServerConsole.WriteLine("Invalid Y coordinate.");
                            //    break;
                            //}
                            foreach (Client i in ClientManager.GetClients())
                            {
                                if (i.IsPlaying() && Ranks.IsDisallowed(i, Enums.Rank.Monitor))
                                {
                                    Messenger.GlobalMsg("The server has warped everyone!", Text.White);
                                    Messenger.PlayerWarp(i, map, x, y);
                                }
                            }
                            ServerConsole.WriteLine("Everyone has been warped.");
                        }
                        else
                        {
                            ServerConsole.WriteLine("Invalid arguments.");
                        }
                    }
                    break;
                case "/kick":
                    {
                        if (fullCommand.CommandArgs.Count == 2)
                        {
                            Client client = ClientManager.FindClient(fullCommand.CommandArgs[1]);
                            if (client == null)
                            {
                                ServerConsole.WriteLine("Player is offline.");
                            }
                            else
                            {
                                Messenger.GlobalMsg(client.Player.Name + " has been kicked by the server!", Text.White);
                                Messenger.AlertMsg(client, "You have been kicked by the server!");
                                ServerConsole.WriteLine(client.Player.Name + " has been kicked!");
                            }
                        }
                        else
                        {
                            ServerConsole.WriteLine("Invalid arguments.");
                        }
                    }
                    break;
                case "/warp":
                    {
                        if (fullCommand.CommandArgs.Count == 5)
                        {
                            Client client = ClientManager.FindClient(fullCommand.CommandArgs[1]);
                            if (client == null)
                            {
                                ServerConsole.WriteLine("Player is offline.");
                            }
                            else
                            {
                                int mapNum = fullCommand.CommandArgs[2].ToInt(-1);
                                int x = fullCommand.CommandArgs[3].ToInt(-1);
                                int y = fullCommand.CommandArgs[4].ToInt(-1);
                                if (mapNum <= 0)
                                {
                                    ServerConsole.WriteLine("Invalid Map.");
                                    break;
                                }
                                else if (x == -1)
                                {
                                    ServerConsole.WriteLine("Invalid X coordinate.");
                                    break;
                                }
                                else if (y == -1)
                                {
                                    ServerConsole.WriteLine("Invalid Y coordinate.");
                                    break;
                                }
                                IMap map;
                                if (MapManager.IsMapActive(MapManager.GenerateMapID(mapNum)))
                                {
                                    map = MapManager.RetrieveActiveMap(MapManager.GenerateMapID(mapNum));
                                }
                                else
                                {
                                    using (Database.DatabaseConnection dbConnection = new Database.DatabaseConnection(Database.DatabaseID.Data))
                                    {
                                        map = MapManager.LoadStandardMap(dbConnection, MapManager.GenerateMapID(mapNum));
                                    }
                                }
                                if (x > map.MaxX)
                                {
                                    ServerConsole.WriteLine("Invalid X coordinate.");
                                    break;
                                }
                                if (y > map.MaxY)
                                {
                                    ServerConsole.WriteLine("Invalid Y coordinate.");
                                    break;
                                }
                                Messenger.PlayerMsg(client, "You have been warped by the server!", Text.White);
                                Messenger.PlayerWarp(client, mapNum, x, y);
                                ServerConsole.WriteLine(client.Player.Name + " has been warped.");
                            }
                        }
                        else
                        {
                            ServerConsole.WriteLine("Invalid arguments.");
                        }
                    }
                    break;
                case "/mapmsg":
                    {
                        if (fullCommand.CommandArgs.Count == 3)
                        {
                            string map = fullCommand.CommandArgs[1];
                            // Check if the map is active
                            if (!MapManager.IsMapActive(map))
                            {
                                ServerConsole.WriteLine("Invalid Map.");
                                break;
                            }
                            Messenger.MapMsg(map, fullCommand.CommandArgs[2], Text.DarkGrey);
                            ServerConsole.WriteLine("Map Msg (Map " + map.ToString() + "): " + fullCommand.CommandArgs[2]);
                        }
                        else
                        {
                            ServerConsole.WriteLine("Invalid arguments.");
                        }
                    }
                    break;
                case "/reloadscripts":
                    {
                        Scripting.ScriptManager.Reload();
                        ServerConsole.WriteLine("Scripts reloaded.");
                    }
                    break;
                case "/players":
                    {
                        string players = "";
                        int count = 0;
                        foreach (Client i in ClientManager.GetClients())
                        {
                            if (i.IsPlaying())
                            {
                                count++;
                                players += i.Player.Name + "\r\n";
                            }
                        }
                        ServerConsole.WriteLine("Players online: \r\n" + players);
                        ServerConsole.WriteLine("There are " + count.ToString() + " players online");
                    }
                    break;
                case "/test":
                    {
                        //Email.Email.SendEmail("test");
                        //ServerConsole.WriteLine("Mail sent!");
                        //ServerConsole.WriteLine("There are currently no benchmarking tests");
                        //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                        //watch.Start();
                        //MapGeneralInfo genInfo = MapManager.RetrieveMapGeneralInfo(10);
                        //watch.Stop();
                        //ServerConsole.WriteLine("Elapsed, New: " + watch.Elapsed.ToString());
                        //watch.Reset();
                        //watch.Start();
                        //Map map = MapManager.LoadMap(10);
                        //watch.Stop();
                        //ServerConsole.WriteLine("Elapsed, Old: " + watch.Elapsed.ToString());
                        //ServerConsole.WriteLine("Name: " + genInfo.Name);
                    }
                    break;
                case "/finditem":
                    {
                        int itemsFound = 0;
                        for (int i = 0; i < Server.Items.ItemManager.Items.MaxItems; i++)
                        {
                            if (ItemManager.Items[i].Name.ToLower().StartsWith(fullCommand.CommandArgs[1].ToLower()))
                            {
                                ServerConsole.WriteLine(ItemManager.Items[i].Name + "'s number is " + i.ToString());
                                itemsFound++;
                                //return;
                            }
                        }
                        if (itemsFound == 0)
                        {
                            ServerConsole.WriteLine("Unable to find an item that starts with '" + fullCommand.CommandArgs[1] + "'");
                        }
                    }
                    break;
                case "/finditemc":
                    {
                        int itemsFound = 0;
                        for (int i = 0; i < Server.Items.ItemManager.Items.MaxItems; i++)
                        {
                            if (ItemManager.Items[i].Name.ToLower().Contains(fullCommand.CommandArgs[1].ToLower()))
                            {
                                ServerConsole.WriteLine(ItemManager.Items[i].Name + "'s number is " + i.ToString());
                                itemsFound++;
                                //return;
                            }
                        }
                        if (itemsFound == 0)
                        {
                            ServerConsole.WriteLine("Unable to find an item that starts with '" + fullCommand.CommandArgs[1] + "'");
                        }
                    }
                    break;
                case "/calcwm":
                    {
                        ServerConsole.WriteLine("Factorial: " + Server.Math.CalculateFactorial(fullCommand.CommandArgs[1].ToInt()).ToString("R"));
                    }
                    break;
                case "/gmmode":
                    {
                        Globals.GMOnly = !Globals.GMOnly;
                        ServerConsole.WriteLine("GM Only Mode Active: " + Globals.GMOnly);
                    }
                    break;
                default:
                    {
                        Scripting.ScriptManager.InvokeSub("ProcessServerCommand", fullCommand, fullArgs);
                    }
                    break;
            }
        }

        internal bool IsValidCommand(string command)
        {
            string[] args = CommandProcessor.SplitCommand(command);
            switch (args[0].ToLower())
            {
                case "/help":
                case "/clear":
                case "/global":
                case "/masskick":
                case "/masswarp":
                case "/dumpstats":
                case "/clearstats":
                case "/kick":
                case "/warp":
                case "/mapmsg":
                case "/mute":
                case "/reloadscripts":
                case "/togglescripts":
                case "/players":
                case "/test":
                case "/finditem":
                case "/finditemc":
                case "/calcwm":
                case "/gmmode":
                    return true;
                default:
                    return Scripting.ScriptManager.InvokeFunction("IsValidServerCommand", args[0], command).ToBool();
            }
        }

        private void DisplayHelp()
        {
            ServerConsole.WriteLine("-=- Server Command Help -=-");
            ServerConsole.WriteLine("Available Commands:");
            ServerConsole.WriteLine("/help - Shows help");
            ServerConsole.WriteLine("/clear - Clears the command prompt");
            ServerConsole.WriteLine("/global [string]msg - Sends a global message");
            ServerConsole.WriteLine("/masskick - Kicks all players, excluding staff");
            ServerConsole.WriteLine("/masswarp [int]map [int]x [int]y - Warps all players to the specified location");
            ServerConsole.WriteLine("/kick [string]playername - Kicks the player specified");
            ServerConsole.WriteLine("/warp [string]playername [int]map [int]x [int]y - Warps the player to the specified location");
            ServerConsole.WriteLine("/mapmsg [int]mapnum [string]msg - Sends a message to the map");
            ServerConsole.WriteLine("/reloadscripts - Reloads the scripts");
            ServerConsole.WriteLine("/togglescripts [bool]value - Turns the scripts on/off based on the value");
            ServerConsole.WriteLine("/players - Gets a list of all online players");
            Scripting.ScriptManager.InvokeSub("DisplayServerCommandHelp");
        }
    }
}
