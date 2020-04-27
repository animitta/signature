using System;
using Xunit;

namespace ThinkerShare.Signature.Test
{
    public class ComplexMatchShould
    {
        [Fact]
        public void BeTrueWhenComplexMatch()
        {
            const string description = "虚构的文件类型记录";
            var complexRecord = new ComplexRecord("a,b,c", "0x11 0x22 ?? ?? ?? 0x33", 2, description);

            var data = new byte[] { 0x11, 0x11, 0x11, 0x22, 0xff, 0xdd, 0x1d, 0x33 };
            var result = complexRecord.Match(data);

            Assert.Equal(description, complexRecord.Description);
            Assert.True(result);
        }

        [Fact]
        public void ThrowExceptionWhenCreateComplexRecord()
        {
            var exception = Assert.Throws<ArgumentException>(() => new ComplexRecord("a,b,c", "0x11 0x22 0x33", 0, null));

            Assert.NotNull(exception);
        }

        [Fact]
        public void ThrowExceptionWhenGetComplexRecordBytes()
        {
            var complexRecord = new ComplexRecord("a,b,c", "0x11 0x22 ?? ?? ?? 0x33", 2, null);

            var exception = Assert.Throws<InvalidCastException>(() => complexRecord.HexBytes);

            Assert.NotNull(exception);
        }
    }
}
