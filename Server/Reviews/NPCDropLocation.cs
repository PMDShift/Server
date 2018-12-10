using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Reviews
{
    public class NPCDropLocation : ILocation
    {
        public int NPCID { get; set; }
        public int DropSlot { get; set; }

        public NPCDropLocation(int npcID, int dropSlot)
        {
            this.NPCID = npcID;
            this.DropSlot = dropSlot;
        }

        public string GetDescription()
        {
            var npc = Server.Npcs.NpcManager.Npcs[NPCID];

            return $"NPC [{NPCID}] `{npc.Name}` Drop {DropSlot + 1}";
        }

        public bool Equals(ILocation other)
        {
            if (other is NPCDropLocation npcDropLocation)
            {
                if (npcDropLocation.NPCID == NPCID && npcDropLocation.DropSlot == DropSlot)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
