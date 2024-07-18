namespace libcrc
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
                    if ((crc & 0x80) > 0)
                        crc = (byte)((crc << 1) ^ 0x07);
                    else
                        crc = (byte)(crc << 1);
                }
            }
            return new byte[] { (byte)(crc ^ 0x55) };
        }
    }
}
