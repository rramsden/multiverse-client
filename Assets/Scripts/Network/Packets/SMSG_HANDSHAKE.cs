using System;

namespace Multiverse.Network.Packets
{
	public class SMSG_HANDSHAKE : Packet
	{
		public SMSG_HANDSHAKE(byte[] packet) : base(packet) { }

		#region Conversions

		public static explicit operator SMSG_HANDSHAKE(byte[] packet)
		{
			SMSG_HANDSHAKE pkt = new SMSG_HANDSHAKE(packet);
			return pkt;
		}

		#endregion
	}
}