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
using Server.Maps;
using Server.Npcs;
using Server.Players;
using Server.Moves;
using Server.Combat;
using Server.Network;
using PMDCP.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Server.AI
{
    class AIProcessor
    {
        public static readonly int MapTTL = 10000;
        public static MapGC mapGC;

        internal static void InitGameLoop()
        {
            ThreadManager.StartOnThreadParams(new System.Threading.WaitCallback(ProcessAI), Constants.GAME_SPEED);
        }

        internal static void ProcessAI(Object obj)
        {
            // Get the game speed
            int loopPause = (int)obj;
            if (loopPause == 0)
            {
                loopPause = 500;
            }
            Events.World.TimedEventManager.Initialize();
            Events.World.TimedEventManager.LoadBasicEvents(Core.GetTickCount());
            // The variables used in the loop
            TickCount tickCount = null;
            TickCount lastSpawn = Core.GetTickCount();
            TickCount lastConnectionCheck = Core.GetTickCount();
            Debug.InfiniteLoopDetector loopDetector = new Debug.InfiniteLoopDetector("AIProcessor");
            mapGC = new MapGC();
            System.Threading.Thread mapGCThread = new Thread(mapGC.Cleanup);
            // Start the map garbage collection thread
            mapGCThread.Start();

            Events.World.MapGCTimedEvent mapGCTimedEvent = new Events.World.MapGCTimedEvent("MapGC", mapGC);
            mapGCTimedEvent.SetInterval(Core.GetTickCount(), 1 * 60 * 1000);
            Events.World.TimedEventManager.TimedEvents.Add(mapGCTimedEvent);

            do
            {
                try
                {
                    if (tickCount != null)
                    {
                        int timePassed = Core.GetTickCount().Tick - tickCount.Tick;

                        if (timePassed < loopPause)
                        {
                            loopDetector.IncrementLoopCount();
                        }
                    }

                    tickCount = Core.GetTickCount();

                    // Copy all active map references to a local list to prevent modification during processing
                    IMap[] activeMaps = MapManager.ToArray();
                    // Now that we have a list of all active maps, lets process the AI on each one
                    //Parallel.ForEach(activeMaps, map =>
                    //{
                    foreach (IMap map in activeMaps)
                    {
                        MapAIProcessingTask aiProcessingTask = new MapAIProcessingTask(map);
                        aiProcessingTask.ProcessAIThreadPoolCallback(null);
                        //ThreadPool.QueueUserWorkItem(new WaitCallback(aiProcessingTask.ProcessAIThreadPoolCallback));
                    }
                    //}
                    //);

                    Events.World.TimedEventManager.Process(tickCount);

                    // Pauses the loop for the time specified
                    System.Threading.Thread.Sleep(loopPause);
                }
                catch (Exception ex)
                {
                    Server.Exceptions.ErrorLogger.WriteToErrorLog(ex, "AIProcessor");
                }
            } while (true);
        }

        public static bool MoveNpcInDirection(Enums.Direction direction, IMap map, Client target, PacketHitList packetList, int mapNpcSlot)
        {
            switch (direction)
            {
                case Enums.Direction.Up:
                    {
                        return MoveNpcUp(map, target, packetList, mapNpcSlot);
                    }
                case Enums.Direction.Down:
                    {
                        return MoveNpcDown(map, target, packetList, mapNpcSlot);
                    }
                case Enums.Direction.Left:
                    {
                        return MoveNpcLeft(map, target, packetList, mapNpcSlot);
                    }
                case Enums.Direction.Right:
                    {
                        return MoveNpcRight(map, target, packetList, mapNpcSlot);
                    }
            }
            return false;
        }

        public static bool MoveNpcUp(IMap map, Client target, PacketHitList packetList, int mapNpcSlot)
        {
            // Up
            if (map.ActiveNpc[mapNpcSlot].Y > target.Player.Y)
            {
                if (MovementProcessor.CanNpcMove(map, mapNpcSlot, Enums.Direction.Up))
                {
                    MovementProcessor.NpcMove(packetList, map, mapNpcSlot, map.ActiveNpc[mapNpcSlot].Direction, Enums.Speed.Walking);
                    return true;
                }
            }
            return false;
        }

        public static bool MoveNpcDown(IMap map, Client target, PacketHitList packetList, int mapNpcSlot)
        {
            // Down
            if (map.ActiveNpc[mapNpcSlot].Y < target.Player.Y)
            {
                if (MovementProcessor.CanNpcMove(map, mapNpcSlot, Enums.Direction.Down))
                {
                    MovementProcessor.NpcMove(packetList, map, mapNpcSlot, map.ActiveNpc[mapNpcSlot].Direction, Enums.Speed.Walking);
                    return true;
                }
            }
            return false;
        }

        public static bool MoveNpcLeft(IMap map, Client target, PacketHitList packetList, int mapNpcSlot)
        {
            // Left
            if (map.ActiveNpc[mapNpcSlot].X > target.Player.X)
            {
                if (MovementProcessor.CanNpcMove(map, mapNpcSlot, Enums.Direction.Left))
                {
                    MovementProcessor.NpcMove(packetList, map, mapNpcSlot, map.ActiveNpc[mapNpcSlot].Direction, Enums.Speed.Walking);
                    return true;
                }
            }
            return false;
        }

        public static bool MoveNpcRight(IMap map, Client target, PacketHitList packetList, int mapNpcSlot)
        {
            // Right
            if (map.ActiveNpc[mapNpcSlot].X < target.Player.X)
            {
                if (MovementProcessor.CanNpcMove(map, mapNpcSlot, Enums.Direction.Right))
                {
                    MovementProcessor.NpcMove(packetList, map, mapNpcSlot, map.ActiveNpc[mapNpcSlot].Direction, Enums.Speed.Walking);
                    return true;
                }
            }
            return false;
        }
    }
}
