namespace libcrc
{
    /// **********************************************************************
    /// Name: CRC-16/DNP    x16+x13+x12+x11+x10+x8+x6+x5+x2+1
    /// Poly: 0x3D65
    /// Init: 0x0000
    /// Refin: true
    /// Refout: true
    /// Xorout: 0xFFFF
    ///*************************************************************************
    public class crc16_dnp_t
    {
        private ushort crc = 0;// Initial value

        public void reset()
        { 
            crc = 0;
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
                        crc = (ushort)((crc >> 1) ^ 0xA6BC);// 0xA6BC = reverse 0x3D65
                    else
                        crc = (ushort)(crc >> 1);
                }
            }
            crc = (ushort)~crc;
            return crc;
        }
    }
}
