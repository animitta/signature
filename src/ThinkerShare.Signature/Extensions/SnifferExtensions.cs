using System.Collections.Generic;

namespace ThinkerShare.Signature {
    /// <summary>
    /// Sniffer扩展方法
    /// </summary>
    public static class SnifferExtensions {
        /// <summary>
        /// 向探测器添加文件头的记录序列
        /// </summary>
        /// <param name="sniffer">探测器</param>
        /// <param name="records">文件头记录序列</param>
        public static void Populate(this Sniffer sniffer, IEnumerable<Record> records) {
            foreach (var record in records) {
                sniffer.Add(record);
            }
        }
    }
}
