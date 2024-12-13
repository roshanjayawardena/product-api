namespace Product.Application.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException()
        {
        }

        public BadRequestException(string message)
              : base(message)
        {
        }


        public BadRequestException(string? message, IDictionary<string, string[]> erros) : base(message)
        {
            Errors = erros;
        }

        public IDictionary<string, string[]> Errors { get; }

    }
}
