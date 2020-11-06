using System;
using Xunit;

namespace Felt.Redactor.Tests
{
    public class RedactorOptionsTests
    {
        [Fact]
        public void Defaults()
        {
            Assert.Equal("[REDACTED]", RedactorOptions.DefaultMask);

            var options = new RedactorOptions();

            Assert.Equal(ComplexTypeHandling.RedactValue, options.ComplexTypeHandling);
            Assert.Null(options.IfIsRedacts);
            Assert.Equal(RedactorOptions.DefaultMask, options.Mask);
            Assert.Equal(OnErrorRedact.All, options.OnErrorRedact);
            Assert.Null(options.Redacts);
            Assert.Equal(StringComparison.OrdinalIgnoreCase, options.StringComparison);
        }
    }
}