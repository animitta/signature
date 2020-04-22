using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThinkerShare.Signature.Extensions;

namespace ThinkerShare.Signature.Test {
    [TestClass]
    public class SignatureTest {
        [TestMethod]
        public void RecordTest() {
            var Signature = new Signature();
            var data = new byte[] { 0x11, 0x22, 0x33 };
            Signature.AddRecord(data, new[] { "what", "file", "type" });


            var result = Signature.Match(data);
            Assert.IsTrue(result.Contains("what"));
            Assert.IsTrue(result.Contains("file"));
            Assert.IsTrue(result.Contains("type"));
        }

        [TestMethod]
        public void ComplexRecordTest() {
            var Signature = new Signature();
            var record = new Record("a,b,c", "0x11 0x22 ?? ?? ?? 0x33", 2);
            Signature.AddRecord(record);

            var data = new byte[] { 0x11, 0x11, 0x11, 0x22, 0xff, 0xdd, 0x1d, 0x33 };
            var result = Signature.Match(data);

            Assert.IsTrue(result.Contains("a"));
            Assert.IsTrue(result.Contains("b"));
            Assert.IsTrue(result.Contains("c"));
        }

        [TestMethod]
        public void JpegTest() {
            var Signature = new Signature();
            Signature.AddRecords(Record.Common);
            Signature.AddRecords(Record.Unfrequent);

            var head = new byte[] { 0xff, 0xd8, 0xff, 0xdb };
            var result = Signature.Match(head);

            Assert.IsTrue(result.Contains("jpg"));
            Assert.IsTrue(result.Contains("jpeg"));
        }

        [TestMethod]
        public void JpegTest2() {
            var Signature = new Signature();
            Signature.AddRecords(Record.Common);

            var data = new byte[]
            {
                0xff, 0xd8, 0xff, 0xe0,
                0x66, 0x74, 0x4a, 0x46,
                0x49, 0x46, 0x00, 0x01,
            };
            var result = Signature.Match(data);

            Assert.IsTrue(result.Contains("jpg"));
            Assert.IsTrue(result.Contains("jpeg"));
        }

        [TestMethod]
        public void JpegTest3() {
            var Signature = new Signature();
            Signature.AddRecords(Record.Common);

            var data = new byte[]
            {
                0xff, 0xd8, 0xff, 0xe1,
                0x66, 0x74, 0x45, 0x78,
                0x69, 0x66, 0x00, 0x00,
            };
            var result = Signature.Match(data);

            Assert.IsTrue(result.Contains("jpg"));
            Assert.IsTrue(result.Contains("jpeg"));
        }

        [TestMethod]
        public void FindAllTest() {
            var Signature = new Signature();
            Signature.AddRecords(Record.Common);
            Signature.AddRecords(Record.Unfrequent);

            var data = new byte[] { 0x25, 0x50, 0x44, 0x46, 0x11 };
            Signature.AddRecord(data, new[] { "pdfx" });
            var result = Signature.Match(data, true);

            Assert.IsTrue(result.Contains("pdf"));
            Assert.IsTrue(result.Contains("pdfx"));
        }

        [TestMethod]
        public void Gp3Test() {
            var Signature = new Signature();
            Signature.AddRecords(Record.Unfrequent);

            var data = new byte[] {
                0x11, 0x11, 0x11, 0x22,
                0x66, 0x74, 0x79, 0x70, 0x33, 0x67
            };
            var result = Signature.Match(data);

            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.Contains("3gp"));
            Assert.IsTrue(result.Contains("3g2"));
        }

        [TestMethod]
        public void MimeTest() {
            var Signature = new Signature();
            Signature.AddRecords(Record.Common);
            Signature.AddRecords(Record.Unfrequent);

            var head = new byte[] { 0xff, 0xd8, 0xff, 0xdb };
            var result = Signature.Match(head);

            var mimeType = result.First().GetMimeType();
            Assert.IsTrue(mimeType == "image/jpeg");
        }

        [TestMethod]
        public void MultipleTest() {
            var Signature = new Signature();
            Signature.AddRecords(Record.Common);
            Signature.AddRecords(Record.Unfrequent);
            var dataZip = new byte[] { 0x50, 0x4b, 0x03, 0x04 };
            var dataZipEmpty = new byte[] { 0x50, 0x4b, 0x05, 0x06 };

            var resultZip = Signature.Match(dataZip);
            var resultZipEmpty = Signature.Match(dataZipEmpty);

            Assert.IsTrue(resultZip.Contains("zip"));
            Assert.IsTrue(resultZip.Contains("docx"));
            Assert.IsTrue(resultZip.Contains("apk"));

            Assert.IsTrue(resultZipEmpty.Contains("zip"));
            Assert.IsTrue(resultZipEmpty.Contains("docx"));
            Assert.IsTrue(resultZipEmpty.Contains("apk"));
        }

        [TestMethod]
        public void OverlapTest() {
            var Signature = new Signature();
            Signature.AddRecords(Record.Common);
            Signature.AddRecords(Record.Unfrequent);

            var data = new byte[] { 0xff, 0xd8, 0xff, 0xdb };
            Signature.AddRecord(data, new[] { "jpegx" });
            var result = Signature.Match(data);

            Assert.IsTrue(result.Contains("jpg"));
            Assert.IsTrue(result.Contains("jpeg"));
            Assert.IsTrue(result.Contains("jpegx"));
        }

        [TestMethod]
        public void PdbTest() {
            var Signature = new Signature();
            Signature.AddRecords(Record.Unfrequent);

            var data = new byte[]
            {
                0x11, 0x11, 0x11, 0x22, 0x00,
                0x66, 0x74, 0x79, 0x70, 0x00,
                0x33, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00,
                0x20, 0xff, 0x11, 0x1f, 0x40
            };
            var result = Signature.Match(data);

            Assert.IsTrue(result.Contains("pdb"));
        }

        [TestMethod]
        public void GifTest() {
            var Signature = new Signature();
            Signature.AddRecords(Record.Common);

            var data = new byte[]
            {
                0x47, 0x49, 0x46, 0x38, 0x39,
                0x61, 0x2c, 0x01, 0xe0, 0x00
            };
            var results = Signature.Match(data, true);

            Assert.IsTrue(results.Contains("gif"));
            Assert.IsFalse(results.Contains("mpg"));
        }
    }
}