using System;
using System.Collections.Generic;

namespace Felt.Redactor
{
    public class RedactorOptions
    {
        public static readonly string DefaultMask = "[REDACTED]";

        public ComplexTypeHandling ComplexTypeHandling { get; set; } = ComplexTypeHandling.RedactValue;

        public IEnumerable<IfIsRedact> IfIsRedacts { get; set; }

        public string Mask { get; set; } = DefaultMask;

        public OnErrorRedact OnErrorRedact { get; set; } = OnErrorRedact.All;

        public IEnumerable<string> Redacts { get; set; }

        public StringComparison StringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;
    }
}