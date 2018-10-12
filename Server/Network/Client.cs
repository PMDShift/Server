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


//#define EVENTTHREAD

namespace Server.Network
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    using PMDCP.Core;
    using Tcp = PMDCP.Sockets.Tcp;
    using Udp = PMDCP.Sockets.Udp;
    using Server.Players;
    using PMDCP.Sockets;
    using Server.Players.Parties;
    using Server.Processing;
    using PMDCP.DatabaseConnector.MySql;
    using Server.Database;

    public class Client
    {
        #region Fields

        Tcp.TcpClient tcpClient;
        Udp.UdpClient udpClient;
        TcpClientIdentifier tcpID;
        PacketModifiers packetModifiers;
        Player player;
        string macAddress;
        string biosId;
#if EVENTTHREAD
        PlayerEventThread eventThread;
#endif

        #endregion Fields

        bool isInitialized = false;

        #region Constructors

        public Client(TcpClientIdentifier tcpID, Tcp.TcpClient tcpClient)
        {
            this.tcpID = tcpID;
            this.tcpClient = tcpClient;
            this.tcpClient.CustomHeaderSize = GetCustomPacketHeaderSize();
            packetModifiers = new PacketModifiers();

            //database = new MySql("localhost", 3306, "test", "root", "test");

#if EVENTTHREAD
            eventThread = new PlayerEventThread(this);
#endif

            AddEventHandlers();

            SetupPacketSecurity();
        }

        #endregion Constructors

        #region Properties

        public Tcp.TcpClient TcpClient
        {
            get { return tcpClient; }
        }

        public TcpClientIdentifier TcpID
        {
            get { return tcpID; }
        }

        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        public PacketModifiers PacketSecurity
        {
            get { return packetModifiers; }
        }

        public IPAddress IP
        {
            get { return ((IPEndPoint)tcpClient.Socket.RemoteEndPoint).Address; }
        }

        public string MacAddress
        {
            get { return macAddress; }
        }

        public string BiosId
        {
            get { return biosId; }
        }

        public string ClientEdition { get; private set; }

        #endregion Properties

        public void InitializeClientSystem()
        {
            if (!isInitialized)
            {
                isInitialized = true;
                player = new Players.Player(this);
            }
        }

        private int GetCustomPacketHeaderSize()
        {
            return
                1 // [byte] Compression enabled
                + 1 // [byte] Encryption enabled
                + 1 // [byte] Send as packet list
                ;
        }

        private void AddEventHandlers()
        {
            // Add tcp event handlers
            tcpClient.DataReceived += Tcp_DataReceived;
            tcpClient.ConnectionBroken += tcpClient_ConnectionBroken;
        }

        private void RemoveEventHandlers()
        {
            //remove tcp event handlers
            tcpClient.DataReceived -= Tcp_DataReceived;
            tcpClient.ConnectionBroken -= tcpClient_ConnectionBroken;
        }

        void tcpClient_ConnectionBroken(object sender, EventArgs e)
        {
            //lock (this) {
            if (isInitialized && player != null)
            {
                if (this.Player.LoggingOut == false)
                {
                    this.Player.LoggingOut = true;
                    try
                    {
                        if (IsPlaying())
                        {
                            // Party system
                            if (player.PartyID != null)
                            {
                                //PartyManager.RemoveFromParty(PartyManager.FindPlayerParty(this), this);
                                PartyManager.HandleMemberLogout(player.PartyID, player.CharID);
                            }
                            // Tournament system
                            if (player.Tournament != null)
                            {
                                Tournaments.Tournament tourny = player.Tournament;
                                // Check if the player is competing with someone
                                if (player.TournamentMatchUp != null)
                                {
                                    player.TournamentMatchUp.EndMatchUp(player.TournamentMatchUp.SelectOtherMember(this).Client.Player.CharID);
                                }
                                tourny.RemoveRegisteredPlayer(this);
                            }
                            Scripting.ScriptManager.InvokeSub("LeftGame", this);

                            this.Player.Statistics.HandleLogout();
                            this.Player.Statistics.Save();

                            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Players))
                            {
                                player.SaveCharacterData(dbConnection);
                            }
                            //if (!(player.Map.MapType == Enums.MapType.Instanced || player.Map.MapType == Enums.MapType.RDungeonMap)) {
                            //    player.Map.PlayersOnMap.Remove(player.CharID);
                            //}
                            Messenger.SendLeaveMap(this, player.Map, true, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        ServerConsole.WriteLine(ex.ToString());
                    }
                    finally
                    {
                        //Players.PlayerID.RemovePlayerFromIndexList(tcpID);


#if EVENTTHREAD
                        eventThread.HandleClientDisconnect();
#endif
                    }
                }
                //}
            }
            RemoveEventHandlers();
            ClientManager.RemoveClient(this);
        }

        void Tcp_DataReceived(object sender, Tcp.DataReceivedEventArgs e)
        {
            try
            {
                bool compression = false;
                if (e.CustomHeader[0] == 1)
                {
                    compression = true;
                }
                bool encryption = false;
                if (e.CustomHeader[1] == 1)
                {
                    encryption = true;
                }
                byte[] packetBytes = e.ByteData;
                if (compression)
                {
                    packetBytes = packetModifiers.DecompressPacket(packetBytes);
                }
                if (encryption)
                {
                    packetBytes = packetModifiers.DecryptPacket(packetBytes);
                }
                if (e.CustomHeader[2] == 1)
                {
                    // This was a packet list, process it
                    int position = 0;
                    while (position < packetBytes.Length)
                    {
                        int segmentSize = ByteEncoder.ByteArrayToInt(packetBytes, position);
                        position += 4;
#if EVENTTHREAD
                        PlayerEvent playerEvent = new PlayerEvent(ByteEncoder.ByteArrayToString(packetBytes, position, segmentSize));
                        eventThread.AddEvent(playerEvent);
#else

                        MessageProcessor.ProcessData(this, ByteEncoder.ByteArrayToString(packetBytes, position, segmentSize));
#endif
                        position += segmentSize;
                    }
                }
                else
                {
#if EVENTTHREAD
                    PlayerEvent playerEvent = new PlayerEvent(PMDCP.Core.ByteEncoder.ByteArrayToString(packetBytes));
                    eventThread.AddEvent(playerEvent);
#else
                    MessageProcessor.ProcessData(this, PMDCP.Core.ByteEncoder.ByteArrayToString(packetBytes));
#endif
                }
            }
            catch (Exception ex)
            {
                Exceptions.ErrorLogger.WriteToErrorLog(ex, "Tcp_DataRecieved");
            }
        }

        public bool IsPlaying()
        {
            return (player != null && player.LoggedIn);
        }

        public void CloseConnection()
        {
            if (TcpClient.Socket.Connected)
            {
                TcpClient.Socket.Close();
            }
            else
            {
                tcpClient_ConnectionBroken(null, EventArgs.Empty);
            }
        }

        public string ConnectionID
        {
            get { return player.CharID; }
        }

        private void SetupPacketSecurity()
        {
            Messenger.SendDataTo(this, TcpPacket.CreatePacket("cryptkey", "----" + Server.Math.Rand(1, 20000)));
            packetModifiers.SetKey(null);
        }

        internal void SetMacAddress(string macAddress)
        {
            this.macAddress = macAddress;
        }

        internal void SetBiosIdentification(string biosId)
        {
            this.biosId = biosId;
        }

        internal void SetClientEdition(string clientEdition)
        {
            this.ClientEdition = clientEdition;
        }
    }
}
