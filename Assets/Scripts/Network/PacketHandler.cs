using System;
using System.Collections.Generic;
using Multiverse.Network.Packets;
using Multiverse.Network.Server;

namespace Multiverse.Network
{
	public class PacketHandler
	{
		public delegate void RequestDelegate(byte[] x, SocketServer y);
		public static Dictionary<UInt16, RequestDelegate> OpcodeList = new Dictionary<ushort, RequestDelegate>();

		public static void Initialize()
		{
			OpcodeList.Add (0, new RequestDelegate (General.Handshake));
		}
	}
}