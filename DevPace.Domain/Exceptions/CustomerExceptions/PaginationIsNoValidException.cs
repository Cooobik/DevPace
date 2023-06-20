namespace DevPace.Domain.Exceptions.CustomerExceptions
{
    public sealed class PaginationIsNoValidException : Exception
    {
        public PaginationIsNoValidException(int rowCount, int pageNumber)
            : base($"Pagination is not valid. Row count: {rowCount}, Page number: {pageNumber}")
        {
        }
    }
}
