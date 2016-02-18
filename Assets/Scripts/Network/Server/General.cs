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
        var status = handshake.Status;
        Logger.Log("CLIENT VERSION: {0}", CMSG_HANDSHAKE.Version);
        Logger.Log("SERVER VERSION: {0}", handshake.Version);

        if (status == (int)SMSG_HANDSHAKE.Protocol.SUCCESS) {
            Logger.Log("HANDSHAKE_OK");
        } else {
            Logger.Log("PROTOCOL_NOT_SUPPORTED");
        }
		}
	}
}
