namespace libcrc
{
    /// **********************************************************************
    /// Name: CRC-16/MODBUS    x16+x15+x2+1
    /// Poly: 0x8005
    /// Init: 0xFFFF
    /// Refin: true
    /// Refout: true
    /// Xorout: 0x0000
    ///*************************************************************************
    public class crc16_modbus_t
    {
        private ushort crc = 0xFFFF;// Initial value

        public void reset()
        { 
            crc = 0xFFFF;
        }
        public byte[] data_get()
        {
            byte[] ret = BitConverter.GetBytes(crc);
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
                        crc = (ushort)((crc >> 1) ^ 0xA001);// 0xA001 = reverse 0x8005
                    else
                        crc = (ushort)(crc >> 1);
                }
            }
            byte[] ret = BitConverter.GetBytes(crc);
            return ret;
        }
    }
}
