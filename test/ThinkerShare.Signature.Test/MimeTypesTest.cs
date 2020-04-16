using Xunit;
using ThinkerShare.Signature.Extensions;

namespace ThinkerShare.Signature.Test {
    public class MimeTypesTest {
        [Fact]
        public void DotTest() {
            var extension = ".jpg";

            var result = extension.GetMimeType();
            Assert.Equal("image/jpeg", result);
        }

        [Fact]
        public void RegularTest() {
            var extension = ".txt";
            var result = FileExtensionStringExtensions.GetMimeType(extension);
            Assert.Equal("text/plain", result);
        }
    }
}
