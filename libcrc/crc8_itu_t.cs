﻿namespace libcrc
{
    /// **********************************************************************
    /// Name: CRC-8/ITU    x8+x2+x+1
    /// Poly: 0x07
    /// Init: 0x00
    /// Refin: false
    /// Refout: false
    /// Xorout: 0x55
    ///*************************************************************************
    public class crc8_itu_t
    {
        private byte crc = 0;// Initial value

        public void reset()
        { 
            crc = 0;
        }
        public byte data_get()
        {
            return crc;
        }
        public byte block_calculate(byte[] buffer, uint start = 0, uint len = 0)
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
                    if ((crc & 0x80) > 0)
                        crc = (byte)((crc << 1) ^ 0x07);
                    else
                        crc = (byte)(crc << 1);
                }
            }
            crc = (byte)(crc ^ 0x55);
            return crc;
        }
    }
}
