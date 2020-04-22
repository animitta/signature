using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThinkerShare.Signature.Extensions;

namespace ThinkerShare.Signature.Test
{
    [TestClass]
    public class ComplexRecordExtentionsShould
    {
        [TestMethod]
        public void ComplexFileTypeTest()
        {
            var list = new List<ComplexRecord>() {
                new Record("a,b,c", "0x11 0x22 ?? ?? ?? 0x33", 2)
            };

            var data = new byte[] { 0x11, 0x11, 0x11, 0x22, 0xff, 0xdd, 0x1d, 0x33 };
            var result = list.First().Match(data);

            Assert.IsTrue(result);
        }
    }
}
