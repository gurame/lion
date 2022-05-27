namespace Lion.Core.Domain.Events;
public class SellerAddedEvent : BaseEvent
{
    public SellerAddedEvent(Seller seller)
    {
        Seller = seller;
    }
    public Seller Seller { get; }
}
