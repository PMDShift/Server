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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Server.Forms
{
    public partial class LoadingUI : Form
    {
        bool closeNormal;

        public LoadingUI() {
            InitializeComponent();
        }

        public delegate void UpdateStatusDelegate(string text);
        public void UpdateStatus(string text) {
            if (InvokeRequired) {
                Invoke(new UpdateStatusDelegate(UpdateStatus), text);
            } else {
                lblStatus.Text = text;
            }
        }

        private delegate void CloseDelegate(bool normal);
        public void Close(bool normal) {
            if (InvokeRequired) {
                Invoke(new CloseDelegate(Close), normal);
            } else {
                if (normal) {
                    closeNormal = true;
                    this.Close();
                } else {
                    this.Close();
                }
            }
        }

        private void LoadingUI_FormClosing(object sender, FormClosingEventArgs e) {
            if (!closeNormal) {
                Environment.Exit(0);
            }
        }

        private void LoadingUI_Load(object sender, EventArgs e)
        {

        }

    }
}
