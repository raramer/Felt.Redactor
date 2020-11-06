namespace Felt.Redactor
{
    public interface IRedact
    {
        string Redact(string value);

        bool TryRedact(string value, out string redactedValue);

        bool TryRedact(string value, out string redactedValue, out string errorMessage);
    }
}