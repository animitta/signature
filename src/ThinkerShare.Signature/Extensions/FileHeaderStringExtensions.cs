using System;

namespace ThinkerShare.Signature.Extensions
{
    /// <summary>
    /// 文件头字符串库总
    /// </summary>
    internal static class FileHeaderStringExtensions
    {
        /// <summary>
        /// 将文件头字符串表示转换为其实际二进制表示
        /// </summary>
        /// <param name="header">文件头的ASCII表示(使用空格分隔)</param>
        /// <returns>文件头实际字节内容</returns>
        internal static byte[] ConvertToBytes(this string header)
        {
            var array = header.Split(',', ' ');
            var byteArray = new byte[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                byteArray[i] = Convert.ToByte(array[i], 16);
            }

            return byteArray;
        }
    }
}
