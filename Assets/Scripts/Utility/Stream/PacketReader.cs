using System;
using System.Text;
using System.IO;
using Multiverse.Utility.Debugging;
using MiscUtil.IO;
using MiscUtil.Conversion;

namespace Multiverse.Utility.Stream
{
    public class PacketReader
    {
        private MemoryStream m_Stream;
        private EndianBinaryReader m_Binary;

        private static Encoding m_UTF8;

        public static Encoding UTF8
        {
            get
            {
                if (m_UTF8 == null)
                    m_UTF8 = new UTF8Encoding(false, false);

                return m_UTF8;
            }
        }

        public PacketReader(byte[] data, int size, bool fixedSize)
        {
            m_Stream = new MemoryStream(data);
            m_Binary = new EndianBinaryReader(EndianBitConverter.Big, m_Stream);
        }

        public MemoryStream Stream
        {
            get
            {
                return m_Stream;
            }
        }

        public int Seek(int offset, SeekOrigin origin)
        {
            return (int)m_Stream.Seek(offset, origin);
        }

        public int ReadInt32()
        {
            return m_Binary.ReadInt32();
        }

        public short ReadInt16()
        {
            return m_Binary.ReadInt16();
        }

        public byte ReadByte()
        {
            return m_Binary.ReadByte();
        }

        public uint ReadUInt32()
        {
            return m_Binary.ReadUInt32();
        }

        public ushort ReadUInt16()
        {
            return m_Binary.ReadUInt16();
        }

        public sbyte ReadSByte()
        {
            return m_Binary.ReadSByte();
        }

        public bool ReadBoolean()
        {
            return m_Binary.ReadBoolean();
        }
    }
}
