namespace Product.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, string[]> errors)
            : base("Validation failed for the request.")
        {
            Errors = errors;
        }
    }
}
