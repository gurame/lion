namespace Lion.Core.Domain.Entities;
public class Seller : BaseAuditableEntity
{
    protected Seller() { }
    public Guid SellerId { get; private set; }
    public string TaxId { get; private set; }
    public string Name { get; private set; }

    public static class Factory
    {
        public static Seller Create(Guid sellerId, string taxId, string name)
        {
            var entity = new Seller()
            {
                SellerId = sellerId,
                TaxId = taxId,
                Name = name
            };

            return entity;
        }
    }
}
