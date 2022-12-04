using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace GZKL.Client.UI.Common
{
    /// <summary>
    /// 加密解密帮助类
    /// </summary>
    public static class SecurityHelper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">待加密字符串</param>
        /// <param name="tag">加密是16位还是32位，如果为true则是16位。</param>
        /// <returns></returns>
        public static string MD5Encrypt(string source, bool tag = false)
        {
            try
            {
                string outputStr = "";
                MD5 md5 = MD5.Create(); //实例化一个md5对像
                byte[] md5byte = md5.ComputeHash(Encoding.UTF8.GetBytes(source));

                if (tag)
                {
                    string md5str16 = BitConverter.ToString(md5byte, 4, 8);
                    outputStr = md5str16.Replace("-", "");
                }
                else
                {
                    // outputStr = StringToHexString(md5byte);
                    for (int i = 0; i < md5byte.Length; i++)
                    {
                        outputStr += md5byte[i].ToString("X2");
                    }
                }
                return outputStr;
            }
            catch (Exception ex)
            {
                throw new Exception("MD5加密错误," + ex.Message);
            }
        }

        /// <summary> 
        /// DES加密 
        /// </summary> 
        /// <param name="text">需要加密的值</param> 
        /// <param name="sKey">加密的密钥</param> 
        /// <returns></returns> 
        public static string DESEncrypt(string text, string sKey = "MATICSOFT")
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(text);
            des.Key = ASCIIEncoding.UTF8.GetBytes(MD5Encrypt(sKey, false).Substring(0, 8));
            des.IV = ASCIIEncoding.UTF8.GetBytes(MD5Encrypt(sKey, false).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            cs.Close();
            cs.Dispose();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary> 
        /// DES解密 
        /// </summary> 
        /// <param name="text">需要解密的值</param> 
        /// <param name="sKey">解密密钥</param> 
        /// <returns></returns> 
        public static string DESDecrypt(string text, string sKey = "MATICSOFT")
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.UTF8.GetBytes(MD5Encrypt(sKey, false).Substring(0, 8));
            des.IV = ASCIIEncoding.UTF8.GetBytes(MD5Encrypt(sKey, false).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            cs.Close();
            cs.Dispose();
            return Encoding.Default.GetString(ms.ToArray());
        }

        /// <summary>
        /// SHA 512加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string SHA512Encrypt(string source)
        {
            string result = "";
            byte[] buffer = Encoding.UTF8.GetBytes(source);//UTF-8 编码

            //64字节,512位
            SHA512CryptoServiceProvider SHA512 = new SHA512CryptoServiceProvider();
            byte[] h5 = SHA512.ComputeHash(buffer);

            result = BitConverter.ToString(h5).Replace("-", string.Empty);

            return result.ToLower();
        }


        #region Access数据库加解密

        //对应MDB文件的前16个字节
        private static byte[] _originalFileHeader = new byte[16] { 0x00, 0x01, 0x00, 0x00, 0x53, 0x74, 0x61, 0x6E, 0x64, 0x61, 0x72, 0x64, 0x20, 0x4A, 0x65, 0x74 };

        //更改后的MDB文件的前16个字节
        private static byte[] _newFileHeader = new byte[16] { 0x48, 0x4A, 0x00, 0x58, 0x55, 0x43, 0x48, 0x41, 0x4E, 0x47, 0x59, 0x4F, 0x55, 0x00, 0x20, 0x20 };

        /// <summary>
        /// 加密Access数据库，用_newFileHeader内容替换MDB前16个字节，以便实现加密的作用
        /// </summary>
        /// <param name="fileName"></param>
        public static void EncrptAccessDb(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(fileName);
            }

            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                fs.Seek(0, SeekOrigin.Begin);
                fs.Write(_newFileHeader, 0, 16);
                fs.Close();
            }
        }

        /// <summary>
        /// 解密Access数据库,还原MDB前16个字节
        /// </summary>
        /// <param name="fileName"></param>
        public static void UncrptAccessDb(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(fileName);
            }

            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                fs.Seek(0, SeekOrigin.Begin);
                fs.Write(_originalFileHeader, 0, 16);
                fs.Close();
            }
        }

        #endregion
    }
}
