using System.Collections.Generic;

namespace ThinkerShare.Signature
{
    /// <summary>
    /// 节点对象.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Gets or sets 深度.
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// Gets or sets 父节点.
        /// </summary>
        public Node Parent { get; set; }

        /// <summary>
        /// Gets or sets 子节点.
        /// </summary>
        public SortedList<byte, Node> Children { get; set; }

        /// <summary>
        /// Gets or sets 扩展名列表.
        /// </summary>
        public List<string> Extentions { get; set; }
    }
}