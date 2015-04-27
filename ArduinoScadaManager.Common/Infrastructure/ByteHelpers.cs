using System;
using System.Linq;
using System.Text;

namespace ArduinoScadaManager.Common.Infrastructure
{
    public static class ByteHelpers
    {
        public static byte[] StringToByteArray(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static string ByteArrayToString(this byte[] ba)
        {
            return Encoding.ASCII.GetString(ba);
        }

        public static byte[] HexStringToByteArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string ByteArrayToHexString(this byte[] ba)
        {
            var bef = ba.Length;
            var hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
