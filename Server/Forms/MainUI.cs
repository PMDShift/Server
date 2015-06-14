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
    public partial class MainUI : Form
    {
        CommandHandler commandProcessor;

        public MainUI()
        {
            InitializeComponent();
            commandProcessor = new CommandHandler(this);
            this.Text = Settings.GameNameShort + " Server";
        }

        private void txtCommand_TextChanged(object sender, EventArgs e)
        {
            int activeLine = txtCommandOutput.Lines.Length - 1;
            string[] lines = txtCommandOutput.Lines;
            lines[activeLine] = "Server> " + txtCommand.Text;
            txtCommandOutput.Lines = lines;
            ScrollToBottom(txtCommandOutput);
        }

        private void ScrollToBottom(TextBox txtBox)
        {
            txtBox.SelectionStart = txtBox.Text.Length - 1;
            txtBox.SelectionLength = 1;
            txtBox.ScrollToCaret();
        }

        private void txtCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && ProcessingCommand == false)
            {
                string command = txtCommand.Text;
                txtCommand.Text = "";
                int activeLine = txtCommandOutput.Lines.Length - 1;
                string[] lines = txtCommandOutput.Lines;
                lines[activeLine] = "Server> " + command;
                txtCommandOutput.Lines = lines;
                if (commandProcessor.IsValidCommand(command))
                {
                    ProcessCommand(command);
                }
                else
                {
                    AddCommandLine(command + " is not a valid command.");
                    AddCommandLine("Server> ");
                }
            }
        }

        private delegate void AddCommandLineDelegate(string line);
        public void AddCommandLine(string line)
        {
            if (InvokeRequired)
            {
                Invoke(new AddCommandLineDelegate(AddCommandLine), line);
            }
            else
            {
                txtCommandOutput.Text += "\r\n" + line;
                ScrollToBottom(txtCommandOutput);
            }
        }
        private bool ProcessingCommand = false;

        private void ProcessCommand(string command)
        {
            txtCommand.Enabled = false;
            int activeLine = txtCommandOutput.Lines.Length - 1;
            string[] lines = txtCommandOutput.Lines;
            lines[activeLine] = "Server> " + command + " [Processing]";
            txtCommandOutput.Lines = lines;
            commandProcessor.ProcessCommand(activeLine, command);
        }

        private delegate void CommandCompleteDelegate(int activeLine, string command);
        internal void CommandComplete(int activeLine, string command)
        {
            if (InvokeRequired)
            {
                Invoke(new CommandCompleteDelegate(CommandComplete), activeLine, command);
            }
            else
            {
                if (txtCommandOutput.Lines.Length > activeLine)
                {
                    string[] lines = txtCommandOutput.Lines;
                    lines[activeLine] = "Server> " + command;
                    txtCommandOutput.Lines = lines;
                }
                AddCommandLine("Server> ");
                txtCommand.Enabled = true;
                txtCommand.Focus();
            }
        }

        private delegate void ClearCommandsDelegate();
        internal void ClearCommands()
        {
            if (InvokeRequired)
            {
                Invoke(new ClearCommandsDelegate(ClearCommands));
            }
            else
            {
                txtCommandOutput.Text = Settings.GameNameShort + " Server Command Prompt";
            }
        }

        private void txtCommandOutput_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
