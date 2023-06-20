namespace DevPace.Domain.Exceptions.CustomerExceptions
{
    public sealed class CustomerIdNotFoundException : Exception
    {
        public CustomerIdNotFoundException(Guid id)
            : base($"Customer with id ({id}) is not found.")
        {
        }
    }
}
