namespace DevPace.Domain.Exceptions.CustomerExceptions
{
    public sealed class FieldNotUniqueException : Exception
    {
        public FieldNotUniqueException(string field, string value)
            : base($"Entity with property {field} and value {value} is already exists.")
        {
        }
    }
}
