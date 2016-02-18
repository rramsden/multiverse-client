﻿using Multiverse.Utility.Stream;
using Multiverse.Utility.Debugging;

namespace Multiverse.Network.Packets
{
    public enum Protocol
    {
        MAJOR = 0,
        MINOR = 0,
        PATCH = 1
    }

    public enum PacketFlag : ushort
    {
        Master = 0x5555
    }

    public class Packet
    {
        #region Private Members

        protected PacketWriter m_Stream;
        protected PacketReader m_ReadStream;

        private readonly ushort m_Opcode;
        private ushort m_Size;
        private ushort m_Flag;

        private bool m_Request;
        private const ushort HEADER_SIZE = 6;

        #endregion

        #region Properties

        public ushort Opcode { get { return m_Opcode; } }
        public bool Request { get { return m_Request; } }
        public byte[] Stream { get { return m_Stream.ToArray (); } }

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

        public void EnsureCapacity(ushort length)
        {
            m_Stream = PacketWriter.CreateInstance((length + HEADER_SIZE), false);
            m_Stream.Write((ushort)(length + HEADER_SIZE));
            m_Stream.Write((ushort)PacketFlag.Master);
            m_Stream.Write(m_Opcode);
        }

        #endregion
    }
}
