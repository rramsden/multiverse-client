using System.IO;
using MiscUtil.IO;
using MiscUtil.Conversion;

namespace Multiverse.Utility.Stream
{
    public class PacketWriter
    {
        private readonly MemoryStream m_Stream;
        private EndianBinaryWriter m_Writer;

        #region Properties

        public MemoryStream Stream { get { return m_Stream; } }

        #endregion

        public PacketWriter(int size)
        {
            m_Stream = new MemoryStream(size);
            m_Writer = new EndianBinaryWriter(EndianBitConverter.Big, m_Stream);
        }

        public void Write(bool value)
        {
            m_Writer.Write(value);
        }

        public void Write(byte value)
        {
            m_Writer.Write(value);
        }

        public void Write(sbyte value)
        {
            m_Writer.Write(value);
        }

        public void Write(short value)
        {
            m_Writer.Write(value);
        }

        public void Write(ushort value)
        {
            m_Writer.Write(value);
        }

        public void Write(int value)
        {
            m_Writer.Write(value);
        }

        public void Write(uint value)
        {
            m_Writer.Write(value);
        }

        public void Write(byte[] buffer, int offset, int size)
        {
            m_Writer.Write(buffer, offset, size);
        }

        public byte[] ToArray()
        {
            return m_Stream.ToArray();
        }
    }
}
