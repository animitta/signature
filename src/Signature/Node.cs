using System.Collections.Generic;

namespace Thinkershare.Signature
{
    /// <summary>
    /// 解析树节点
    /// </summary>
    internal class Node
    {
        /// <summary>
        /// 文件扩展名列表
        /// </summary>
        internal List<string> Extensions { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        internal SortedList<byte, Node> Children { get; set; }
    }
}
