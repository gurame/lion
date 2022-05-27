using FluentValidation.Results;

namespace Lion.Core.Domain.Exceptions;
public class ModelValidationException : Exception
{
    private readonly Dictionary<string, string[]> _errors;

    public ModelValidationException(IEnumerable<ValidationFailure> failures) : base("One or more failures have occurred.")
    {
        var failureGroups = failures.GroupBy(x => x.PropertyName, e => e.ErrorMessage + " " + e.AttemptedValue);
        var dictionary = new Dictionary<string, string[]>();
        foreach (var item in failureGroups)
        {
            dictionary.Add(item.Key, item.ToArray());
        }
        _errors = dictionary;
    }
    public Dictionary<string, string[]> Errors => _errors;
}
