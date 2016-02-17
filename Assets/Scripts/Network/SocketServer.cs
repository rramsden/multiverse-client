using System.Collections;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using Multiverse.Network;
using Multiverse.Network.Packets;
using Multiverse.Utility.Stream;
using Multiverse.Utility.Debugging;

namespace Multiverse.Network
{
    public class SocketClient
    {
        #region Private Members

        private string m_ServerIP;
        private int m_Port;

        private TcpClient m_Socket;
        private Stream m_Stream;

        private byte[] m_bRecvBuffer = new byte[0x1000];
        private byte[] m_bPacketStream = new byte[0];

        private int m_MaxPacketSize = 0x1000;
        private int m_HeaderSize = 6;

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
                m_Stream.BeginRead(m_bRecvBuffer, 0, m_MaxPacketSize, new AsyncCallback(EndDataReceive), null);

                if (m_Socket.Connected) {
                    Logger.Log("Connected to {0} on {1}", m_ServerIP, m_Port);	
                } else {
                    Disconnect();
                    throw new ApplicationException("Failed to connect to server");
                }

                return true;
            }
            catch(Exception e)
            {
                Logger.Log (Logger.LogLevel.Info, "SocketServer", "Connect: {0}", e.Message);
            }
            return false;
        }

        private void EndDataReceive(IAsyncResult async)
        {
            int numRecvBytes = 0;

            try
            {
                numRecvBytes = m_Stream.EndRead(async);
                byte[] newData = new byte[numRecvBytes];

                Buffer.BlockCopy(m_bRecvBuffer, 0, newData, 0, numRecvBytes);

                m_bPacketStream = newData;

                // When server closes its socket 0 is sent
                if (numRecvBytes == 0)
                {
                    Disconnect();
                    return;
                }

                Logger.Log("Received {0} bytes", numRecvBytes);

                ProcessPacket(m_bRecvBuffer);
            }
            catch (SocketException e)
            {
                Disconnect ();
                Logger.Log (Logger.LogLevel.Info, "SocketServer", "OnDataReceive: {0}", e.Message);
            }

            // Return to Listening State
            m_Stream.BeginRead(m_bRecvBuffer, 0, m_MaxPacketSize, new AsyncCallback(EndDataReceive), null);
        }

        private void ProcessPacket(byte[] buffer)
        {
            int offset = 0;

            PacketReader pReader = new PacketReader (buffer, buffer.Length, true);

            // Traverse Packet
            while ((buffer.Length - offset) >= m_HeaderSize)
            {
                pReader.Seek (offset, System.IO.SeekOrigin.Begin);
                UInt16 Size = pReader.ReadUInt16 ();
                UInt16 Flag = pReader.ReadUInt16 ();
                UInt16 Opcode = pReader.ReadUInt16 ();

                Logger.Log ("Flag {0}", Flag);
                Logger.Log ("Master {0}", (UInt16)PacketFlag.Master);
                Logger.Log ("Size {0}", Size);

                if ((Flag == (UInt16)PacketFlag.Master) && (Size < m_MaxPacketSize))
                {
                    byte[] payload = new byte[Size];
                    Buffer.BlockCopy (buffer, offset, payload, 0, Size);

                    Logger.Log (Utility.Misc.HexBytes (payload));

                    // Let PacketHandler delegate the Packet
                    if (PacketHandler.OpcodeList.ContainsKey (Opcode))
                    {
                        PacketHandler.OpcodeList[Opcode](payload, this);
                        Logger.Log("Processed Opcode {0}", Opcode);
                    }

                    if (Size == 0)
                    {
                        break;
                    }
                    else
                    {
                        offset += Size;
                    }
                } 
                else
                {
                    Logger.Log ("Unrecognized Opcode {0}", Opcode);
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
            m_Stream.BeginWrite (data, 0, data.Length, new AsyncCallback (EndSend), null);
        }

        public void Disconnect()
        {
            m_Socket.Close ();
            Logger.Log ("Disconnected from {0}:{1}", m_ServerIP, m_Port);
        }

        #endregion
    }
}
