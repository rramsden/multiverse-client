using System.Collections.Generic;
using Multiverse.Network.Packets;
using Multiverse.Utility.Debugging;
using MsgPack;

namespace Multiverse.Network.Server
{
  public static class General
	{
		public static void Handshake(byte[] packet, SocketClient sockstate)
		{
        var handshake = (HANDSHAKE)(packet);

        var major = handshake.Body["major"].AsInt32();
        var minor = handshake.Body["minor"].AsInt32();
        var patch = handshake.Body["patch"].AsInt32();

        Logger.Log("SERVER PROTOCOL: v{0}.{1}.{2}", major, minor, patch);

        if (handshake.Body["success"].AsBoolean()) {
            Logger.Log("HANDSHAKE_OK");
        } else {
            Logger.Log("HANDSHAKE_FAILED");
        }
    }
	}
}
