using System.Collections.Generic;

namespace ThinkerShare.Signature
{
    /// <summary>
    /// 解析树节点
    /// </summary>
    internal class Node
    {
        /// <summary>
        /// 节点的深度
        /// </summary>
        internal int Depth { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        internal Node Parent { get; set; }

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
