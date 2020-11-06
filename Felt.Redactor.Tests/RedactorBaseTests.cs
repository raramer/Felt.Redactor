using System;
using System.Linq;
using Xunit;

namespace Felt.Redactor.Tests
{
    public class RedactorBaseTests
    {
        [Fact]
        public void RedactorOptionsIsNull()
        {
            var redactor = new ExposedOptionsRedactor(null);

            Assert.Equal(ComplexTypeHandling.RedactValue, redactor.Options.ComplexTypeHandling);
            Assert.Null(redactor.Options.IfIsRedacts);
            Assert.Equal(RedactorOptions.DefaultMask, redactor.Options.Mask);
            Assert.Equal(OnErrorRedact.All, redactor.Options.OnErrorRedact);
            Assert.Null(redactor.Options.Redacts);
            Assert.Equal(StringComparison.OrdinalIgnoreCase, redactor.Options.StringComparison);
        }

        [Fact]
        public void RedactorOptionsIsSpecified()
        {
            // Define non-default options
            var options = new RedactorOptions
            {
                ComplexTypeHandling = ComplexTypeHandling.RedactValue,
                IfIsRedacts = new[]
                {
                    new IfIsRedact("GHI", "3", "JKL"),
                    new IfIsRedact("MNO", "5", "PQR"),
                    null,
                },
                Mask = Guid.NewGuid().ToString("n"),
                OnErrorRedact = OnErrorRedact.None,
                Redacts = new[]
                {
                    "ABC",
                    "DEF",
                    null
                },
                StringComparison = StringComparison.CurrentCulture
            };

            // Ensure not the defaults
            Assert.NotEqual(ComplexTypeHandling.RedactDescendants, options.ComplexTypeHandling);
            Assert.NotNull(options.IfIsRedacts);
            Assert.NotEqual(RedactorOptions.DefaultMask, options.Mask);
            Assert.NotEqual(OnErrorRedact.All, options.OnErrorRedact);
            Assert.NotNull(options.Redacts);
            Assert.NotEqual(StringComparison.OrdinalIgnoreCase, options.StringComparison);

            // create redactor
            var redactor = new ExposedOptionsRedactor(options);

            // assert options
            Assert.Equal(options.ComplexTypeHandling, redactor.Options.ComplexTypeHandling);

            var expectedIfIsRedacts = options.IfIsRedacts.Where(i => i != null);
            Assert.NotNull(redactor.Options.IfIsRedacts);
            Assert.Equal(expectedIfIsRedacts.Count(), redactor.Options.IfIsRedacts.Count());
            foreach (var ifisredact in expectedIfIsRedacts)
            {
                Assert.DoesNotContain(ifisredact, redactor.Options.IfIsRedacts);
                Assert.Contains(redactor.Options.IfIsRedacts, i => i.If == ifisredact.If && i.Is == ifisredact.Is && i.Redact == ifisredact.Redact);
            }

            Assert.Equal(options.Mask, redactor.Options.Mask);

            Assert.Equal(options.OnErrorRedact, redactor.Options.OnErrorRedact);

            var expectedRedacts = options.Redacts.Where(i => i != null);
            Assert.NotNull(redactor.Options.Redacts);
            Assert.Equal(expectedRedacts.Count(), redactor.Options.Redacts.Count());
            foreach (var redact in expectedRedacts)
            {
                Assert.Contains(redact, redactor.Options.Redacts);
            }

            Assert.Equal(options.StringComparison, redactor.Options.StringComparison);
        }

        [Theory]
        [InlineData(default(string))] // null
        [InlineData("")] // empty
        [InlineData(" ")] // whitespace
        [InlineData("[REDACTED]")] // default
        [InlineData("*********")] // asterisks
        public void SanitizeMaskCalled(string mask)
        {
            var redactor = new ExposedOptionsRedactor(new RedactorOptions { Mask = mask });

            Assert.Equal(mask, redactor.SanitizeMaskCalledWith);
        }

        internal sealed class ExposedOptionsRedactor : RedactorBase
        {
            public RedactorOptions Options => _options;

            public string SanitizeMaskCalledWith { get; private set; }

            public ExposedOptionsRedactor(RedactorOptions options) : base(options)
            {
            }

            public override string Redact(string value)
            {
                throw new NotImplementedException();
            }

            public override bool TryRedact(string value, out string redactedValue)
            {
                throw new NotImplementedException();
            }

            public override bool TryRedact(string value, out string redactedValue, out string message)
            {
                throw new NotImplementedException();
            }

            protected override string SanitizeMask(string mask)
            {
                SanitizeMaskCalledWith = mask;
                return mask;
            }
        }
    }
}