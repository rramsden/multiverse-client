using System;
using Multiverse.Network.Packets;
using Multiverse.Utility.Debugging;

namespace Multiverse.Network.Server
{
  public static class General
	{
		public static void Handshake(byte[] packet, SocketClient sockstate)
		{
        var handshake = (SMSG_HANDSHAKE)(packet);

        if (handshake.Status == (int)SMSG_HANDSHAKE.Protocol.SUCCESS) {
            Logger.Log("SERVER VERSION: {0}", handshake.Version);
            Logger.Log("HANDSHAKE_COMPLETE");
        } else {
            Logger.Log("SERVER VERSION: {0}", handshake.Version);
            Logger.Log("HANDSHAKE_FAILED");
        }
		}
	}
}
