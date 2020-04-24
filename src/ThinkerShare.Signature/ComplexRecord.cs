using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace ThinkerShare.Signature
{
    /// <summary>
    /// 复杂文件头记录
    /// </summary>
    internal class ComplexRecord : Record
    {
        [ThreadStatic]
        private static readonly WeakReference<StringBuilder> _stringBuilder = new WeakReference<StringBuilder>(null);

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="extentions">文件扩展名列表</param>
        /// <param name="hex">十六进制字符串</param>
        /// <param name="offsetSize">偏移</param>
        /// <param name="description">描述</param>
        internal ComplexRecord(string extentions, string hex, int offsetSize, string description)
            : base(extentions, hex, offsetSize, description)
        {
            if (!IsComplex)
            {
                throw new ArgumentException("无法构成有效的复杂文件头记录");
            }

            Offsets = new List<Offset>(4);
            Extensions = base.Extensions.Split(',', ' ').ToList();

            // 将文件头字符串表示补齐
            var start = 0;
            var previousIsArbitraryByte = true;
            var stringHeadlers = hex.Split(',', ' ');
            for (var i = 0; stringHeadlers.Length >= i; ++i)
            {
                if (i == stringHeadlers.Length)
                {// 单独处理最好一个字符
                    if (!previousIsArbitraryByte)
                    {// 最后字符不是问题标记
                        var offset = new Offset(stringHeadlers, start, i - start, offsetSize);
                        Offsets.Add(offset);
                    }

                    break;
                }

                if (stringHeadlers[i] == "??")
                {
                    if (!previousIsArbitraryByte)
                    {
                        var offset = new Offset(stringHeadlers, start, i - start, offsetSize);
                        Offsets.Add(offset);
                    }

                    previousIsArbitraryByte = true;
                }
                else
                {
                    if (previousIsArbitraryByte)
                    { // 这是新的起点
                        start = i;
                    }

                    previousIsArbitraryByte = false;
                }
            }
        }

        /// <summary>
        /// 全部的请求头偏移
        /// </summary>
        internal List<Offset> Offsets { get; set; }

        /// <summary>
        /// 文件扩展名集合(一个文件可能对应多个文件扩展名)
        /// </summary>
        public new List<string> Extensions { get; set; }

        /// <summary>
        /// 文件内容是否匹配当前的元数据
        /// </summary>
        /// <param name="data">文件内容</param>
        /// <returns>是否匹配</returns>
        public bool Match(byte[] data)
        {
            foreach (var offset in Offsets)
            {
                if (offset.Start + offset.Count > data.Length)
                {
                    return false;
                }

                var realValue = data.AsSpan().Slice(offset.Start, offset.Count);
                if (!realValue.SequenceEqual(offset.Value.AsSpan()))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
