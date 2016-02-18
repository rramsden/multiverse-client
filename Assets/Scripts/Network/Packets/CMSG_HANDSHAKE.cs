using UnityEngine;
using System.Collections;

namespace Multiverse.Network.Packets
{
    public sealed class CMSG_HANDSHAKE : Packet
    {
        public static string Version
        {
            get {
                return string.Format("{0}.{1}.{2}", (int)Protocol.MAJOR, (int)Protocol.MINOR, (int)Protocol.PATCH);
            }
        }

        public CMSG_HANDSHAKE() : base(0x0000)
        {
            this.EnsureCapacity (3);

            m_Stream.Write((byte)Protocol.MAJOR);
            m_Stream.Write((byte)Protocol.MINOR);
            m_Stream.Write((byte)Protocol.PATCH);
        }
    }
}
