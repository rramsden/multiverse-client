using System;
using System.IO;
using System.Collections.Generic;
using Multiverse.Utility.Stream;
using MsgPack;
using MsgPack.Serialization;


namespace Multiverse.Network.Packets
{
    public enum Protocol
    {
        MAJOR = 0,
        MINOR = 0,
        PATCH = 1
    }

    public class Packet
    {
        #region Private Members

        protected PacketWriter m_Stream;
        protected PacketReader m_ReadStream;

        private readonly ushort m_Opcode;
        private ushort m_Size;
        private MessagePackObjectDictionary m_Dict;

        private bool m_Request;

        #endregion

        #region Properties

        public ushort Opcode { get { return m_Opcode; } }
        public bool Request { get { return m_Request; } }
        public byte[] Stream { get { return m_Stream.ToArray (); } }
        public MessagePackObjectDictionary Body { get { return m_Dict; } }
        public const ushort HEADER_SIZE = 4;

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
            m_Opcode = m_ReadStream.ReadUInt16 ();

            Deserialize();

            m_Request = true;
        }

        public void EnsureCapacity(ushort length)
        {
            m_Stream = new PacketWriter((length + HEADER_SIZE));
            m_Stream.Write((ushort)(length + HEADER_SIZE));
            m_Stream.Write(m_Opcode);
        }

        public void Serialize(MessagePackObjectDictionary body)
        {
            var serializer = MessagePackSerializer.Get<MessagePackObjectDictionary>();
            var stream = new MemoryStream();
            serializer.Pack(stream, body);
            var buffer = stream.GetBuffer();
            EnsureCapacity((ushort)stream.Length);
            m_Stream.Write(buffer, 0, (int)stream.Length);
        }

        public void Deserialize()
        {
            var serializer = MessagePackSerializer.Get<MessagePackObjectDictionary>();
            m_Dict = serializer.Unpack(m_ReadStream.Stream);
        }

        #endregion
    }
}
