using System.Collections.Generic;

namespace ThinkerShare.Signature
{
    /// <summary>
    /// 解析树节点
    /// </summary>
    internal class Node
    {
        /// <summary>
        /// 节点在树中的深度
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public Node Parent { get; set; }

        /// <summary>
        /// 扩展名列表
        /// </summary>
        public List<string> Extensions { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        public SortedList<byte, Node> Children { get; set; }
    }
}
