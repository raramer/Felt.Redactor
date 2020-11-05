namespace Felt.Redactor
{
    public class IfIsRedact
    {
        public string If { get; set; }

        public string Is { get; set; }

        public string Redact { get; set; }

        public IfIsRedact()
        {
        }

        public IfIsRedact(string @if, string @is, string redact)
        {
            If = @if;
            Is = @is;
            Redact = @redact;
        }
    }
}