using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hextobin_wpf
{
    public static class Utils
    {
        /// <summary>
        /// Binary String To Hex String
        /// </summary>
        /// <param name="binary"></param>
        /// <returns></returns>
        public static string BinaryStringToHexString(string binary)
        {
            if (string.IsNullOrEmpty(binary))
                return binary;

            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }

        /// <summary>
        /// check the hex values
        /// </summary>
        /// <param name="hexvalue"></param>
        /// <returns></returns>
        public static bool OnlyHexInString(string hexvalue)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(hexvalue, @"\A\b[0-9a-fA-F]+\b\Z");
        }
        
        /// <summary>
        /// string to byte
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string hex)
        {
            if ((hex.Length % 2) != 0)
                hex = "0" + hex;
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        /// <summary>
        /// ByteToBinary
        /// </summary>
        /// <param name="byteValue"></param>
        /// <returns></returns>
        public static string ByteToBinary(byte byteValue)
        {
            string binaryString = Convert.ToString(byteValue, 2).PadLeft(8, '0');
            return binaryString;
        }

        /// <summary>
        /// create the dictionary for byteArray To Binary
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ByteArrayToBinary(byte[] byteArray)
        {
            string output = "";
            Dictionary<string, string> outList = new Dictionary<string, string>();
            int i = 1;
            foreach (byte byteValue in byteArray)
            {
                output = ByteToBinary(byteValue);
                string key = "Byte" + i;
                outList.Add(key, output);
                i++;
                
            }
            return outList;
        }
    }
}
