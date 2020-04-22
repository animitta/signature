using System.Collections.Generic;

namespace ThinkerShare.Signature.Extensions
{
    /// <summary>
    /// 复杂文件头记录序列扩展
    /// </summary>
    internal static class ComplexRecordExtensions
    {
        /// <summary>
        /// 从MetadataRecord列表中匹配指定的头的扩展名
        /// </summary>
        /// <param name="records">MetadataRecord列表</param>
        /// <param name="data">目标文件的头数据</param>
        /// <param name="matchAll">匹配所有</param>
        /// <returns>查找到的文件扩展名列表</returns>
        public static IEnumerable<string> Match(this IEnumerable<ComplexRecord> records, byte[] data, bool matchAll = false)
        {
            var extentionStore = new List<string>(4);
            foreach (var record in records)
            {
                if (record.Match(data))
                {
                    extentionStore.AddRange(record.Extentions);
                    if (!matchAll)
                    {
                        break;
                    }
                }
            }

            return extentionStore;
        }
    }
}
