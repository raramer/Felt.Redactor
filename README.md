# Redactor
Redactor is a set of libraries used to make it easy to mask sensitive information within structured text.

### How do I get started

Configure the redactor that fits the type of structured data you want to redact.  In it's simplest form this is simply a list of field names you want to mask.  Then call the Redact method with the structured text.

```csharp
var redactor = new JsonRedactor("password");

var json = @"{
               ""username"": ""jdoe"",
               ""password": ""P@ssw0rd!""
             }";
var redactedJson = redactor.Redact(json); 
/*
 * {"username":"jdoe","password":"[REDACTED]"}
 */
```

### Options

* Redacts : a list of fields whose values need to be masked.

_See example above._

* IfIsRedacts : a list of rules for optional masking.

** If : the name of a field you want to check.
** Is : the expected value of field to enable masking.
** Redact: the name of a field whose value needs to be masked.

```csharp
var redactor = new JsonRedactor(new IfIsRedact 
{ 
    If = "name", 
    Is = "username", 
    Redact = "value" 
});

var json = @"{
               ""properties"": [
                 {
                   ""name"": ""username"",
                   ""value"": ""jdoe""
                 },
                 {
                   ""name"": ""password"",
                   ""value"": ""P@ssw0rd!""
                 },
               ]
             }";
var redactedJson = redactor.Redact(json);
/*
 * {"properties":[{"name":"username","value":"jdoe"},{"name":"password","value":"[REDACTED]"}}
 */
```

* Mask : the value used to mask redacted values.  The default is "[REDACTED]", but you can provide the value you prefer.  e.g. "********"

* StringComparison : how to compare values.  The default is OrdinalIgnoreCase.

* ComplexTypeHandling : how to mask complex types.
** RedactValue (default) - redacts the entire value.
** RedactDescendants - redacts each descendant value, preserving the complex type's data structure.

* OnErrorRedact : how to handle the response when the value does not conform to the structured text type.
** All (default) - redacts the entire response.
** None - the original value is returned.

