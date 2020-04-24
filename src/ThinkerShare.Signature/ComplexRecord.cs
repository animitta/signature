using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace ThinkerShare.Signature
{
    /// <summary>
    /// 复杂文件头记录
    /// </summary>
    public class ComplexRecord : Record
    {
        [ThreadStatic]
        private static readonly WeakReference<StringBuilder> _stringBuilder = new WeakReference<StringBuilder>(null);

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="extentions">文件扩展名列表</param>
        /// <param name="hex">十六进制字符串</param>
        /// <param name="offset">偏移</param>
        /// <param name="description">描述</param>
        internal ComplexRecord(string extentions, string hex, int offset, string description)
            : base(extentions, hex, offset, description)
        {
            if (!IsComplex)
            {
                throw new ArgumentException("无法构成有效的复杂文件头记录");
            }

            Offsets = new List<Offset>(4);
            Extensions = base.Extensions.Split(',', ' ').ToList();

            // 将文件头字符串表示补齐
            if (offset > 0)
            {
                hex = Repeat("??", offset, ',') + hex;
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
                        Offsets.Add(new Offset(bytesStringHeader, start, i - start));
                    }

                    break;
                }

                if (bytesStringHeader[i] == "??")
                {
                    if (!previousIsArbitraryByte)
                    {
                        Offsets.Add(new Offset(bytesStringHeader, start, i - start));
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

            static string Repeat(string source, int count, char seprator)
            {
                var builder = StringBuilder;
                builder.Clear();
                for (var i = 0; count > i; ++i)
                {
                    builder.Append(source).Append(seprator);
                }

                var result = builder.ToString();
                return result;
            }
        }

        /// <summary>
        /// 获取当前线程可用的StringBuilder对象
        /// </summary>
        private static StringBuilder StringBuilder
        {
            get
            {
                if (_stringBuilder.TryGetTarget(out var target))
                {
                    return target;
                }
                else
                {
                    target = new StringBuilder();
                    _stringBuilder.SetTarget(target);
                    return target;
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
