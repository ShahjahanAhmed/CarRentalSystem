namespace CarRentalSystem.Business.Exception
{
    public class ContainerResolutionException : System.Exception
    {
        public ContainerResolutionException(string typeFullName) : base(typeFullName)
        {
            
        }
    }
}
