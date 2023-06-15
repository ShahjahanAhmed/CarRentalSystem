namespace CarRentalSystem.Business.Exception
{
    /// <summary>
    /// The exception that is thrown when data entry is not found with a given reference.
    /// </summary>
    public class EntryNotFoundException :  System.Exception
    {
        public EntryNotFoundException(string message) : base(message)
        {
            
        }
    }
}
