using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Network;
using System.Xml;
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


namespace Server.Events.Player.TriggerEvents
{
    public class StepCounterTriggerEvent : ITriggerEvent
    {
        #region Fields

        TriggerEventAction action;
        Client client;
        string id;
        int steps;
        int triggerCommand;
        bool autoRemove;
        int stepsCounted;

        #endregion Fields

        #region Constructors

        internal StepCounterTriggerEvent()
        {
        }

        public StepCounterTriggerEvent(string id, TriggerEventAction action, int triggerCommand, bool autoRemove,
            Client client, int steps)
        {
            this.id = id;
            this.action = action;
            this.triggerCommand = triggerCommand;
            this.autoRemove = autoRemove;
            this.client = client;
            this.steps = steps;

            stepsCounted = 0;
        }

        #endregion Constructors

        #region Properties

        public string ID
        {
            get { return id; }
        }

        public int Steps
        {
            get { return steps; }
        }

        public int StepsCounted
        {
            get { return stepsCounted; }
            set { stepsCounted = value; }
        }

        public TriggerEventTrigger Trigger
        {
            get { return TriggerEventTrigger.StepCounter; }
        }

        public int TriggerCommand
        {
            get { return triggerCommand; }
        }

        public TriggerEventAction Action
        {
            get { return action; }
        }

        public bool AutoRemove
        {
            get { return autoRemove; }
        }

        #endregion Properties

        #region Methods

        public bool CanInvokeTrigger()
        {
            if (stepsCounted >= steps)
            {
                stepsCounted = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void InvokeTrigger()
        {
            TriggerEventHelper.InvokeGenericTrigger(this, client);
            if (autoRemove)
            {
                client.Player.RemoveTriggerEvent(this);
            }
        }

        public void Load(DataManager.Players.PlayerDataTriggerEvent triggerEvent, Client client)
        {
            this.client = client;

            id = triggerEvent.Items.GetValue("ID");
            action = (TriggerEventAction)Enum.Parse(typeof(TriggerEventAction), triggerEvent.Items.GetValue("Action"), true);
            triggerCommand = triggerEvent.Items.GetValue("TriggerCommand").ToInt();
            autoRemove = triggerEvent.Items.GetValue("AutoRemove").ToBool();

            steps = triggerEvent.Items.GetValue("Steps").ToInt();
            stepsCounted = triggerEvent.Items.GetValue("StepsCounted").ToInt();
        }

        public void Save(DataManager.Players.PlayerDataTriggerEvent triggerEvent)
        {
            triggerEvent.Items.Clear();

            triggerEvent.Items.Add("Type", ((int)Trigger).ToString());
            triggerEvent.Items.Add("ID", id);
            triggerEvent.Items.Add("Action", ((int)action).ToString());
            triggerEvent.Items.Add("TriggerCommand", triggerCommand.ToString());
            triggerEvent.Items.Add("AutoRemove", autoRemove.ToIntString());

            triggerEvent.Items.Add("Steps", steps.ToString());
            triggerEvent.Items.Add("StepsCounted", stepsCounted.ToString());
        }

        #endregion Methods
    }
}