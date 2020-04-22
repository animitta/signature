using Xunit;
using ThinkerShare.Signature.Extensions;

namespace ThinkerShare.Signature.Test
{
    public class FileExtensionStringExtensionsShould
    {
        [Fact]
        public void BeEqualWhenGetMimeType()
        {
            var extension = ".jpg";

            var result = extension.GetMimeType();
            Assert.Equal("image/jpeg", result);

            extension = ".txt";
            result = extension.GetMimeType();
            Assert.Equal("text/plain", result);
        }
    }
}
