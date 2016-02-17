using UnityEngine;
using System.Collections;

namespace Multiverse.Network.Packets
{
    public sealed class CMSG_HANDSHAKE : Packet
    {
        // semver version
        public enum Protocol
        {
            MAJOR = 1,
            MINOR = 2,
            MICRO = 3
        }

        public CMSG_HANDSHAKE() : base(0x0000)
        {
            this.EnsureCapacity (3);

            m_Stream.Write((byte)CMSG_HANDSHAKE.Protocol.MAJOR);
            m_Stream.Write((byte)CMSG_HANDSHAKE.Protocol.MINOR);
            m_Stream.Write((byte)CMSG_HANDSHAKE.Protocol.MICRO);
        }
    }
}
