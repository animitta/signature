using System.Text;
using System.Collections.Generic;

namespace ThinkerShare.Signature
{
    /// <summary>
    /// （复杂)文件扩展名到文件头的记录
    /// </summary>
    public class ComplexRecord
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public ComplexRecord()
        {
            // 初始化偏移元素容量
            Offsets = new List<Offset>(4);
        }

        /// <summary>
        /// 全部的请求头偏移
        /// </summary>
        public List<Offset> Offsets { get; set; }

        /// <summary>
        /// 扩展名(一个元数据格式可能对应多个扩展名,例如zip压缩文件,可能对应.zip,.crx等)
        /// </summary>
        public List<string> Extentions { get; set; }

        /// <summary>
        /// 指定的流内容是否匹配当前的元数据
        /// </summary>
        /// <param name="data">文件头内容</param>
        /// <returns>匹配结果</returns>
        public bool Match(byte[] data)
        {
            foreach (var offset in Offsets)
            {
                if (data.Length < offset.Start + offset.Count)
                {
                    return false;
                }

                if (offset.Value != Encoding.ASCII.GetString(data, offset.Start, offset.Count))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
