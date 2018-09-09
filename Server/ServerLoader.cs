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
using System.Threading;
using Server.Network;
using System.Threading.Tasks;
using Open.Nat;
using System.Reflection;
using Server.Discord;

namespace Server
{
    public class ServerLoader
    {
        static bool loadingComplete = false;
        static ManualResetEvent resetEvent;
        public static event EventHandler LoadComplete;

        public static void LoadServer() {
            Globals.CommandLine = CommandProcessor.ParseCommand(Environment.CommandLine); 
            string startFolder;
            int overridePath = Globals.CommandLine.FindCommandArg("-overridepath");
            if (overridePath > -1) {
                startFolder = Globals.CommandLine[overridePath + 1];
            } else {
                startFolder = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            }

            Globals.LiveTime = new System.Diagnostics.Stopwatch();
            Globals.LiveTime.Start();

            resetEvent = new ManualResetEvent(false);
            IO.IO.Initialize(startFolder);
            Settings.Initialize();
            Settings.LoadConfig();
            Settings.LoadNews();
            Players.PlayerID.Initialize();
            Players.PlayerID.LoadIDInfo();

            //ForwardPorts();

            Thread t = new Thread(new ParameterizedThreadStart(LoadServerBackground));
            t.Start();

            if (!string.IsNullOrEmpty(Settings.DiscordBotToken)) {
                var discordThread = new Thread(new ThreadStart(() =>
                {
                    new DiscordManager().Run(Settings.DiscordBotToken);
                }));
                discordThread.Start();
            }
        }

        private static async Task ForwardPorts()
        {
            var discoverer = new NatDiscoverer();
            var cts = new CancellationTokenSource(5000);
            var device = await discoverer.DiscoverDeviceAsync(PortMapper.Upnp, cts);

            await device.CreatePortMapAsync(new Mapping(Protocol.Tcp, Settings.GamePort, Settings.GamePort, "Temporary"));
        }

        private static void LoadServerBackground(Object param) {
            Migrations.MigrationRunner.MigrateDatabase();

            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(LoadDatasBackground));
            t.Name = "Data Load Thread";
            t.Start();

            resetEvent.WaitOne();

            ServerConsole.WriteLine("Initializing TCP...");

            NetworkManager.Initialize();
            NetworkManager.TcpListener.Listen(System.Net.IPAddress.Any, Settings.GamePort);

            ServerConsole.WriteLine("Server loaded!");

            if (LoadComplete != null)
                LoadComplete(null, EventArgs.Empty);
        }

        private static void LoadDatasBackground(object data) {
            //ThreadManager.SetMaxThreads(Settings.MaxWorkerThreads, Settings.MaxCompletionThreads);
            // Load pokedex
            //Pokedex.Pokedex.LoadUpdate += delegate(System.Object o, Server.LoadingUpdateEventArgs e) { ServerConsole.WriteLine("Loading Pokedex... " + e.Percent.ToString() + "%"); };
            Pokedex.Pokedex.Initialize();
            Pokedex.Pokedex.LoadAllPokemon();
            // Load emoticons
            //Emoticons.EmoticonManagerBase.LoadUpdate += delegate(System.Object o, Server.LoadingUpdateEventArgs e) { loading.UpdateStatus("Loading Emoticons... " + e.Percent.ToString() + "%"); };
            //Emoticons.EmoticonManagerBase.Initialize(Settings.MaxEmoticons);
            //Emoticons.EmoticonManagerBase.LoadEmotions(null);
            // Load exp
            ServerConsole.WriteLine("Loading Exp...");
            //Exp.ExpManager.LoadUpdate += delegate(System.Object o, Server.LoadingUpdateEventArgs e) { ServerConsole.WriteLine("Loading Experience values... " + e.Percent.ToString() + "%"); };
            Exp.ExpManager.Initialize();
            Exp.ExpManager.LoadExps(null);
            // Load maps
            ServerConsole.WriteLine("Loading Maps...");
            //Maps.MapManager.LoadUpdate += delegate(System.Object o, Server.LoadingUpdateEventArgs e) { ServerConsole.WriteLine("Loading Maps... " + e.Percent.ToString() + "%"); };
            Maps.MapManager.Initialize();
            //Maps.MapManager.LoadMaps(null);
            // Load items
            ServerConsole.WriteLine("Loading Items...");
            //Items.ItemManager.LoadUpdate += delegate(System.Object o, Server.LoadingUpdateEventArgs e) { ServerConsole.WriteLine("Loading Items... " + e.Percent.ToString() + "%"); };
            Items.ItemManager.Initialize();
            Items.ItemManager.LoadItems(null);
            // Load npcs
            ServerConsole.WriteLine("Loading Npcs...");
            //Npcs.NpcManager.LoadUpdate += delegate(System.Object o, Server.LoadingUpdateEventArgs e) { ServerConsole.WriteLine("Loading Npcs... " + e.Percent.ToString() + "%"); };
            Npcs.NpcManager.Initialize();
            Npcs.NpcManager.LoadNpcs(null);
            // Load shops
            ServerConsole.WriteLine("Loading Shops...");
            //Shops.ShopManager.LoadUpdate += delegate(System.Object o, Server.LoadingUpdateEventArgs e) { ServerConsole.WriteLine("Loading Shops... " + e.Percent.ToString() + "%"); };
            Shops.ShopManager.Initialize();
            Shops.ShopManager.LoadShops(null);
            // Load moves
            ServerConsole.WriteLine("Loading Moves...");
            //Moves.MoveManager.LoadUpdate += delegate(System.Object o, Server.LoadingUpdateEventArgs e) { ServerConsole.WriteLine("Loading Moves... " + e.Percent.ToString() + "%"); };
            Moves.MoveManager.Initialize();
            Moves.MoveManager.LoadMoves(null);
            // Load evos
            ServerConsole.WriteLine("Loading Evolutions...");
            //Evolutions.EvolutionManager.LoadUpdate += delegate(System.Object o, Server.LoadingUpdateEventArgs e) { ServerConsole.WriteLine("Loading Evolutions... " + e.Percent.ToString() + "%"); };
            Evolutions.EvolutionManager.Initialize();
            Evolutions.EvolutionManager.LoadEvos(null);
            // Load stories
            ServerConsole.WriteLine("Loading Stories...");
            //Stories.StoryManager.LoadUpdate += delegate(System.Object o, Server.LoadingUpdateEventArgs e) { ServerConsole.WriteLine("Loading Stories... " + e.Percent.ToString() + "%"); };
            Stories.StoryManager.Initialize();
            Stories.StoryManager.LoadStories(null);
            // Load rdungeons
            ServerConsole.WriteLine("Loading RDungeons...");
            //RDungeons.RDungeonManager.LoadUpdate += delegate(System.Object o, Server.LoadingUpdateEventArgs e) { ServerConsole.WriteLine("Loading RDungeons... " + e.Percent.ToString() + "%"); };
            RDungeons.RDungeonManager.Initialize();
            RDungeons.RDungeonManager.LoadRDungeons(null);
            // Load dungeons
            ServerConsole.WriteLine("Loading Dungeons...");
            //Dungeons.DungeonManager.LoadUpdate += delegate(System.Object o, Server.LoadingUpdateEventArgs e) { ServerConsole.WriteLine("Loading Dungeons... " + e.Percent.ToString() + "%"); };
            Dungeons.DungeonManager.Initialize();
            Dungeons.DungeonManager.LoadDungeons(null);
            //Load wonder mail
            ServerConsole.WriteLine("Loading WonderMail...");
            //WonderMails.WonderMailManager.LoadUpdate += delegate(System.Object o, Server.LoadingUpdateEventArgs e) { ServerConsole.WriteLine("Loading WonderMail... " + e.Percent.ToString() + "%"); };
            WonderMails.WonderMailManager.Initialize();
            WonderMails.WonderMailManager.LoadMissionPools(null);

            //WonderMails.WonderMailSystem.Initialize();

            // Initialize the tournament subsystem
            Tournaments.TournamentManager.Initialize();

            // Initialize the party subsystem
            Players.Parties.PartyManager.Initialize();

            // Initialize statistics
            Statistics.PacketStatistics.Initialize();
            // Initialize logs
            Logging.Logger.Initialize();

            // Load scripts
            ServerConsole.WriteLine("Loading scripts... ");
            Scripting.ScriptManager.Initialize();
            Scripting.ScriptManager.CompileScript(IO.Paths.ScriptsFolder, true);
            ServerConsole.WriteLine("Loading script editor data...");
            Scripting.Editor.EditorHelper.Initialize();
            Scripting.ScriptManager.InvokeSub("ServerInit");

            // Finalizing load
            ServerConsole.WriteLine("Starting game loop...");
            AI.AIProcessor.InitGameLoop();

            resetEvent.Set();
            loadingComplete = true;
        }
    }
}
