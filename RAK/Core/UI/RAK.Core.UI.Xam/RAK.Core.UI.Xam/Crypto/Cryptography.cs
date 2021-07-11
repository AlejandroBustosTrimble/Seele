using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RAK.Core.UI.Xam.Crypto
{
    /// <summary>
    /// Helper de Encriptación / Desencriptación
    /// </summary>
    public static class Cryptography
    {
        #region Const

        /// <summary>
        /// Obtiene la Key de desencriptación
        /// </summary>
        private static string sharedSecret = GetSharedSecret();

        /// <summary>
        /// Obtiene el multiplicador randmom de caracteres
        /// </summary>
        private static string chars = GetMultiplicatorSecret();

        /// <summary>
        /// Encoder de los char multiplicadores
        /// </summary>
        private static byte[] _salt = Encoding.ASCII.GetBytes(chars);

        #endregion

        #region Methods

        /// <summary>
        /// Encriptación usando el algoritmo RijndaelManaged
        /// </summary>
        /// <param name="plainText">Texto plano</param>
        /// <returns>Texto encriptado</returns>
        public static string Encrypt(string plainText)
        {
            #region Encrypt

            // -- Clave generada
            string outStr = null;
            // -- Algoritmo de encriptación
            RijndaelManaged aesAlg = null;

            try
            {
                if (string.IsNullOrEmpty(plainText) || string.IsNullOrEmpty(sharedSecret))
                    throw new ArgumentNullException("plainText");

                // -- Generamos la clave de encriptación para el algoritmo
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                // -- Creamos el algoritmo de encriptacion
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                // -- Creamos el encriptador para convertir el texto
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // -- Creamos el objeto a encriptar
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // -- Anteponenemos la IV -> Vector de inicialización (IV) para el algoritmo simétrico
                    // -- Utilizamos unos bytes aleatorios en el comienzo de la cadena
                    byte[] bytes = new byte[aesAlg.IV.Length];
                    new Random().NextBytes(bytes);
                    msEncrypt.Write(bytes, 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            // -- Escribimos los datos en el objeto
                            swEncrypt.Write(plainText);
                        }
                    }

                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // -- Limpiamos el algoritmo
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            #endregion

            // -- Retorna texto encriptado
            return outStr;
        }

        /// <summary>
        /// Algoritmo de desencriptación
        /// </summary>
        /// <param name="cipherText">Texto Cifrado</param>
        /// <returns>Texto desencriptado</returns>
        public static string Decrypt(string cipherText)
        {
            #region Decrypt

            if (string.IsNullOrEmpty(cipherText) || string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("cipherText");

            // -- Algoritmo de encriptación
            RijndaelManaged aesAlg = null;
            // -- Texto desencriptado
            string plaintext = null;

            try
            {
                // -- Generamos la clave de encriptación para el algoritmo
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                // -- Generamos la cadena a desencriptar
                byte[] bytes = Convert.FromBase64String(cipherText);
                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    // -- Creamos el algoritmo de desencriptacion con la llave
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                    // -- Obtenemos el vector de inicialización
                    aesAlg.IV = ReadByteArray(msDecrypt);

                    // -- Creamos el desencriptador para tranformar el texto
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // -- Leemos los bytes desencriptados del objeto desencriptador
                            // y los escribimos en la variable de salida
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                if (aesAlg != null)
                    aesAlg.Clear();
            }

            #endregion

            // -- Retorna el texto desencriptado
            return plaintext;
        }

        /// <summary>
        /// Lee los bytes de la secuencia enviada
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns></returns>
        private static byte[] ReadByteArray(Stream stream)
        {
            byte[] rawLength = new byte[sizeof(int)];
            byte[] buffer = new byte[sizeof(int) * 4];

            // -- Valida que el parametro de entrada no este vacio
            if (stream.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
                throw new SystemException("Stream did not contain properly formatted byte array");

            // -- Valida que el tamaño del vector sea de 16
            if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
                throw new SystemException("Did not read byte array properly");

            return buffer;
        }

        #endregion

        #region Methods

        private const string SecretKey = "encriptedkey123";
        private const string SecretMultiplicator = "o6806642kbM7c5";

        /// <summary>
        /// Retorna la clave secreta
        /// </summary>
        /// <returns>String</returns>
        private static string GetSharedSecret()
        {
            return SecretKey;
        }

        /// <summary>
        /// Retorna los bytes multiplicadores
        /// </summary>
        /// <returns>String</returns>
        private static string GetMultiplicatorSecret()
        {
            return SecretMultiplicator;
        }

        #endregion
    }

    /// <summary>
    /// Encriptacion 3DES
    /// </summary>
    public class Crypto3DES
    {
        private TripleDESCryptoServiceProvider cryptoProvider;

        private TripleDESCryptoServiceProvider CryptoProvider
        {
            get
            {
                if (this.cryptoProvider == null)
                {
                    this.cryptoProvider = new TripleDESCryptoServiceProvider();
                    this.cryptoProvider.Mode = CipherMode.CBC;
                    this.cryptoProvider.Padding = PaddingMode.Zeros;
                    this.cryptoProvider.KeySize = 128;
                    this.cryptoProvider.IV = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                }

                return this.cryptoProvider;
            }
        }

        /// <summary>
        /// CipherMode del DES
        /// </summary>
        public CipherMode CypherMode
        {
            set
            {
                this.CryptoProvider.Mode = value;
            }
        }

        /// <summary>
        /// PaddingMode
        /// </summary>
        public PaddingMode PaddingMode
        {
            set
            {
                this.CryptoProvider.Padding = value;
            }
        }

        /// <summary>
        /// IV
        /// </summary>
        public byte[] IV
        {
            set
            {
                this.CryptoProvider.IV = value;
            }
        }

        /// <summary>
        /// KeySize
        /// </summary>
        public int KeySize
        {
            set
            {
                this.CryptoProvider.KeySize = value;
            }
        }


        public byte[] Encrypt(byte[] data, byte[] key)
        {
            // — Para el cifrado del DATA se utiliza el algoritmo 3DES en su modo CBC.
            // — El DATA a cifrar tiene que ser multiplo de 8, en que no se cumpla esta condicion, se rellena (padding) con 0xCA = 0
            // — El largo de la WkLP (Workign Key Lectograbador - POS) debe ser de 128 bits
            this.CryptoProvider.Key = key;

            ICryptoTransform cTransform = this.CryptoProvider.CreateEncryptor(this.CryptoProvider.Key, this.CryptoProvider.IV);

            byte[] resultArray = cTransform.TransformFinalBlock(data, 0, data.Length);
            this.CryptoProvider.Clear();

            // — Devuelvo los datos encriptados
            return resultArray;// Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="useHashing"></param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] data, byte[] key)
        {
            this.CryptoProvider.Key = key;
            this.CryptoProvider.IV = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            ICryptoTransform cTransform = this.CryptoProvider.CreateDecryptor(this.CryptoProvider.Key, this.CryptoProvider.IV);

            byte[] resultArray = cTransform.TransformFinalBlock(data, 0, data.Length);

            this.CryptoProvider.Clear();

            // — Devuelvo los datos desencriptados
            return resultArray; //UTF8Encoding.UTF8.GetString(resultArray);
        }

    }
}
