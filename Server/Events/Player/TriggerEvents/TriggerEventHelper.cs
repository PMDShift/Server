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
using Server.Stories;
using Server.Network;

namespace Server.Events.Player.TriggerEvents
{
    public class TriggerEventHelper
    {
        public static void InvokeGenericTrigger(ITriggerEvent triggerEvent, Client client)
        {
            switch (triggerEvent.Action)
            {
                case TriggerEventAction.PlayStory:
                    {
                        StoryManager.PlayStory(client, triggerEvent.TriggerCommand);
                    }
                    break;
                case TriggerEventAction.RunScript:
                    {
                        Scripting.ScriptManager.InvokeSub("TriggerEventScript", client, triggerEvent);
                    }
                    break;
            }
        }

        internal static ITriggerEvent CreateTriggerEventInstance(TriggerEventTrigger triggerEvent)
        {
            switch (triggerEvent)
            {
                case TriggerEventTrigger.MapLoad:
                    return new MapLoadTriggerEvent();
                case TriggerEventTrigger.SteppedOnTile:
                    return new SteppedOnTileTriggerEvent();
                case TriggerEventTrigger.StepCounter:
                    return new StepCounterTriggerEvent();
                default:
                    return null;
            }
        }
    }
}
