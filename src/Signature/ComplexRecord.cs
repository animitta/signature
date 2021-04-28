using System;
using System.Collections.Generic;

namespace Thinkershare.Signature
{
    /// <summary>
    /// 复杂文件头记录
    /// </summary>
    internal class ComplexRecord : Record
    {
        private const string AnyByteString = "??";

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="extensions">文件扩展名列表</param>
        /// <param name="hex">十六进制字符串</param>
        /// <param name="offsetSize">偏移</param>
        /// <param name="description">描述</param>
        internal ComplexRecord(string extensions, string hex, int offsetSize, string description)
            : base(extensions, hex, offsetSize, description)
        {
            if (!IsComplex)
            {
                throw new ArgumentException("无法构成有效的复杂文件头记录");
            }

            var offsets = new List<Offset>(4);
            Offsets = offsets;

            var start = 0;
            var previousIsAnyByte = true;
            var stringHeaders = hex.Split(Separator);
            for (var i = 0; stringHeaders.Length >= i; ++i)
            {
                if (i == stringHeaders.Length)
                {// 单独处理最好一个字符
                    if (!previousIsAnyByte)
                    {// 最后字符不是问题标记
                        var offset = new Offset(stringHeaders, start, i - start, offsetSize);
                        offsets.Add(offset);
                    }

                    break;
                }

                if (string.Equals(stringHeaders[i], AnyByteString))
                {
                    if (!previousIsAnyByte)
                    {// 当前是??字节,且前面不是??, 故此前面的连续字节构成一个Offset
                        var offset = new Offset(stringHeaders, start, i - start, offsetSize);
                        offsets.Add(offset);
                    }

                    previousIsAnyByte = true;
                }
                else
                {
                    if (previousIsAnyByte)
                    { // 前面是任意字符,则标识此处是新起点
                        start = i;
                    }

                    // 当前字节不是任意字符
                    previousIsAnyByte = false;
                }
            }
        }

        /// <summary>
        /// 请求头偏移序列
        /// </summary>
        internal IReadOnlyList<Offset> Offsets { get; set; }

        /// <summary>
        /// 文件内容是否匹配当前的元数据
        /// </summary>
        /// <param name="data">文件内容</param>
        /// <returns>是否匹配</returns>
        internal bool Match(ReadOnlySpan<byte> data)
        {
            foreach (var offset in Offsets)
            {
                if (offset.Start + offset.Count > data.Length)
                {
                    return false;
                }

                var realValue = data.Slice(offset.Start, offset.Count);
                if (!realValue.SequenceEqual(offset.Value))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
