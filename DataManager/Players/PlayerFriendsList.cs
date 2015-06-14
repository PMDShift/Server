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
using PMDCP.DatabaseConnector.MySql;
using PMDCP.DatabaseConnector;

namespace DataManager.Players
{
    public class PlayerFriendsList
    {
        public static readonly bool UpdateOnDemand = true;

        List<string> friends;
        string ownerCharID;
        bool loaded;

        public bool Loaded {
            get { return loaded; }
            internal set {
                loaded = value;
            }
        }

        public PlayerFriendsList(string ownerCharID) {
            this.ownerCharID = ownerCharID;
            friends = new List<string>();
        }

        public int Count {
            get { return friends.Count; }
        }

        /// <summary>
        /// Adds a new friend. Return codes are as follows:
        /// <list type="table">
        /// <item>
        /// 0 - Success
        /// </item>
        /// <item>
        /// 1 - Friend is already on list
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="name"></param>
        public int AddFriend(MySql database, string name) {
            int error = 0;
            if (friends.Contains(name) == false) {
                QuickAdd(name);

                if (UpdateOnDemand && database != null) {
                    database.UpdateOrInsert("friends", new IDataColumn[] {
                        database.CreateColumn(false, "CharID", ownerCharID),
                        database.CreateColumn(false, "FriendListSlot", (friends.Count - 1).ToString()),
                        database.CreateColumn(false, "FriendName", name)
                    });
                }
            } else {
                error = 1;
            }
            return error;
        }

        /// <summary>
        /// Removes a friend based on name. Return codes are as follows:
        /// <list type="number">
        /// <item>
        /// 0 - Success
        /// </item>
        /// <item>
        /// 1 - Friend not found
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int RemoveFriend(MySql database, string name) {
            int error = 0;
            int friendIndex = friends.IndexOf(name);
            if (friendIndex > -1) {
                QuickRemove(friendIndex);

                if (UpdateOnDemand && database != null) {
                    database.DeleteRow("friends", "CharID = \'" + ownerCharID + "\' AND FriendName = \'" + name + "\'");
                }
            } else {
                error = 1;
            }

            return error;
        }

        public bool HasFriend(string name) {
            return friends.Contains(name);
        }

        public void QuickAdd(string name) {
            friends.Add(name);
        }

        public void QuickRemove(int index) {
            friends.RemoveAt(index);
        }

        public string this[int index] {
            get { return friends[index]; }
        }
    }
}
