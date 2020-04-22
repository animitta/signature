namespace ThinkerShare.Signature
{
    /// <summary>
    /// 复杂文件头记录中的偏移
    /// </summary>
    internal struct Offset
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="start">偏移开始位置</param>
        /// <param name="value">将头字节当作ASCII编码解析出的字符串</param>
        public Offset(int start, string value)
        {
            Start = start;
            Value = value;
        }

        /// <summary>
        /// 偏移开始位置
        /// </summary>
        public int Start { get; }

        /// <summary>
        /// 将头字节当作ASCII编码解析出的字符串
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// 偏移匹配尺寸
        /// </summary>
        public int Count => Value?.Length ?? 0;

        /// <summary>
        /// 重写==运算符
        /// </summary>
        /// <param name="left">左运算数</param>
        /// <param name="right">右运算数</param>
        /// <returns>结果</returns>
        public static bool operator ==(Offset left, Offset right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// 重写不等于运算符
        /// </summary>
        /// <param name="left">左运算数</param>
        /// <param name="right">右运算数</param>
        /// <returns>是否不相等</returns>
        public static bool operator !=(Offset left, Offset right)
        {
            return !(left == right);
        }

        /// <summary>
        /// 强类型的比较实现
        /// </summary>
        /// <param name="other">其它对象</param>
        /// <returns>是否相等</returns>
        public bool Equals(Offset other)
        {
            return Start == other.Start && Value == other.Value;
        }

        /// <summary>
        /// 比较相等性
        /// </summary>
        /// <param name="other">其它对象</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object other)
        {
            var value = other as Offset?;
            return value.HasValue ? Equals(value.Value) : false;
        }

        /// <summary>
        /// 重写HashCode算法
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return Start & Value.GetHashCode();
        }
    }
}
