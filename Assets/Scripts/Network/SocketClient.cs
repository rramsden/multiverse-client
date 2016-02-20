using System;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using Multiverse.Network;
using Multiverse.Network.Packets;
using Multiverse.Utility.Stream;
using Multiverse.Utility.Debugging;

namespace Multiverse.Network
{
    public class SocketClient
    {

        #region Public Members

        public Queue<byte[]> PacketQueue = new Queue<byte[]>();

        #endregion

        #region Private Members

        private readonly string m_ServerIP;
        private readonly int m_Port;

        private TcpClient m_Socket;
        private Stream m_Stream;

        private byte[] m_bRecvBuffer = new byte[0x1000];
        private byte[] m_bPacketStream = new byte[0];

        private const int MAX_PACKET_SIZE = 0x1000;
        private const int HEADER_SIZE = 6;

        #endregion

        #region Constructors

        public SocketClient(string address, int port)
        {
            this.m_ServerIP = address;
            this.m_Port = port;
        }

        #endregion

        #region Public Methods

        public bool Connect() {
            try
            {
                // Initialize Packet Factory
                PacketHandler.Initialize();

                // Connect Socket
                m_Socket = new TcpClient(m_ServerIP, m_Port);
                m_Stream = m_Socket.GetStream();
                m_Stream.BeginRead(m_bRecvBuffer, 0, MAX_PACKET_SIZE, new AsyncCallback(EndDataReceive), null);

                if (m_Socket.Connected) {
                    Debugger.Log("Connected to {0} on {1}", m_ServerIP, m_Port);	
                } else {
                    Disconnect();
                    throw new ApplicationException("Failed to connect to server");
                }

                return true;
            }
            catch(Exception e)
            {
                Debugger.Log (Debugger.LogLevel.Info, "SocketServer", "Connect: {0}", e.Message);
            }
            return false;
        }

        private void EndDataReceive(IAsyncResult async)
        {
            int numRecvBytes;

            try
            {
                numRecvBytes = m_Stream.EndRead(async);
                var newData = new byte[numRecvBytes];

                Buffer.BlockCopy(m_bRecvBuffer, 0, newData, 0, numRecvBytes);

                m_bPacketStream = newData;

                // When server closes its socket 0 is sent
                if (numRecvBytes == 0)
                {
                    Disconnect();
                    return;
                }

                Debugger.Log("Received {0} bytes", numRecvBytes);
                Debugger.Log(Utility.Misc.HexBytes(m_bPacketStream));

                // Process the packet
                ProcessPacket(m_bPacketStream);
                ProcessQueue();
            }
            catch (SocketException e)
            {
                Disconnect ();
                Debugger.Log (Debugger.LogLevel.Info, "SocketServer", "EndDataReceive: {0}", e.Message);
            }
            catch (Exception e)
            {
                Debugger.Log ("{0}:\n{1}", e.Message, e.StackTrace);
            }

            // Return to Listening State
            m_Stream.BeginRead(m_bRecvBuffer, 0, MAX_PACKET_SIZE, new AsyncCallback(EndDataReceive), null);
        }

        private void ProcessPacket(byte[] buffer)
        {
            int offset = 0;

            var pReader = new PacketReader (buffer, buffer.Length, true);

            // Traverse Packet
            while ((buffer.Length - offset) >= HEADER_SIZE)
            {
                pReader.Seek (offset, SeekOrigin.Begin);
                UInt16 Size = pReader.ReadUInt16 ();
                UInt16 Flag = pReader.ReadUInt16 ();
                UInt16 Opcode = pReader.ReadUInt16 ();

                if ((Flag == (UInt16)PacketFlag.Master) && (Size < MAX_PACKET_SIZE))
                {
                    var payload = new byte[Size];
                    Buffer.BlockCopy (buffer, offset, payload, 0, Size);

                    // Let PacketHandler delegate the Packet
                    if (PacketHandler.OpcodeList.ContainsKey (Opcode))
                    {
                        PacketHandler.OpcodeList[Opcode](payload, this);
                    }

                    if (Size == 0) {break;}
                    offset += Size;
                } 
                else
                {
                    Debugger.Log ("Unrecognized Opcode {0}", Opcode);
                    break;
                }
            }
        }

        private void EndSend(IAsyncResult async)
        {
            m_Stream.EndWrite (async);
        }

        public void Send(byte[] data)
        {
            Debugger.Log("<- SERVER");
            Debugger.Log(Utility.Misc.HexBytes(data));
            m_Stream.BeginWrite (data, 0, data.Length, new AsyncCallback (EndSend), null);
        }

        public void ProcessQueue()
        {
            while(PacketQueue.Count > 0)
            {
                byte[] packet = PacketQueue.Dequeue();
                Debugger.Log("Processing Packet:");
                Debugger.Log (Utility.Misc.HexBytes (packet));
                Send(packet);
            }
        }

        public void Disconnect()
        {
            m_Socket.Close ();
            Debugger.Log ("Disconnected from {0}:{1}", m_ServerIP, m_Port);
        }

        #endregion
    }
}
