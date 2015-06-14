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
using Server.Network;
using PMDCP.Core;
using Server.Maps;

namespace Server.Tournaments
{
    public class Tournament
    {
        string id;
        bool tournamentStarted;
        WarpDestination hub;
        string name;
        TournamentRules rules;

        public string Name {
            get { return name; }
        }

        public WarpDestination Hub {
            get { return hub; }
        }

        public string ID {
            get { return id; }
        }

        public bool TournamentStarted {
            get { return tournamentStarted; }
        }

        TournamentMemberCollection registeredMembers;

        List<MatchUp> activeMatchups;
        List<string> combatMaps;
        InstancedMap hubMap;

        int playersNeeded = 2;

        public int PlayersNeeded {
            get { return playersNeeded; }
            set {
                if (value < 2) {
                    value = 2;
                }
                playersNeeded = value;
            }
        }

        public List<MatchUp> ActiveMatchups {
            get { return activeMatchups; }
        }

        public TournamentMemberCollection RegisteredMembers {
            get { return registeredMembers; }
        }

        public TournamentRules Rules {
            get { return rules; }
        }

        public Tournament(string id, string name, WarpDestination hub) {
            this.id = id;
            this.name = name;
            this.hub = hub;

            registeredMembers = new TournamentMemberCollection();
            activeMatchups = new List<MatchUp>();
            combatMaps = new List<string>();

            rules = new TournamentRules();
        }

        public void AddCombatMap(string mapID) {
            combatMaps.Add(mapID);
        }

        public void WarpToHub(Client client) {
            if (hubMap == null) {
                hubMap = MapCloner.CreateInstancedMap(MapManager.RetrieveMap(hub.MapID));
            }
            Messenger.PlayerWarp(client, hubMap, hub.X, hub.Y);
        }

        public void CancelTournament(string reason) {
            Scripting.ScriptManager.InvokeSub("OnTournamentCanceled", this, reason);

            for (int i = registeredMembers.Count - 1; i >= 0; i--) {
                WarpToHub(registeredMembers[i].Client);
                registeredMembers[i].Client.Player.Tournament = null;
                registeredMembers[i].Client.Player.TournamentMatchUp = null;
                registeredMembers.RemoveAt(i);
            }
            TournamentManager.RemoveTournament(this);
        }

        public void RemoveRegisteredPlayer(Client client) {
            if (registeredMembers.Contains(client.Player.CharID)) {
                registeredMembers.Remove(client);
                client.Player.Tournament = null;

                bool tournyHasAdmin = false;
                for (int i = 0; i < RegisteredMembers.Count; i++) {
                    if (RegisteredMembers[i].Admin) {
                        tournyHasAdmin = true;
                        break;
                    }
                }
                if (tournyHasAdmin == false) {
                    CancelTournament("All tournament organizers have left!");
                }
            }
        }

        public void RegisterSpectator(Client client) {
            if (registeredMembers.Contains(client.Player.CharID) == false) {
                TournamentMember member = new TournamentMember(this, client);
                client.Player.Tournament = this;
                member.Active = false;
                registeredMembers.Add(member);
                OnPlayerRegistered(member);
            }
        }

        public void RegisterPlayer(Client client) {
            if (tournamentStarted == false) {
                if (registeredMembers.Count < playersNeeded && registeredMembers.Contains(client.Player.CharID) == false) {
                    TournamentMember member = new TournamentMember(this, client);
                    client.Player.Tournament = this;
                    member.Active = true;
                    registeredMembers.Add(member);
                    OnPlayerRegistered(member);
                }
            }
        }

        public void RegisterPlayer(TournamentMember member) {
            if (tournamentStarted == false) {
                if (registeredMembers.Count < playersNeeded) {
                    member.Client.Player.Tournament = this;
                    registeredMembers.Add(member);
                    OnPlayerRegistered(member);
                }
            }
        }

        private void OnPlayerRegistered(TournamentMember member) {
            Scripting.ScriptManager.InvokeSub("OnPlayerRegisteredInTournament", this, member, !member.Active);
        }

        private void StartTournamentIfReady() {
            if (IsReadyToStart()) {
                StartRound(new MatchUpRules());
            }
        }

        public int CountRemainingPlayers() {
            int activePlayerCount = 0;
            for (int i = 0; i < registeredMembers.Count; i++) {
                if (registeredMembers[i].Active) {
                    activePlayerCount++;
                }
            }
            return activePlayerCount;
        }

        public bool IsReadyToStart() {
            if (registeredMembers.Count == playersNeeded) {
                return true;
            } else {
                return false;
            }
        }

        public void StartRound(MatchUpRules matchUpRules) {
            if (!tournamentStarted) {
                tournamentStarted = true;
            }
            bool evenPlayerCount = (CountRemainingPlayers() % 2 == 0);
            TournamentMemberCollection membersWaitList = registeredMembers.Clone() as TournamentMemberCollection;
            // Remove inactive players from wait list
            for (int i = membersWaitList.Count - 1; i >= 0; i--) {
                if (membersWaitList[i].Active == false) {
                    membersWaitList.RemoveAt(i);
                }
            }
            if (!evenPlayerCount) {
                int skipIndex = MathFunctions.Rand(0, membersWaitList.Count);
                membersWaitList.RemoveAt(skipIndex);
            }
            this.activeMatchups.Clear();
            // Continue making match-ups until all players have been accounted for
            while (membersWaitList.Count > 0) {
                int playerOneIndex = MathFunctions.Rand(0, membersWaitList.Count);
                TournamentMember playerOne = membersWaitList[playerOneIndex];
                membersWaitList.RemoveAt(playerOneIndex);

                int playerTwoIndex = MathFunctions.Rand(0, membersWaitList.Count);
                TournamentMember playerTwo = membersWaitList[playerTwoIndex];
                membersWaitList.RemoveAt(playerTwoIndex);

                MatchUp matchUp = new MatchUp(GenerateUniqueMatchUpID(), this, playerOne, playerTwo);
                matchUp.Rules = matchUpRules;

                this.activeMatchups.Add(matchUp);

                int combatMapIndex = MathFunctions.Rand(0, combatMaps.Count);
                InstancedMap iMap = MapCloner.CreateInstancedMap(MapManager.RetrieveMap(combatMaps[combatMapIndex]));
                matchUp.StartMatchUp(iMap);
            }
        }

        internal void MatchUpComplete(MatchUp matchUp) {
            this.activeMatchups.Remove(matchUp);

            if (this.activeMatchups.Count == 0) {
                // All match-ups have been completed, and winners were determined

                int remainingPlayersCount = CountRemainingPlayers() + 1;
                if (remainingPlayersCount == 1) {
                    // Only one player left, so this player wins the tournament!
                    TournamentMember winner = null;
                    // Find the winning player instance
                    for (int i = 0; i < registeredMembers.Count; i++) {
                        if (registeredMembers[i].Active) {
                            winner = registeredMembers[i];
                        }
                    }
                    if (winner != null) {
                        TournamentComplete(winner);
                    }
                } else if (remainingPlayersCount > 1) {
                    // We have more than one player, continue the match-ups

                    Scripting.ScriptManager.InvokeSub("OnTournamentRoundComplete", this);
                }

            }
        }

        internal void TournamentComplete(TournamentMember winner) {
            Scripting.ScriptManager.InvokeSub("OnTournamentComplete", this, winner);
        }

        private string GenerateUniqueMatchUpID() {
            string testID;
            while (true) {
                // Generate a new ID
                testID = Security.PasswordGen.Generate(16);
                // Check if the same ID is already in use
                if (!IsMatchUpIDInUse(testID)) {
                    // If it isn't, our generated ID is useable!
                    return testID;
                }
                // If the same ID is in use, try to generate a new ID
            }
        }

        private bool IsMatchUpIDInUse(string idToTest) {
            for (int i = 0; i < activeMatchups.Count; i++) {
                if (activeMatchups[i].ID == idToTest) {
                    return true;
                }
            }
            return false;
        }

    }
}
