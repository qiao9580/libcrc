namespace libcrc
{
    /// **********************************************************************
    /// Name: CRC32    x32+x26+x23+x22+x16+x12+x11+x10+x8+x7+x5+x4+x2+x+1
    /// Poly: 0x04C11DB7
    /// Init: 0xFFFFFFFF
    /// Refin: true
    /// Refout: true
    /// Xorout: 0xFFFFFFFF
    ///*************************************************************************
    public class crc32_t
    {
        private uint crc = 0xFFFFFFFF;// Initial value

        public void reset()
        { 
            crc = 0xFFFFFFFF;
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
                crc ^= buffer[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 1) > 0)
                        crc = (crc >> 1) ^ 0xEDB88320;// 0xEDB88320= reverse 0x04C11DB7
                    else
                        crc = crc >> 1;
                }
            }
            byte[] ret = BitConverter.GetBytes(~crc);
            Array.Reverse(ret);
            return ret;
        }
    }
}
