using System.Linq;
using System.Collections.Generic;

namespace ThinkerShare.Signature.Extensions
{
    /// <summary>
    /// 二进制数据扩展方法
    /// </summary>
    public static class BytesExtensions
    {
        static BytesExtensions()
        {
            Signature = new Signature();
            Signature.AddRecords(Record.FrequentRecords);
            Signature.AddRecords(Record.UnfrequentRecords);
        }

        /// <summary>
        /// 全局Signature(多线程不安全)
        /// </summary>
        public static Signature Signature { get; }

        /// <summary>
        /// 获取真实文件类型的文件扩展名
        /// </summary>
        /// <param name="data">文件数据(允许只包含的数据切片)</param>
        /// <param name="matchAll">是否启用复杂匹配(如果需要匹配复杂的文件指纹请启用此参数)</param>
        /// <returns>扩展扩展名或null</returns>
        public static string GetExtension(this byte[] data, bool matchAll = false)
        {
            return Signature.Match(data, matchAll).FirstOrDefault();
        }

        /// <summary>
        /// 获取真实文件类型的文件扩展名列表
        /// </summary>
        /// <param name="data">文件数据(允许只包含的数据切片)</param>
        /// <param name="matchAll">是否启用复杂匹配(如果需要匹配复杂的文件指纹请启用此参数)</param>
        /// <returns>文件扩展名列表</returns>
        public static IReadOnlyList<string> GetExtensions(this byte[] data, bool matchAll = false)
        {
            return Signature.Match(data, matchAll);
        }
    }
}
