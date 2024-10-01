namespace Ecommerce.Service.src.Shared.Interface
{
    public abstract class IDataValidator<T>
    {
        private readonly List<Func<T, ValidationResult>> _rules = [];

        protected void RuleFor(
            Func<T, object> property,
            Func<object, bool> predicate,
            string errorMessage
        )
        {
            _rules.Add(instance =>
            {
                var value = property(instance);
                return predicate(value)
                    ? ValidationResult.Success()
                    : ValidationResult.Failure(errorMessage);
            });
        }

        public ValidationResult Validate(T instance)
        {
            foreach (var rule in _rules)
            {
                var result = rule(instance);
                if (!result.IsValid)
                {
                    return result;
                }
            }
            return ValidationResult.Success();
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; }
        public string ErrorMessage { get; }

        private ValidationResult(bool isValid, string errorMessage)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }

        public static ValidationResult Success() => new ValidationResult(true, null);

        public static ValidationResult Failure(string errorMessage) =>
            new ValidationResult(false, errorMessage);
    }
}
