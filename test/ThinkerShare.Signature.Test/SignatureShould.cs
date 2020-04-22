using Xunit;

namespace ThinkerShare.Signature.Test
{
    public class SignatureShould
    {
        [Fact]
        public void BeContainsWhenMatch()
        {
            var Signature = new Signature();
            var data = new byte[] { 0x11, 0x22, 0x33 };
            Signature.AddRecord(data, new[] { "what", "file", "type" });

            var result = Signature.Match(data);
            Assert.Contains("what", result);
            Assert.Contains("file", result);
            Assert.Contains("type", result);
        }

        [Fact]
        public void BeContainsWhenComplexMatch()
        {
            var Signature = new Signature();
            var record = new Record("a,b,c", "0x11 0x22 ?? ?? ?? 0x33", 2);
            Signature.AddRecord(record);

            var data = new byte[] { 0x11, 0x11, 0x11, 0x22, 0xff, 0xdd, 0x1d, 0x33 };
            var result = Signature.Match(data);

            Assert.Contains("a", result);
            Assert.Contains("b", result);
            Assert.Contains("c", result);
        }

        [Fact]
        public void BeContainsJpgWhenMatch()
        {
            var Signature = new Signature();
            Signature.AddRecords(Record.FrequentRecords);
            Signature.AddRecords(Record.UnfrequentRecords);

            var data = new byte[] { 0xff, 0xd8, 0xff, 0xdb };
            var result = Signature.Match(data);

            Assert.Contains("jpg", result);
            Assert.Contains("jpeg", result);


            Signature = new Signature();
            Signature.AddRecords(Record.FrequentRecords);

            data = new byte[]
            {
                0xff, 0xd8, 0xff, 0xe0,
                0x66, 0x74, 0x4a, 0x46,
                0x49, 0x46, 0x00, 0x01,
            };
            result = Signature.Match(data);

            Assert.Contains("jpg", result);
            Assert.Contains("jpeg", result);


            Signature = new Signature();
            Signature.AddRecords(Record.FrequentRecords);

            data = new byte[]
            {
                0xff, 0xd8, 0xff, 0xe1,
                0x66, 0x74, 0x45, 0x78,
                0x69, 0x66, 0x00, 0x00,
            };
            result = Signature.Match(data);

            Assert.Contains("jpg", result);
            Assert.Contains("jpeg", result);
        }

        [Fact]
        public void BeContainsPdfWhenMatch()
        {
            var Signature = new Signature();
            Signature.AddRecords(Record.FrequentRecords);
            Signature.AddRecords(Record.UnfrequentRecords);
            var data = new byte[] { 0x25, 0x50, 0x44, 0x46, 0x11 };
            Signature.AddRecord(data, new[] { "pdfx" });

            var result = Signature.Match(data, true);

            Assert.Contains("pdf", result);
            Assert.Contains("pdfx", result);
        }

        [Fact]
        public void BeContains3gpWhenMatch()
        {
            var Signature = new Signature();
            Signature.AddRecords(Record.UnfrequentRecords);

            var data = new byte[] {
                0x11, 0x11, 0x11, 0x22,
                0x66, 0x74, 0x79, 0x70, 0x33, 0x67
            };
            var result = Signature.Match(data);

            Assert.NotEmpty(result);
            Assert.Contains("3gp", result);
            Assert.Contains("3g2", result);
        }

        [Fact]
        public void BeContainsWhenMultipleMatch()
        {
            var Signature = new Signature();
            Signature.AddRecords(Record.FrequentRecords);
            Signature.AddRecords(Record.UnfrequentRecords);

            var dataZip = new byte[] { 0x50, 0x4b, 0x03, 0x04 };
            var dataZipEmpty = new byte[] { 0x50, 0x4b, 0x05, 0x06 };
            var resultZip = Signature.Match(dataZip);
            var resultZipEmpty = Signature.Match(dataZipEmpty);

            Assert.Contains("apk", resultZip);
            Assert.Contains("zip", resultZip);
            Assert.Contains("docx", resultZip);

            Assert.Contains("zip", resultZipEmpty);
            Assert.Contains("apk", resultZipEmpty);
            Assert.Contains("docx", resultZipEmpty);
        }

        [Fact]
        public void BeContainsWhenOverlap()
        {
            var Signature = new Signature();
            Signature.AddRecords(Record.FrequentRecords);
            Signature.AddRecords(Record.UnfrequentRecords);
            var data = new byte[] { 0xff, 0xd8, 0xff, 0xdb };
            Signature.AddRecord(data, new[] { "jpegx" });

            var result = Signature.Match(data);

            Assert.Contains("jpg", result);
            Assert.Contains("jpeg", result);
            Assert.Contains("jpegx", result);
        }

        [Fact]
        public void BeContainsPdbWhenMatch()
        {
            var Signature = new Signature();
            Signature.AddRecords(Record.UnfrequentRecords);

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

            Assert.Contains("pdb", result);
        }

        [Fact]
        public void BeContainsGifWhenMatch()
        {
            var Signature = new Signature();
            Signature.AddRecords(Record.FrequentRecords);

            var data = new byte[]
            {
                0x47, 0x49, 0x46, 0x38, 0x39,
                0x61, 0x2c, 0x01, 0xe0, 0x00
            };
            var results = Signature.Match(data, true);

            Assert.Contains("gif", results);
            Assert.DoesNotContain("mpg", results);
        }
    }
}
