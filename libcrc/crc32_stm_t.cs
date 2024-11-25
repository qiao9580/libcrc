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
            byte[] buffer_reverse = buffer.Skip((int)start).Take((int)length).ToArray();
            for (uint i = start; i < length; i += 4)
            {
                Array.Reverse(buffer_reverse, (int)i, 4);
            }

            for (uint i = start; i < length; i++)
            {
                for (uint j = 0; j < 8; j++)
                {
                    bool bit = ((buffer_reverse[i] >> (7 - (int)j) & 1) == 1);
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
            return crc;
        }
    }
}
