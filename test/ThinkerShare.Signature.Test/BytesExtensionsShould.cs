using Xunit;
using ThinkerShare.Signature.Extensions;

namespace ThinkerShare.Signature.Test
{
    public class BytesExtensionsShould
    {
        [Fact]
        public void BeEqualWhenGetExtension()
        {
            var data = new byte[] { 0xff, 0xd8, 0xff, 0xdb };
            var extension = data.GetExtension();
            Assert.Contains("jpg", extension);
        }

        [Fact]
        public void BeEqualWhenNotUnknownExtension()
        {
            var data = new byte[] { 0xff, 0xd8, 0xff, 0xdb };
            var extensions = data.GetExtensions();

            Assert.Contains("jpg", extensions);
            Assert.Contains("jpeg", extensions);
        }
    }
}
