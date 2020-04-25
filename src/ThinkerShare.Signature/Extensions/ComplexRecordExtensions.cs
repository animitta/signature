using System;
using System.Collections.Generic;

namespace ThinkerShare.Signature.Extensions
{
    /// <summary>
    /// 复杂文件头记录扩展
    /// </summary>
    internal static class ComplexRecordExtensions
    {
        /// <summary>
        /// 从ComplexRecord列表中匹配文件扩展名
        /// </summary>
        /// <param name="records">ComplexRecord列表</param>
        /// <param name="data">目标文件内容</param>
        /// <param name="matchAll">是否允许匹配多个格式的文件头记录</param>
        /// <returns>查找到的文件扩展名列表</returns>
        internal static IEnumerable<string> Match(this IEnumerable<ComplexRecord> records, ReadOnlySpan<byte> data, bool matchAll = false)
        {
            var extensions = new List<string>(4);
            foreach (var record in records)
            {
                if (!record.Match(data))
                {
                    continue;
                }

                extensions.AddRange(record.Extensions);
                if (!matchAll)
                {
                    break;
                }
            }

            return extensions;
        }
    }
}
