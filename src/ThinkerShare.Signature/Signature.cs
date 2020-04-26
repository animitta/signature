using System;
using System.Collections.Generic;
using System.Linq;
using ThinkerShare.Signature.Extensions;

namespace ThinkerShare.Signature
{
    /// <summary>
    /// 探测器(树结构)
    /// Signatures参考: https://en.wikipedia.org/wiki/List_of_file_signatures
    /// </summary>
    public class Signature
    {
        private readonly List<ComplexRecord> _complexRecords = new List<ComplexRecord>(16);
        private readonly Node _rootNode = new Node() { Depth = -1, Children = new SortedList<byte, Node>(256) };

        /// <summary>
        /// 添加文件头记录
        /// </summary>
        /// <param name="record">文件头记录</param>
        public void AddRecord(Record record)
        {
            if (record is ComplexRecord complexRecord)
            {
                _complexRecords.Add(complexRecord);
            }
            else
            {
                AddRecord(record.HexBytes, record.Extensions);
            }
        }

        /// <summary>
        /// 添加新的头和扩展名映射
        /// </summary>
        /// <param name="data">文件头</param>
        /// <param name="extensions">文件扩展名列表</param>
        public void AddRecord(IReadOnlyList<byte> data, IEnumerable<string> extensions)
        {
            BuildResolver(data, _rootNode, extensions, 0);
        }

        /// <summary>
        /// 向探测器添加文件头的记录序列
        /// </summary>
        /// <param name="records">文件头记录序列</param>
        public void AddRecords(IEnumerable<Record> records)
        {
            foreach (var record in records)
            {
                AddRecord(record);
            }
        }

        private static void BuildResolver(IReadOnlyList<byte> data, Node parent, IEnumerable<string> extensions, int depth)
        {
            while (true)
            {
                parent.Children ??= new SortedList<byte, Node>((int)(128 / Math.Pow(2, depth)));

                Node currentNode;
                if (!parent.Children.ContainsKey(data[depth]))
                { // 简单头可以添加到当前层(还没有被值占领)
                    currentNode = new Node { Depth = depth, Parent = parent };
                    parent.Children.Add(data[depth], currentNode);
                }
                else
                {
                    currentNode = parent.Children[data[depth]];
                }

                if (depth == (data.Count - 1))
                { // 最后一个字节, 则需要将文件扩展名添加到文件头匹配的集合中
                    currentNode.Extensions ??= new List<string>(4);
                    currentNode.Extensions.AddRange(extensions);
                    return;
                }

                parent = currentNode;
                depth += 1;
            }
        }

        /// <summary>
        /// 查找文件头的扩展名
        /// </summary>
        /// <param name="data">文件头</param>
        /// <param name="matchAll">是否匹配查找全部的库总名</param>
        /// <returns>匹配的文件扩展结果列表</returns>
        public IReadOnlyList<string> Match(ReadOnlySpan<byte> data, bool matchAll = false)
        {
            var extensions = Match(data, 0, _rootNode, matchAll);
            if (matchAll || !extensions.Any())
            { // 简单文件头记录匹配失败
                extensions.AddRange(_complexRecords.Match(data, matchAll));
            }

            return extensions.Distinct().ToList();
        }

        private static List<string> Match(ReadOnlySpan<byte> data, int depth, Node node, bool matchAll)
        {
            var extensions = new List<string>(4);
            while (true)
            {
                if (data.Length == depth)
                { // 找到尽头了
                    return extensions;
                }

                node.Children.TryGetValue(data[depth], out Node current);
                if (current != null)
                {
                    if (current.Extensions != null && current.Extensions.Count > 0)
                    {
                        extensions.AddRange(current.Extensions);
                        if (!matchAll)
                        { // 不允许多重匹配,提前结束后续匹配
                            return extensions;
                        }
                    }
                }
                else
                { // 当前字符串未找到，则表示文件头类型未知
                    return extensions;
                }

                if (current.Children != null)
                { // 尝试再下一层继续找
                    depth += 1;
                    node = current;
                }
                else
                {
                    return extensions;
                }
            }
        }
    }
}
