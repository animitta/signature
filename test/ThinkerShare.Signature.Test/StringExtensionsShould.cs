using System;
using ThinkerShare.Signature.Extensions;
using Xunit;

namespace ThinkerShare.Signature.Test
{
    public class StringExtensionsShould
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

        [Fact]
        public void BeEqualWhenNotUnknownExtension()
        {
            const string extension = ".kkk";
            var result = extension.GetMimeType();
            Assert.Equal("application/octet-stream", result);
        }

        [Fact]
        public void ThrowExceptionWhenEmptyExtension()
        {
            var extension = "";
            var exception = Assert.Throws<ArgumentNullException>(() => extension.GetMimeType());
            Assert.NotNull(exception);

            extension = " ";
            exception = Assert.Throws<ArgumentNullException>(() => extension.GetMimeType());
            Assert.NotNull(exception);

            extension = null;
            exception = Assert.Throws<ArgumentNullException>(() => extension.GetMimeType());
            Assert.NotNull(exception);
        }
    }
}
