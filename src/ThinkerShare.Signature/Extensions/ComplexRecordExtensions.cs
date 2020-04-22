using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

namespace ThinkerShare.Signature.Extensions
{
    /// <summary>
    /// 文件扩展名到文件头的记录扩展方法(复杂)
    /// </summary>
    public static class ComplexRecordExtensions
    {
        /// <summary>
        /// 将HeaderRecord解析为MetadataRecord然后添加到Metadata列表中
        /// </summary>
        /// <param name="records">MetadataRecord列表</param>
        /// <param name="record">需要添加的HeaderRecord文件头记录</param>
        public static void Add(this List<ComplexRecord> records, Record record)
        {
            // 处理扩展名
            var metadata = new ComplexRecord
            {
                Extentions = record.Extentions.Split(',', ' ').ToList(),
            };

            // 将文件头字符串表示补齐
            var hex = record.Hex;
            if (record.Offset > 0)
            {
                hex = Repeat("??", record.Offset, ',') + hex;
            }

            var start = 0;
            var previousIsArbitraryByte = true;
            var bytesStringHeader = hex.Split(',', ' ');
            for (var i = 0; bytesStringHeader.Length >= i; ++i)
            {
                if (i == bytesStringHeader.Length)
                {
                    // 查找到最好一个字节
                    if (!previousIsArbitraryByte)
                    {
                        // 最后字符不是问题标记
                        metadata.Offsets.Add(MakeOffset(bytesStringHeader, start, i - start));
                    }

                    break;
                }

                if (bytesStringHeader[i] == "??")
                {
                    if (!previousIsArbitraryByte)
                    {
                        metadata.Offsets.Add(MakeOffset(bytesStringHeader, start, i - start));
                    }

                    previousIsArbitraryByte = true;
                }
                else
                {
                    if (previousIsArbitraryByte)
                    {
                        // 这是新的起点
                        start = i;
                    }

                    previousIsArbitraryByte = false;
                }
            }

            records.Add(metadata);
        }

        /// <summary>
        /// 从MetadataRecord列表中匹配指定的头的扩展名
        /// </summary>
        /// <param name="records">MetadataRecord列表</param>
        /// <param name="data">目标文件的头数据</param>
        /// <param name="matchAll">匹配所有</param>
        /// <returns>查找到的文件扩展名列表</returns>
        internal static List<string> Match(this List<ComplexRecord> records, byte[] data, bool matchAll = false)
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

        private static Offset MakeOffset(string[] bytesStringHeader, int start, int count)
        {
            // 将16进制的字符序列转换为内存表示（FF->转换为单字节255）
            var buffer = string.Join(",", bytesStringHeader, start, count).ConvertToBytes();
            return new Offset(start, count, Encoding.ASCII.GetString(buffer));
        }

        [DebuggerStepThrough]
        private static string Repeat(string source, int count, char seprator)
        {
            var builder = new StringBuilder(count);
            for (var i = 0; count > i; ++i)
            {
                builder.Append(source).Append(seprator);
            }

            return builder.ToString();
        }
    }
}
