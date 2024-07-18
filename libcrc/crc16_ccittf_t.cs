namespace libcrc
{
    /// **********************************************************************
    /// Name: CRC-16/CCITT FALSE    x16+x12+x5+1
    /// Poly: 0x1021
    /// Init: 0xFFFF
    /// Refin: false
    /// Refout: false
    /// Xorout: 0x0000
    ///*************************************************************************
    public class crc16_ccittf_t
    {
        private ushort crc = 0xFFFF;// Initial value

        public void reset()
        { 
            crc = 0xFFFF;
        }
        public byte[] data_get()
        {
            byte[] ret = BitConverter.GetBytes(crc);
            Array.Reverse(ret);
            return ret;
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
                crc ^= (ushort)(buffer[i] << 8);
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x8000) > 0)
                        crc = (ushort)((crc << 1) ^ 0x1021);
                    else
                        crc = (ushort)(crc << 1);
                }
            }
            byte[] ret = BitConverter.GetBytes(crc);
            Array.Reverse(ret);
            return ret;
        }
    }
}
