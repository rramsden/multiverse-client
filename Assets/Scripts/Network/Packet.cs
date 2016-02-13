using UnityEngine;
using System.Collections;
using Multiverse.Utility.Stream;

namespace Multiverse.Network.Packets
{
	public abstract class Packet
	{
		#region Private Members

		protected PacketWriter m_Stream;
		protected PacketReader m_ReadStream;

		private ushort m_Opcode;
		private ushort m_Size;
		private ushort m_Flag;

		private bool m_Request = false;

		#endregion

		#region Properties

		public ushort Opcode { get { return m_Opcode; } }
		public bool Request { get { return m_Request; } }

		#endregion

		#region Public Methods

		public Packet(ushort opcode)
		{
			m_Opcode = opcode;
		}

		public Packet(byte[] buffer)
		{
			m_ReadStream = new PacketReader (buffer, buffer.Length, true);

			m_Size = m_ReadStream.ReadUInt16 ();
			m_Flag = m_ReadStream.ReadUInt16 ();
			m_Opcode = m_ReadStream.ReadUInt16 ();

			m_Request = true;
		}

		#endregion
	}
}