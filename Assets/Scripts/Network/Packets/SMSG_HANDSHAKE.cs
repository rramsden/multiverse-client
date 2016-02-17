using System;
using System.IO;
using Multiverse.Utility.Debugging;

namespace Multiverse.Network.Packets
{
	public class SMSG_HANDSHAKE : Packet
	{
		public SMSG_HANDSHAKE(byte[] packet) : base(packet) { }

    #region Properties

    public int Status
    {
        get
        {
            return m_ReadStream.ReadInt32();
        }
    }

    #endregion

		#region Conversions

		public static explicit operator SMSG_HANDSHAKE(byte[] packet)
		{
			var pkt = new SMSG_HANDSHAKE(packet);
			return pkt;
		}

		#endregion
	}
}
