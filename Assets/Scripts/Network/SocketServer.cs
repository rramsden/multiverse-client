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
				IAsyncResult result = m_Socket.BeginConnect( m_ServerIP, m_Port, null, null );

				bool success = result.AsyncWaitHandle.WaitOne( 5000, true );

				if (!success)
				{
					m_Socket.Close();
					throw new ApplicationException("Failed to connect to server");
				}

				Logger.Log("Connected to {0} on port {1}", m_ServerIP, m_Port);
				return true;
			}
			catch(Exception e)
			{
				Logger.Log (Logger.LogLevel.Info, "SocketServer", "Connect: {0}", e.Message);
			}
			return false;
		}

		#endregion
	}
}