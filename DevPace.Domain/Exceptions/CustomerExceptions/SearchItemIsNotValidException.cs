namespace DevPace.Domain.Exceptions.CustomerExceptions
{
    public sealed class SearchItemIsNotValidException : Exception
    {
        public SearchItemIsNotValidException()
            : base($"Search item is null or not valid.")
        {
        }
    }
}
