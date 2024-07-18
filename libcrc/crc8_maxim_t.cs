namespace libcrc
{
    /// **********************************************************************
    /// Name: CRC-8/MAXIM    x8+x5+x4+1
    /// Poly: 0x31
    /// Init: 0x00
    /// Refin: true
    /// Refout: true
    /// Xorout: 0x00
    ///*************************************************************************
    public class crc8_maxim_t
    {
        private byte crc = 0;// Initial value

        public void reset()
        { 
            crc = 0;
        }
        public byte[] data_get()
        {
            return new byte[] { crc };
        }
        public byte[] block_calculate(byte[] buffer, int start = 0, int len = 0)
        {
            if (buffer == null || buffer.Length == 0) return null;
            if (start < 0) return null;
            if (len == 0) len = buffer.Length - start;
            int length = start + len;
            if (length > buffer.Length) return null;
            for (int i = start; i < length; i++)
            {
                crc ^= buffer[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 1) > 0)
                        crc = (byte)((crc >> 1) ^ 0x8C);// 0x8C = reverse 0x31
                    else
                        crc = (byte)(crc >> 1);
                }
            }
            return new byte[] { crc };
        }
    }
}
