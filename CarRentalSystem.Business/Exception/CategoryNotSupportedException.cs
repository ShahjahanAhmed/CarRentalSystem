namespace CarRentalSystem.Business.Exception
{
    public class CategoryNotSupportedException : System.Exception
    {
        public CategoryNotSupportedException(string categoryName) : base($"'{categoryName}' category not supported.")
        {
        }
    }
}
