using System.Collections;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Multiverse.Network;
using Multiverse.Utility.Debugging;

namespace Multiverse.Network
{
	public class SocketServer
	{
		#region Private Members

		private IPAddress m_ServerIP;
		private int m_Port;
		private Socket m_Socket;

		private byte[] m_bRecvBuffer = new byte[0x1000];
		private byte[] m_bPacketStream = new byte[0];

		#endregion

		#region Constructors

		public SocketServer(IPAddress address, int port)
		{
			this.m_ServerIP = address;
			this.m_Port = port;
		}

		#endregion

		#region Properties

		public Socket Socket { get { return m_Socket; } }

		#endregion

		#region Public Methods

		public bool Connect() {
			try
			{
				// Initialize Packet Factory
				PacketHandler.Initialize();

				// Connect Socket
				m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				IAsyncResult result = m_Socket.BeginConnect( m_ServerIP, m_Port, new AsyncCallback(OnConnect), null );

				bool success = result.AsyncWaitHandle.WaitOne( 5000, true );

				if (!success)
				{
					m_Socket.Close();
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

		public void OnConnect(IAsyncResult async)
		{
			try
			{
				// Complete the connection
				m_Socket.EndConnect(async);

				Logger.Log("Client connected to {0}",  m_Socket.RemoteEndPoint);

				// Setup BeginReceive Callback
				m_Socket.BeginReceive(m_bRecvBuffer, 0, 0x1000, SocketFlags.None, new AsyncCallback(OnDataReceive), null);
			}
			catch (SocketException e)
			{
				Logger.Log (Logger.LogLevel.Info, "SocketServer", "OnConnect: {0}", e.Message);
			}
		}

		public void OnDataReceive(IAsyncResult async)
		{
			int numRecvBytes = 0;

			try
			{
				numRecvBytes = m_Socket.EndReceive(async);
				byte[] newData = new byte[numRecvBytes];

				Buffer.BlockCopy(m_bRecvBuffer, 0, newData, 0, numRecvBytes);

				m_bPacketStream = newData;

				Logger.Log("Received {0} bytes", numRecvBytes);

				ProcessPacket(m_bRecvBuffer);
			}
			catch (SocketException e)
			{
				m_Socket.Disconnect (true);
				Logger.Log (Logger.LogLevel.Info, "SocketServer", "OnDataReceive: {0}", e.Message);
			}

			// Return to Listening State
			m_Socket.BeginReceive(m_bRecvBuffer, 0, 0x1000, SocketFlags.None, new AsyncCallback(OnDataReceive), null);
		}

		public void ProcessPacket(byte[] buffer)
		{
			Logger.Log("Received Packet {0}", buffer);
		}

		#endregion
	}

}