using System;
using System.IO;
using Multiverse.Utility.Debugging;

namespace Multiverse.Network.Packets
{
	public class SMSG_HANDSHAKE : Packet
	{
		public SMSG_HANDSHAKE(byte[] packet) : base(packet) { }

    public enum Protocol
    {
        INCOMPATIBLE = 0, 
        SUCCESS = 1,
        UPDATE_AVAILABLE = 2
    }

    #region Properties

    public int Status
    {
        get
        {
            return m_ReadStream.ReadByte();
        }
    }

    public string Version
    {
        get
        {
            var major = m_ReadStream.ReadByte();
            var minor = m_ReadStream.ReadByte();
            var micro = m_ReadStream.ReadByte();
            return String.Format("{0}.{1}.{2}", major, minor, micro);
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
