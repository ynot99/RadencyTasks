namespace HomeTask2.Core.Exceptions
{
    public class ValidationFailedException : Exception
    {
        public ValidationFailedException() { }
        public ValidationFailedException(string message) : base(message) { }
    }
}
