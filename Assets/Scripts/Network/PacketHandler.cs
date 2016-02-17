using System;
using System.Collections.Generic;
using Multiverse.Network.Packets;
using Multiverse.Network.Server;
using Multiverse.Utility.Debugging;

namespace Multiverse.Network
{
	public class PacketHandler
	{
		public delegate void RequestDelegate(byte[] x, SocketClient y);
		public static Dictionary<UInt16, RequestDelegate> OpcodeList = new Dictionary<ushort, RequestDelegate>();

		public static void Initialize()
		{
			OpcodeList.Add (0, new RequestDelegate (General.Handshake));
			Logger.Log(Logger.LogLevel.Info, "Packet Monitor", "Monitoring {0} packets.", OpcodeList.Count);
		}
	}
}
