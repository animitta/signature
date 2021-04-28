using System;
using System.Collections.Generic;

namespace Thinkershare.Signature
{
    /// <summary>
    /// 复杂文件头记录中的偏移
    /// </summary>
    internal readonly struct Offset
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="sections">分节头数据</param>
        /// <param name="start">偏移起始索引</param>
        /// <param name="count">偏移内容长度</param>
        /// <param name="offsetSize">当前sections在数据文件中的偏移</param>
        internal Offset(IReadOnlyList<string> sections, int start, int count, int offsetSize = 0)
        {
            Start = start + offsetSize;
            Value = new byte[count];

            for (var i = 0; count > i; ++i)
            {
                Value[i] = Convert.ToByte(sections[start + i], 16);
            }
        }

        /// <summary>
        /// 偏移起始点索引
        /// </summary>
        internal int Start { get; }

        /// <summary>
        /// 字节序列
        /// </summary>
        internal byte[] Value { get; }

        /// <summary>
        /// 偏移字节数
        /// </summary>
        internal int Count => Value.Length;
    }
}
