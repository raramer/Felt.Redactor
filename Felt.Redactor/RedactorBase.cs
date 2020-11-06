using System.Linq;

namespace Felt.Redactor
{
    public abstract class RedactorBase : IRedact
    {
        protected readonly RedactorOptions _options;

        protected RedactorBase(RedactorOptions options)
        {
            // clone options to prevent tampering after the fact
            _options = options == null ? new RedactorOptions() : new RedactorOptions
            {
                ComplexTypeHandling = options.ComplexTypeHandling,

                IfIsRedacts = options.IfIsRedacts == null
                    ? new IfIsRedact[0]
                    : options.IfIsRedacts
                        .Where(iir => iir != null && iir.If != null && iir.Redact != null)
                        .Select(iir => new IfIsRedact(iir.If, iir.Is, iir.Redact))
                        .ToArray(),

                Mask = SanitizeMask(options.Mask),

                OnErrorRedact = options.OnErrorRedact,

                StringComparison = options.StringComparison,

                Redacts = options.Redacts == null
                    ? new string[0]
                    : options.Redacts
                        .Where(r => r != null)
                        .ToArray(),
            };
        }

        /// <summary>
        /// Redact the value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract string Redact(string value);

        /// <summary>
        /// Try to redact the value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool TryRedact(string value, out string redactedValue);

        /// <summary>
        /// Try to redact the value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool TryRedact(string value, out string redactedValue, out string message);

        /// <summary>
        /// Allow implementation to encode special characters
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        protected abstract string SanitizeMask(string mask);
    }
}