using System.Collections.Generic;
using Multiverse.Utility.Debugging;
using MsgPack;

namespace Multiverse.Network.Packets
{
    public class HANDSHAKE : Packet
    {
        public HANDSHAKE() : base(0x0000) {
            var payload = new MessagePackObjectDictionary {
                { "major", (byte)Protocol.MAJOR },
                { "minor", (byte)Protocol.MINOR },
                { "patch", (byte)Protocol.PATCH }
            };

            Serialize(payload);
        }

        public HANDSHAKE(byte[] buffer) : base(buffer) { }

        #region Conversions

        public static explicit operator HANDSHAKE(byte[] buffer)
        {
            var handshake = new HANDSHAKE(buffer);
            return handshake;
        }

        #endregion
    }
}
