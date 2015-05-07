using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls.Primitives;

namespace ArduinoScadaManager.Common.Infrastructure
{
    public static class ByteHelpers
    {
        public static byte[] StringToByteArray(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static string ByteArrayToString(this byte[] byteArray)
        {
            return Encoding.ASCII.GetString(byteArray);
        }

        public static byte[] HexStringToByteArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string ByteArrayToHexString(this byte[] byteArray)
        {
            var hex = new StringBuilder(byteArray.Length * 2);
            foreach (byte b in byteArray)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static byte[] ToByteArray(this BitArray bitArray)
        {
            byte numOfDataBytes = (byte) Math.Ceiling(bitArray.Length/8.0f);
            var bitsDataArray = new byte[numOfDataBytes];

            for (int i = 0; i < bitArray.Length; i++)
            {
                var currDataArrayOffset = i/8;
                byte currByteOffset = (byte) (i%8);

                if((bitArray[i]))
                    bitsDataArray[currDataArrayOffset] |= (byte)(1 << (currByteOffset));
            }

            return bitsDataArray;
        }

        public static uint GetUint16(this byte[] data, int position)
        {
            var tempArray = new[] {data[position + 1], data[position]};
            return BitConverter.ToUInt16(tempArray, 0);
        }

        public static BitArray Get(this BitArray bitArray, int startIndex, int length)
        {
            var toReturn = new BitArray(length);
            for (int i = 0; i < length; i++)
            {
                toReturn[i] = bitArray[i + startIndex];
            }

            return toReturn;
        }
    }
}
