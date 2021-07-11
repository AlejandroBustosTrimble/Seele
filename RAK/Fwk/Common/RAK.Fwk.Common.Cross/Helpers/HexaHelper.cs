using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RAK.Fwk.Common.Cross.Helpers
{
    /// <summary>
    /// Helper para los numeros hexadecimales
    /// </summary>
    public static class HexaHelper
    {
        public const int HEXA_CHAR_STEP = 2;

        public const int BIN_BASE = 2;

        public const int HEXA_BASE = 16;

        public const string HEXA_SEPARATOR_CHAR = "-";

        public const string HEXA_MAX_DIGIT = "F";

        public const string HEXA_DIGIT_PATTERN = "[A-F|0-9]";

        /// <summary>
        /// Convierte un string que representa una serie de numeros hexa decimales en su equivalente array de bytes
        /// Cada par de Hexa (Ej 0E) equivale a un elemento del array que es un numero decimal (ej 14)
        /// </summary>
        /// <param name="hexaStr"></param>
        /// <returns></returns>
        public static byte[] GetBytesFromHexa(string hexaStr, string separatorString = HEXA_SEPARATOR_CHAR, int formatOnDigits = HexaHelper.HEXA_CHAR_STEP)
        {
            // -- Limpio el string de los separadores que tenga
            if (!String.IsNullOrWhiteSpace(separatorString))
            {
                hexaStr = hexaStr.Replace(separatorString, String.Empty);
            }

            // -- Llevo cada numero Hexa a Decimal(1 byte)
            var bytes = new byte[hexaStr.Length / formatOnDigits];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexaStr.Substring(i * formatOnDigits, formatOnDigits), HexaHelper.HEXA_BASE);
            }

            return bytes;
        }

        /// <summary>
        /// Convierte a numero base decimal un numero hexa
        /// </summary>
        /// <param name="hexaStr"></param>
        /// <returns></returns>
        public static int ToInt32(string hexaStr)
        {
            var num = Convert.ToInt32(hexaStr, HexaHelper.HEXA_BASE);

            return num;
        }

        /// <summary>
        /// Convierte a numero base decimal un numero hexa
        /// </summary>
        /// <param name="hexaStr"></param>
        /// <returns></returns>
        public static Int64 ToInt64(string hexaStr)
        {
            var num = Convert.ToInt64(hexaStr, HexaHelper.HEXA_BASE);

            return num;
        }

        /// <summary>
        /// Intenta parsear el hexa a Int64
        /// </summary>
        /// <param name="hexaStr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryParseInt64(String hexaStr, out Int64 value)
        {
            var result = false;
            try
            {
                value = ToInt64(hexaStr);
                result = true;
            }
            catch
            {
                result = false;
                value = 0;
            }

            return result;
        }

        /// <summary>
        /// Intenta convertirlo en bytes
        /// </summary>
        /// <param name="hexaStr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryParseBytes(string hexaStr, out byte[] value)
        {
            var result = false;
            value = new byte[1];

            try
            {
                if (!String.IsNullOrEmpty(hexaStr) && (hexaStr.Length % HEXA_CHAR_STEP == 0))
                {
                    value = GetBytesFromHexa(hexaStr);

                    result = true;
                }
            }
            catch
            {
                result = false;

            }

            return result;
        }

        /// <summary>
        /// Convierte un numero base decimal a hexa
        /// </summary>
        /// <param name="decimalNumber"></param>
        /// <param name="formatOnDigits"></param>
        /// <returns></returns>
        public static string ToHexa(int decimalNumber, int formatOnDigits = 0)
        {
            return ToHexa((Int64)decimalNumber, formatOnDigits);
        }

        /// <summary>
        /// Convierte un numero base decimal a hexa
        /// </summary>
        /// <param name="decimalNumber"></param>
        /// <param name="formatOnDigits"></param>
        /// <returns></returns>
        public static String ToHexa(Int64 decimalNumber, int formatOnDigits = 0)
        {
            var format = "X";
            if (formatOnDigits > 0)
            {
                format += formatOnDigits;
            }

            var hexaNumber = decimalNumber.ToString(format);

            return hexaNumber;
        }

        /// <summary>
        /// Transforma un numero hexa a su equivalente decimal, int, etc
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hexaNumber"></param>
        /// <param name="formatOnDigits"></param>
        /// <param name="hasSign"></param>
        /// <param name="isBigEndian"></param>
        /// <returns></returns>
        public static T TransformNumber<T>(string hexaNumber, int formatOnDigits = 0, bool hasSign = true, bool isBigEndian = true)
            where T : struct
        {
            object result = default(T);
            if (!string.IsNullOrEmpty(hexaNumber))
            {
                if (formatOnDigits == 0)
                {
                    formatOnDigits = hexaNumber.Length;
                }

                if (!isBigEndian)
                {
                    result = Convert.ChangeType(HexaHelper.GetBigEndianDecimal(hexaNumber, formatOnDigits), typeof(T));
                }
                else
                {
                    result = Convert.ChangeType(HexaHelper.ToInt64(hexaNumber), typeof(T));
                }

                if (hasSign)
                {
                    decimal decValue = Convert.ToDecimal(result);

                    #region Manejo numero negativo

                    var hexMaxValue = String.Empty.PadLeft(formatOnDigits, Convert.ToChar(HexaHelper.HEXA_MAX_DIGIT));

                    var maxValue = HexaHelper.ToInt64(hexMaxValue);
                    // -- Me posiciono sobre el limite superior del numero
                    // -- Ej si la cantidad de bytes del numero es 4, son 8 numeros. El valor maximo es FFFF FFFF 
                    // -- el limite superior es 100000000
                    maxValue += (Int64)Decimal.One;
                    // -- Luego divido ese limite superior por 2, aquellos numeros que queden por debajo son positivos y los mayores
                    // -- a este limite son negativos
                    var limitValue = maxValue / 2;

                    if (decValue >= limitValue)
                    {
                        // -- Si esta por encima del limite para ser considera positivo, entonces significa que es negativo
                        // -- Entonces al maximo valor le resto el valor que quiero obtener y le cambio el signo por ser negativo
                        decValue = maxValue - decValue;
                        decValue *= Decimal.MinusOne;
                    }

                    #endregion

                    result = decValue;
                }

                result = Convert.ChangeType(result, typeof(T));
            }

            return (T)result;
        }

        /// <summary>
        /// Transforma un numero a hexa, manejando si debe realizar conversiones little/big endian y de negativos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="number"></param>
        /// <param name="formatOnDigits"></param>
        /// <param name="isBigEndian"></param>
        /// <returns></returns>
        public static string TransformNumberToHexa<T>(T number, int formatOnDigits, bool isBigEndian = true)
            where T : struct
        {
            decimal decValue = Convert.ToDecimal(number);

            #region Manejo numero negativo

            if (decValue < 0)
            {
                var hexMaxValue = String.Empty.PadLeft(formatOnDigits, Convert.ToChar(HexaHelper.HEXA_MAX_DIGIT));

                var maxValue = HexaHelper.ToInt64(hexMaxValue);
                // -- Me posiciono sobre el limite superior del numero
                // -- Ej si la cantidad de bytes del numero es 4, son 8 numeros. El valor maximo es FFFF FFFF 
                // -- el limite superior es 100000000
                maxValue += (Int64)Decimal.One;

                // -- Al valor maximo le "resto" el valor (que como esta negativo lo sumo)
                decValue = maxValue + decValue;
            }

            #endregion

            string strResult = null;

            if (!isBigEndian)
            {
                strResult = HexaHelper.GetLittleEndianHexa(Convert.ToInt64(decValue), formatOnDigits);
            }
            else
            {
                strResult = HexaHelper.ToHexa(Convert.ToInt64(decValue), formatOnDigits);
            }

            return strResult;
        }

        /// <summary>
        /// Lleva un numero hexa little Endian (formato inverso) a big Endian (Formato clasico)
        /// </summary>
        /// <param name="littleEndianHexValue"></param>
        /// <param name="formatOnDigits"></param>
        /// <returns></returns>
        public static string GetBigEndianHexa(string littleEndianHexValue, int formatOnDigits)
        {
            var trimHex = littleEndianHexValue.PadRight(formatOnDigits, '0');
            if (trimHex.Length % 2 != 0)
            {
                // -- Si le falta un 0, lo agrego del lado menos significativo
                // -- en LittleEndian son los ceros a la DERECHA, por ej 181000, esos ultimos 0 no tienen valor
                trimHex += "0";
            }

            var result = ReverseHexaNumber(trimHex);

            return result.PadLeft(formatOnDigits, '0');
        }

        /// <summary>
        /// Obtengo el valor decimal de un nro little endian en hexa
        /// </summary>
        /// <param name="littleEndianHexValue"></param>
        /// <param name="formatOnDigits"></param>
        /// <returns></returns>
        public static Decimal GetBigEndianDecimal(string littleEndianHexValue, int formatOnDigits)
        {
            var bigEndianHex = GetBigEndianHexa(littleEndianHexValue, formatOnDigits);
            var intValue = ToInt64(bigEndianHex);
            var decimalValue = Convert.ToDecimal(intValue);

            return decimalValue;
        }

        /// <summary>
        /// Lleva un numero hexa big Endian (Formato clasico) a little Endian (formato inverso)
        /// </summary>
        /// <param name="bigEndianHexValue"></param>
        /// <param name="formatOnDigits"></param>
        /// <returns></returns>
        public static string GetLittleEndianHexa(string bigEndianHexValue, int formatOnDigits)
        {
            //var trimHex = bigEndianHexValue.Trim('0');
            var trimHex = bigEndianHexValue.PadLeft(formatOnDigits, '0');
            if (trimHex.Length % 2 != 0)
            {
                // -- Si le falta un 0, lo agrego del lado menos significativo
                // -- en BigEndian son los ceros a la IZQUIERDA, por ej 001018, esos primeros 0 no tienen valor
                trimHex = "0" + trimHex;
            }

            var result = ReverseHexaNumber(trimHex);

            return result.PadRight(formatOnDigits, '0');
        }

        /// <summary>
        /// Obtiene el valor little endian de un numero entero formateado en x digitos
        /// </summary>
        /// <param name="value"></param>
        /// <param name="formatOnDigits"></param>
        /// <returns></returns>
        public static String GetLittleEndianHexa(Int64 value, int formatOnDigits)
        {
            return HexaHelper.GetLittleEndianHexa(HexaHelper.ToHexa((value), formatOnDigits), formatOnDigits);
        }

        /// <summary>
        /// Invirte un numero hexa
        /// Sirve para pasar de LittleEndiand <=> BigEndian
        /// </summary>
        /// <param name="hexaValue"></param>
        /// <returns></returns>
        private static string ReverseHexaNumber(string hexaValue)
        {
            var sb = new StringBuilder();
            for (int i = hexaValue.Length - HEXA_CHAR_STEP; i >= 0; i -= HEXA_CHAR_STEP)
            {
                sb.Append(hexaValue.Substring(i, HEXA_CHAR_STEP));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Lleva un numero binario a hexa
        /// </summary>
        /// <param name="bin"></param>
        /// <returns></returns>
        public static string BinToHex(string bin)
        {
            if (bin == null)
                throw new ArgumentNullException("bin");
            if (bin.Length % 8 != 0)
                throw new ArgumentException("The length must be a multiple of 8", "bin");

            var hex = Enumerable.Range(0, bin.Length / 8)
                             .Select(i => bin.Substring(8 * i, 8))
                             .Select(s => Convert.ToByte(s, 2))
                             .Select(b => b.ToString("x2"));
            return String.Join(null, hex);
        }

        /// <summary>
        /// Lleva un numero hexa a binario
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static string HexToBin(string hex)
        {
            // Convert.ToUInt32 this is an unsigned int
            // so no negative numbers but it gives you one more bit
            // it much of a muchness 
            // Uint MAX is 4,294,967,295 and MIN is 0
            // this padds to 4 bits so 0x5 = "0101"

            // Llevo cada par de hexa a int en base 16, luego a binario y eso lo formatea en 4 digitos
            return String.Join(String.Empty, hex.Select(c => Convert.ToString(Convert.ToUInt32(c.ToString(), HEXA_BASE), BIN_BASE).PadLeft(4, '0')));
        }

        /// <summary>
        /// Obtiene la representacion de un array de bytes en su equivalente Hexadecimal
        /// Cada elemento del array de bytes (Ej 11) equivale a un elemento del array de string en Hexa (Ej 0B)
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string[] GetHexaFromBytes(byte[] bytes, int formatOnDigits = HexaHelper.HEXA_CHAR_STEP)
        {
            var hexaList = new List<string>();
            for (int i = 0; i < bytes.Length; i++)
            {
                hexaList.Add(ToHexa(bytes[i], formatOnDigits));
            }

            return hexaList.ToArray();
        }

        /// <summary>
        /// Obtiene un string de numeros hexadecimales
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="separatorString"></param>
        /// <returns></returns>
        public static string GetHexaStringFromBytes(byte[] bytes, String separatorString = HEXA_SEPARATOR_CHAR, int formatOnDigits = HexaHelper.HEXA_CHAR_STEP)
        {
            return String.Join(separatorString, GetHexaFromBytes(bytes, formatOnDigits));
        }

        /// <summary>
        /// Convierte el tamaño en bytes su tamaño equivalente en numeros hexadecimales
        /// Por ej: el tamaño de 2 bytes equivale a 4 caracteres de un numero hexa
        /// </summary>
        /// <param name="byteSize"></param>
        /// <returns></returns>
        public static int ConvertByteSizeToHexaSize(int byteSize, int formatOnDigits = HexaHelper.HEXA_CHAR_STEP)
        {
            return (byteSize * formatOnDigits);
        }

        /// <summary>
        /// Convierte el tamaño en de un hexadecimales a su tamaño equivalente en tamaño en bytes
        /// Por ej: el tamaño de 8 de un numero hexadecimal a 4 bytes
        /// EJ: 01 A6 85 7F  (8 caracteres) equivale a un array de bytes { 1, 166, 133, 127 } (4 elementos)
        /// </summary>
        /// <param name="hexaSize"></param>
        /// <returns></returns>
        public static int ConvertHexaSizeToByteSize(int hexaSize, int formatOnDigits = HexaHelper.HEXA_CHAR_STEP)
        {
            return (hexaSize / formatOnDigits);
        }

        /// <summary>
        /// Obtiene un numero Hexa de determinado largo
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomHex(int length)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                sb.Append(GetRandomHex());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Obtiene un numero random hexa (Rango 0-F equivalente a 0-15)
        /// </summary>
        /// <returns></returns>
        public static string GetRandomHex()
        {
            var intNumber = RandomNumberHelper.Between(0, HexaHelper.HEXA_BASE - 1);

            var hexNumber = HexaHelper.ToHexa(intNumber);

            return hexNumber;
        }
    }
}
