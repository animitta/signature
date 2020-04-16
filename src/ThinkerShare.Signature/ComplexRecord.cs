using System.Text;
using System.Collections.Generic;

namespace ThinkerShare.Signature {
    /// <summary>
    /// （复杂)文件扩展名到文件头的记录
    /// </summary>
    public class ComplexRecord {
        #region 属性
        /// <summary>
        /// 全部的请求头偏移
        /// </summary>
        public List<Offset> Offsets { get; set; }

        /// <summary>
        /// 扩展名(一个元数据格式可能对应多个扩展名,例如zip压缩文件,可能对应.zip,.crx等)
        /// </summary>
        public List<string> Extentions { get; set; }
        #endregion

        /// <summary>
        /// 构造器
        /// </summary>
        public ComplexRecord() {
            //初始化偏移元素容量
            Offsets = new List<Offset>(4);
        }

        /// <summary>
        /// 指定的流内容是否匹配当前的元数据
        /// </summary>
        /// <param name="data">文件头内容</param>
        /// <returns>匹配结果</returns>
        public bool Match(byte[] data) {
            foreach (var offset in Offsets) {
                if (data.Length < offset.Start + offset.Count) {
                    return false;
                }
                if (offset.Value != Encoding.ASCII.GetString(data, offset.Start, offset.Count)) {
                    return false;
                }
            }
            return true;
        }
    }

    /// <summary>
    /// 表示一个复杂头记录中的一个偏移节
    /// </summary>
    public struct Offset {
        /// <summary>
        /// 尺寸
        /// </summary>
        public readonly int Count { get; }

        /// <summary>
        /// 起始位置
        /// </summary>
        public readonly int Start { get; }

        /// <summary>
        /// 将头字节当作ASCII编码解析出的字符串
        /// </summary>
        public readonly string Value { get; }

        /// <summary>
        /// 构造偏移
        /// </summary>
        /// <param name="start">偏移开始</param>
        /// <param name="count">偏移结束</param>
        /// <param name="value">内容(ASCII编码的字节字符串)</param>
        public Offset(int start, int count, string value) {
            Start = start;
            Count = count;
            Value = value;
        }

        /// <summary>
        /// 重写相等方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            var value = obj as Offset?;
            return value.HasValue ? Equals(value.Value) : false;
        }

        /// <summary>
        /// 强类型的比较实现
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Equals(Offset obj) {
            return Count == obj.Count && Start == obj.Start && Value == obj.Value;
        }

        /// <summary>
        /// 重写HashCode算法
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return Start & Count & Value.GetHashCode();
        }

        /// <summary>
        /// 重写==运算符
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Offset left, Offset right) {
            return left.Equals(right);
        }

        /// <summary>
        /// 重写不等于运算符
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Offset left, Offset right) {
            return !(left == right);
        }
    }
}