namespace CarRentalSystem.Business.Exception
{
    /// <summary>
    /// The exception that is thrown when an attempt to resolve a type from container is made even though the type is not registered.
    /// </summary>
    public class ContainerResolutionException : System.Exception
    {
        public ContainerResolutionException(string typeFullName) : base(typeFullName)
        {

        }
    }
}
