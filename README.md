# Redactor
Redactor is a set of libraries used to make it easy to mask sensitive information within structured text.

### How do I get started

1. Download and reference the implementation that best fits your needs.  All packages are available on nuget.org

    [Felt.Redactor.Json](https://github.com/raramer/Felt.Redactor.Json) : redacts json structured text.

    [Felt.Redactor.Xml](https://github.com/raramer/Felt.Redactor.Xml) : redacts xml structured text.

2. Configure the redactor that fits the type of structured data you want to redact.  In it's simplest form this is simply a list of field names you want to mask.  
    ```csharp
    var redactor = new JsonRedactor("password");

    var json = @"{
                   ""username"": ""jdoe"",
                   ""password": ""P@ssw0rd!""
                 }";
    ```
3. Call the Redact method with the structured text.             
    ```          
    var redactedJson = redactor.Redact(json); 
    ```
    ```
    {"username":"jdoe","password":"[REDACTED]"}
    ```

### Common Options

* Redacts : a list of fields whose values need to be masked.

    See example above.

* IfIsRedacts : a list of rules for optional masking.

  * If : the name of a field you want to check.
  * Is : the expected value of the If field to enable masking of the Redact field.
  * Redact: the name of a field whose value needs to be masked. This can either be the If property or one of its siblings.

    ```csharp
    var redactor = new JsonRedactor(new IfIsRedact 
    { 
        If = "name", 
        Is = "password", 
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
    ```
    ```
    {"properties":[{"name":"username","value":"jdoe"},{"name":"password","value":"[REDACTED]"}}
    ```

* Mask : the value used to mask redacted values. The default is "[REDACTED]", but you can provide the value you prefer.  e.g. "********"

* StringComparison : how to compare values. The default is OrdinalIgnoreCase.

* ComplexTypeHandling : how to mask complex types.
  * RedactValue (default) - redacts the entire value.
     ```
    {"total":19.99,"creditCard":"[REDACTED]"}
    ```
  * RedactDescendants - redacts each descendant value, preserving the complex type's data structure.
    ```
    {"total":19.99,"creditCard":{"type":"[REDACTED]","number":"[REDACTED]","expiration":"[REDACTED]","cvv":"[REDACTED]" } }
    ``` 

* OnErrorRedact : how to handle the response when the value does not conform to the structured text type.
  * All (default) - redacts the entire response.
  * None - the original value is returned.

* Formatting : some redactors support options for formatting the response.