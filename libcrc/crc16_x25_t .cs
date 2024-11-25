namespace libcrc
{
    /// **********************************************************************
    /// Name: CRC-16/X25    x16+x12+x5+1
    /// Poly: 0x1021
    /// Init: 0xFFFF
    /// Refin: true
    /// Refout: true
    /// Xorout: 0xFFFF
    ///*************************************************************************
    public class crc16_x25_t
    {
        private ushort crc = 0xFFFF;// Initial value

        public void reset()
        { 
            crc = 0xFFFF;
        }
        public ushort data_get()
        {
            return crc;
        }
        public ushort block_calculate(byte[] buffer, uint start = 0, uint len = 0)
        {
            if (buffer == null || buffer.Length == 0) return 0;
            if (len == 0) len = (uint)buffer.Length - start;
            uint length = start + len;
            if (length > buffer.Length) return 0;
            for (uint i = start; i < length; i++)
            {
                crc ^= buffer[i];
                for (uint j = 0; j < 8; j++)
                {
                    if ((crc & 1) > 0)
                        crc = (ushort)((crc >> 1) ^ 0x8408);// 0x8408 = reverse 0x1021
                    else
                        crc = (ushort)(crc >> 1);
                }
            }
            crc = (ushort)~crc;
            return crc;
        }
    }
}
