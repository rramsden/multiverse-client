using System;
using Multiverse.Network.Packets;
using Multiverse.Utility.Debugging;

namespace Multiverse.Network.Server
{
  public static class General
	{
		public static void Handshake(byte[] packet, SocketClient sockstate)
		{
        var spkt = (SMSG_HANDSHAKE)(packet);
        Logger.Log("Server Status: {0}", spkt.Status);
        Logger.Log("Received Handshake");
		}
	}
}
