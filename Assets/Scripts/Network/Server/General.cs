using System;
using System.Collections.Generic;
using Multiverse.Network.Packets;
using Multiverse.Utility.Debugging;

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

            Debugger.Log("SERVER PROTOCOL: v{0}.{1}.{2}", major, minor, patch);

            if (handshake.Body["success"].AsBoolean()) {
                Debugger.Log("HANDSHAKE_OK");
            } else {
                Debugger.Log("HANDSHAKE_FAILED");
            }
        }
    }
}
