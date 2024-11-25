namespace libcrc
{
    /// **********************************************************************
    /// Name: CRC32/MPEG-2    x32+x26+x23+x22+x16+x12+x11+x10+x8+x7+x5+x4+x2+x+1
    /// Poly: 0x04C11DB7
    /// Init: 0xFFFFFFFF
    /// Refin: false
    /// Refout: false
    /// Xorout: 0x00000000
    ///*************************************************************************
    public class crc32_mpeg_2_t
    {
        private uint crc = 0xFFFFFFFF;// Initial value

        public void reset()
        { 
            crc = 0xFFFFFFFF;
        }
        public uint data_get()
        {
            return crc;
        }
        public uint block_calculate(byte[] buffer, uint start = 0, uint len = 0)
        {
            if (buffer == null || buffer.Length == 0) return 0;
            if (len == 0) len = (uint)buffer.Length - start;
            uint length = start + len;
            if (length > buffer.Length) return 0;
            for (uint i = start; i < length; i++)
            {
                crc ^= (uint)(buffer[i] << 24);
                for (uint j = 0; j < 8; j++)
                {
                    if ((crc & 0x80000000) > 0)
                        crc = (crc << 1) ^ 0x04C11DB7;
                    else
                        crc = crc << 1;
                }
            }
            return crc;
        }
    }
}
