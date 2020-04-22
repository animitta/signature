using System;
using System.Linq;
using System.Collections.Generic;
using ThinkerShare.Signature.Extensions;

namespace ThinkerShare.Signature
{
    /// <summary>
    /// 探测器(树结构)
    /// Signatures参考: https://en.wikipedia.org/wiki/List_of_file_signatures
    /// </summary>
    public class Signature
    {
        private readonly List<ComplexRecord> _metadatas = new List<ComplexRecord>(10);
        private readonly Node _rootNode = new Node() { Depth = -1, Children = new SortedList<byte, Node>(128) };

        /// <summary>
        /// 添加记录作为新映射
        /// </summary>
        /// <param name="record">记录</param>
        public void AddRecord(Record record)
        {
            if (record.IsComplex)
            {
                _metadatas.Add(record);
            }
            else
            {
                AddRecord(record.Hex.ConvertToBytes(), record.Extentions.Split(',', ' '));
            }
        }

        /// <summary>
        /// 添加新的头和扩展名映射
        /// </summary>
        /// <param name="data">文件头</param>
        /// <param name="extentions">文件扩展名列表</param>
        public void AddRecord(byte[] data, string[] extentions)
        {
            AddRecord(data, _rootNode, extentions, 0);
        }

        private void AddRecord(byte[] data, Node parent, string[] extentions, int depth)
        {
            parent.Children = parent.Children ?? new SortedList<byte, Node>((int)(128 / Math.Pow(2, depth)));

            Node currentNode;
            if (!parent.Children.ContainsKey(data[depth]))
            {
                // 简单头可以添加到当前层(还没有被值占领)
                currentNode = new Node { Depth = depth, Parent = parent };
                parent.Children.Add(data[depth], currentNode);
            }
            else
            {
                currentNode = parent.Children[data[depth]];
            }

            // 最后一个字节,放入到扩展中
            if (depth == (data.Length - 1))
            {
                // 已经是文件头最后一个字节,则当前文件头映射必须就在此层
                // 为此类型的头添加文件扩展名
                currentNode.Extentions = currentNode.Extentions ?? new List<string>(4);
                currentNode.Extentions.AddRange(extentions);
                return;
            }

            AddRecord(data, currentNode, extentions, depth + 1);
        }

        /// <summary>
        /// 查找文件头的扩展名
        /// </summary>
        /// <param name="data">文件头</param>
        /// <param name="matchComplex">是否匹配查找全部的库总名</param>
        /// <returns>匹配的文件扩展结果列表</returns>
        public List<string> Match(byte[] data, bool matchComplex = false)
        {
            var extentionStore = new List<string>(4);
            Match(data, 0, _rootNode, extentionStore, matchComplex);

            if (matchComplex || !extentionStore.Any())
            {
                // 单一匹配失败或者接受复杂匹配
                extentionStore.AddRange(_metadatas.Match(data, matchComplex));
            }

            return extentionStore.Distinct().ToList();
        }

        private void Match(byte[] data, int depth, Node node, List<string> extentionStore, bool matchAll)
        {
            if (data.Length == depth)
            {
                // 找到尽头了
                return;
            }

            node.Children.TryGetValue(data[depth], out Node current);
            if (current != null)
            {
                if (current.Extentions != null)
                {
                    extentionStore.AddRange(current.Extentions);
                    if (!matchAll)
                    {// 不再匹配(可能提前中断)
                        return;
                    }
                }
            }
            else
            {// 找不到
                return;
            }

            if (current.Children != null)
            {// 尝试再下一层继续找
                Match(data, depth + 1, current, extentionStore, matchAll);
            }
            else
            {
                return;
            }
        }
    }
}