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
using Server.Players;
using Server.Maps;
using Server.Moves;
using System.Collections.Concurrent;
using System.Threading;

namespace Server.Combat
{
    public class ExtraStatusCollection
    {

        private List<ExtraStatus> extraStatus;
        ReaderWriterLockSlim rwLock;

        public ExtraStatusCollection() {
            extraStatus = new List<ExtraStatus>();
            rwLock = new ReaderWriterLockSlim();
        }

        public void Add(ExtraStatus status) {
            rwLock.EnterWriteLock();
            try {
                extraStatus.Add(status);
            } finally {
                rwLock.ExitWriteLock();
            }
        }

        public void Clear() {
            rwLock.EnterWriteLock();
            try {
                extraStatus.Clear();
            } finally {
                rwLock.ExitWriteLock();
            }
        }

        public void Remove(ExtraStatus status) {
            rwLock.EnterWriteLock();
            try {
                extraStatus.Remove(status);
            } finally {
                rwLock.ExitWriteLock();
            }
        }

        public ExtraStatus GetStatus(string statusName) {
            rwLock.EnterReadLock();
            try {
                foreach (ExtraStatus vs in extraStatus) {
                    if (vs.Name == statusName) {
                        return vs;
                    }
                }
                return null;
            } finally {
                rwLock.ExitReadLock();
            }
        }

        public List<ExtraStatus> GetStatuses(params string[] statusNames) {
            rwLock.EnterReadLock();
            try {
                List<ExtraStatus> statuses = new List<ExtraStatus>((statusNames.Length > extraStatus.Count) ? statusNames.Length : extraStatus.Count);
                foreach (ExtraStatus vs in extraStatus) {
                    for (int i = 0; i < statusNames.Length; i++) {
                        if (vs.Name == statusNames[i]) {
                            statuses.Add(vs);
                            break;
                        }
                    }
                }

                return statuses;
            } finally {
                rwLock.ExitReadLock();
            }
        }

        public int Count {
            get {
                rwLock.EnterReadLock();
                try {
                    return extraStatus.Count;
                } finally {
                    rwLock.ExitReadLock();
                }
            }
        }

        public ExtraStatus this[int index] {
            get {
                rwLock.EnterReadLock();
                try {
                    return extraStatus[index];
                } finally {
                    rwLock.ExitReadLock();
                }
            }
            set {
                rwLock.EnterWriteLock();
                try {
                    extraStatus[index] = value;
                } finally {
                    rwLock.ExitWriteLock();
                }
            }
        }


    }
}
