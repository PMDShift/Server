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


namespace Server.Emoticons
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;

    public class EmoticonManagerBase
    {
        #region Fields

        static EmoticonCollection emoticons;

        #endregion Fields

        #region Events

        public static event EventHandler LoadComplete;

        public static event EventHandler<LoadingUpdateEventArgs> LoadUpdate;

        #endregion Events

        #region Properties

        public static EmoticonCollection Emoticons {
            get { return emoticons; }
        }

        #endregion Properties

        #region Methods

        public static void CheckEmotions() {
            if (System.IO.File.Exists(Path.Combine(IO.Paths.DataFolder, "emoticons.xml")) == false) {
                //SaveEmotions();
            }
        }

        public static void Initialize(int maxEmotions) {
            emoticons = new EmoticonCollection(maxEmotions);
            CheckEmotions();
        }

        public static void LoadEmotions(object object1) {
            try {
                using (XmlReader reader = XmlReader.Create(Path.Combine(IO.Paths.DataFolder, "emoticons.xml"))) {
                    while (reader.Read()) {
                        if (reader.IsStartElement()) {
                            switch (reader.Name) {
                                case "Emoticon": {
                                        string idval = reader["id"];
                                        int id = 0;
                                        if (idval != null) {
                                            id = idval.ToInt();
                                        }
                                        emoticons[id] = new Emoticon();
                                        if (reader.Read()) {
                                            emoticons[id].Pic = reader.ReadElementString("Pic").ToInt();
                                            emoticons[id].Command = reader.ReadElementString("Command");
                                        }
                                        if (LoadUpdate != null)
                                            LoadUpdate(null, new LoadingUpdateEventArgs(id, emoticons.MaxEmoticons));
                                    }
                                    break;
                            }
                        }
                    }
                }
                if (LoadComplete != null)
                    LoadComplete(null, null);
            } catch (Exception ex) {
                Exceptions.ErrorLogger.WriteToErrorLog(ex);
            }
        }

        #endregion Methods
    }
}