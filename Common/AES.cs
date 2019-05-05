using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Linq;

namespace Common
{
    public class AES
    {
        private static void test()
        {
            //密码
            string password = "1234567890123456";
            //加密初始化向量
            //string iv = "                ";
            string message = AESEncrypt("abcdefghigklmnopqrstuvwxyz0123456789", password, iv);
            Console.WriteLine(message);

            message = AESDecrypt("8Z3dZzqn05FmiuBLowExK0CAbs4TY2GorC2dDPVlsn/tP+VuJGePqIMv1uSaVErr", password, iv);

            Console.WriteLine(message);
        }
        private static string iv = "                ";

        public static string GetDecryptConnectString(string str)
        {
            return string.Join(";", str.Split(';').Select(item => string.Join(" = ", item.Split(new String[] { " = " }, StringSplitOptions.RemoveEmptyEntries).Select((value, index) => index == 0 ? value : AES.AESDecrypt(value.Trim())))));
        }
        public static string GetEncryptConnectString(string str)
        {
            return string.Join(";", str.Split(';').Select(item => string.Join(" = ", item.Split(new String[] { " = " }, StringSplitOptions.RemoveEmptyEntries).Select((value, index) => index == 0 ? value.Trim() : AES.AESEncrypt(value.Trim())))));
        }
        public static string AESEncrypt(string text, string password = "1980112319801123")
        {
            //return Encryption(text, password);//
            //return AESEncrypt(text, password, iv);
            return DES.AESEncrypt(text);
        }
        /// <summary>
        /// AES加密 
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static string AESEncrypt(string text, string password, string iv)
        {
            AesManaged rijndaelCipher = new AesManaged();

            rijndaelCipher.Mode = CipherMode.CBC;

            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 128;

            rijndaelCipher.BlockSize = 128;

            byte[] pwdBytes = System.Text.Encoding.GetEncoding("GB2312").GetBytes(password);

            byte[] keyBytes = new byte[16];

            int len = pwdBytes.Length;

            if (len > keyBytes.Length) len = keyBytes.Length;

            System.Array.Copy(pwdBytes, keyBytes, len);

            rijndaelCipher.Key = keyBytes;


            byte[] ivBytes = System.Text.Encoding.GetEncoding("GB2312").GetBytes(iv);
            rijndaelCipher.IV = new byte[16];

            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();

            byte[] plainText = Encoding.GetEncoding("GB2312").GetBytes(text);

            byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);

            return Convert.ToBase64String(cipherBytes);

        }

        public static string AESDecrypt(string text, string password = "1980112319801123")
        {
            //return Decrypt(text, password);//
            //return AESDecrypt(text, password, iv);
            return DES.AESDecrypt(text);
        }

        private static  string Encryption(string express,string password)
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = password;//密匙容器的名称，保持加密解密一致才能解密成功
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] plaindata = Encoding.UTF8.GetBytes(express);//将要加密的字符串转换为字节数组
                byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
            }
        }

        //解密
        private static string Decrypt(string ciphertext, string password)
        {
            try
            {
                CspParameters param = new CspParameters();
                param.KeyContainerName = password;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
                {
                    byte[] encryptdata = Convert.FromBase64String(ciphertext);
                    byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                    return Encoding.UTF8.GetString(decryptdata);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string AESDecrypt(string text, string password, string iv)
        {
            try
            {
                //System.Security.Cryptography.AesManaged
                AesManaged rijndaelCipher = new AesManaged();

                rijndaelCipher.Mode = CipherMode.CBC;

                rijndaelCipher.Padding = PaddingMode.PKCS7;

                rijndaelCipher.KeySize = 128;

                rijndaelCipher.BlockSize = 128;

                byte[] encryptedData = Convert.FromBase64String(text);

                byte[] pwdBytes = System.Text.Encoding.GetEncoding("GB2312").GetBytes(password);

                byte[] keyBytes = new byte[16];

                int len = pwdBytes.Length;

                if (len > keyBytes.Length) len = keyBytes.Length;

                System.Array.Copy(pwdBytes, keyBytes, len);

                rijndaelCipher.Key = keyBytes;

                byte[] ivBytes = System.Text.Encoding.GetEncoding("GB2312").GetBytes(iv);
                rijndaelCipher.IV = ivBytes;

                ICryptoTransform transform = rijndaelCipher.CreateDecryptor(pwdBytes, ivBytes);

                byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);

                return Encoding.GetEncoding("GB2312").GetString(plainText);
            }
            catch(Exception ex)
            {
                
            }
            return "";
        }



    }
    public class DES
    {
        public static string AESEncrypt(string text, string password = "19801123")
            
        {
            DESCryptoServiceProvider DesCSP = new DESCryptoServiceProvider();
            byte[] kes = kes = Encoding.UTF8.GetBytes(password);
            DesCSP.Key = kes;
            DesCSP.Mode = CipherMode.ECB;
            MemoryStream ms = new MemoryStream();//先创建 一个内存流
            CryptoStream cryStream = new CryptoStream(ms, DesCSP.CreateEncryptor(), CryptoStreamMode.Write);//将内存流连接到加密转换流
            StreamWriter sw = new StreamWriter(cryStream);
            sw.WriteLine(text);//将要加密的字符串写入加密转换流
            sw.Close();
            cryStream.Close();
            byte[] buffer = ms.ToArray();//将加密后的流转换为字节数组
            return Convert.ToBase64String(buffer);
        }
        public static string AESDecrypt(string text, string password = "19801123")
        {
            DESCryptoServiceProvider DesCSP = new DESCryptoServiceProvider();
            byte[] kes = Encoding.UTF8.GetBytes(password);
            DesCSP.Key = kes;
            DesCSP.Mode = CipherMode.ECB;
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(text));//将加密后的字节数据加入内存流中
            CryptoStream cryStream = new CryptoStream(ms, DesCSP.CreateDecryptor(), CryptoStreamMode.Read);//内存流连接到解密流中
            StreamReader sr = new StreamReader(cryStream);
            string result= sr.ReadLine();//将解密流读取为字符串
            sr.Close();
            cryStream.Close();
            ms.Close();
            return result;
        }
    }
}



