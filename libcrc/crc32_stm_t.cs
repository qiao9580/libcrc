using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libcrc
{
    public class crc32_stm_t
    {
        private uint crc = 0xFFFFFFFF;// Initial value

        public void reset()
        {
            crc = 0xFFFFFFFF;
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
                for (int j = 0; j < 8; j++)
                {
                    bool bit = ((buffer[i] >> (7 - j) & 1) == 1);
                    bool c31 = ((crc >> 31 & 1) == 1);
                    crc <<= 1;
                    if (c31 ^ bit)
                    {
                        crc ^= 0x04C11DB7;
                    }
                }
            }
            crc &= 0xFFFFFFFF;
            crc ^= 0x00000000;
            byte[] ret = BitConverter.GetBytes(crc);
            return ret;
        }
    }
}
