namespace Lion.Core.Domain.Exceptions;

public class SellerAlreadyExistsException : DomainException
{
    public SellerAlreadyExistsException(string taxId)
        : base($"Seller with tax id \"{taxId}\" already exists.")
    {

    }
}
