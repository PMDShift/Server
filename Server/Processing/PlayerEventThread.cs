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
using System.Linq;
using System.Text;
using System.Threading;

using Server.Network;

namespace Server.Processing
{
    public class PlayerEventThread
    {
        Thread playerThread;
        bool quitFlag = false;
        ManualResetEvent resetEvent;
        PlayerEvent activeEvent;
        PlayerEventQueue eventQueue;
        Client ownerClient;

        public PlayerEventThread(Client ownerClient)
        {
            playerThread = new Thread(new ThreadStart(ProcessQueuedEvents));
            resetEvent = new ManualResetEvent(false);
            eventQueue = new PlayerEventQueue();
            this.ownerClient = ownerClient;

            playerThread.IsBackground = true;
            playerThread.Start();
        }

        public void HandleClientDisconnect()
        {
            quitFlag = true;
        }

        private void ProcessQueuedEvents()
        {
            while (!quitFlag)
            {
                if (eventQueue.Empty())
                {
                    resetEvent.WaitOne();
                    resetEvent.Reset();
                }
                if (eventQueue.Empty() == false)
                {
                    activeEvent = eventQueue.Dequeue();
                    Network.MessageProcessor.ProcessData(ownerClient, activeEvent.Data);
                }
            }
        }

        public void AddEvent(PlayerEvent playerEvent)
        {
            eventQueue.Enqueue(playerEvent);
            resetEvent.Set();
        }
    }
}
