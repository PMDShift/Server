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
using Server.Scripting;
using Server.Network;

namespace Server.CustomMenus
{
    public class CustomMenuManager
    {
        Client Client;
        Dictionary<string, CustomMenu> mMenus;

        internal CustomMenuManager(Client client)
        {
            Client = client;
            mMenus = new Dictionary<string, CustomMenu>();
        }

        public CustomMenu CreateMenu(string menuName, string backgroundImagePath, bool closeable)
        {
            return new CustomMenu(menuName, backgroundImagePath, closeable);
        }

        public void AddMenu(CustomMenu menuToAdd)
        {
            if (mMenus.ContainsKey(menuToAdd.MenuName) == false)
            {
                mMenus.Add(menuToAdd.MenuName, menuToAdd);
            }
        }

        public void SendMenu(string menuName)
        {
            if (mMenus.ContainsKey(menuName))
            {
                mMenus[menuName].SendMenuTo(Client);
            }
        }

        public bool IsMenuOpen(string menuName)
        {
            return mMenus.ContainsKey(menuName);
        }

        // TODO: Add subs to scripts
        internal void ProcessTCP(string[] parse)
        {
            if (IsMenuOpen(parse[1]))
            {
                switch (parse[0].ToLower())
                {
                    case "picclick":
                        ScriptManager.InvokeSub("MenuPicClicked", Client, parse[1], parse[2].ToInt());
                        break;
                    case "lblclick":
                        ScriptManager.InvokeSub("MenuLblClicked", Client, parse[1], parse[2].ToInt());
                        break;
                    case "txtclick":
                        ScriptManager.InvokeSub("MenuTxtClicked", Client, parse[1], parse[2].ToInt(), parse[3]);
                        break;
                    case "menuclosed":
                        ScriptManager.InvokeSub("MenuClosed", Client, parse[1]);
                        mMenus.Remove(parse[1]);
                        break;
                }
            }
        }
    }
}
