namespace Lion.Core.Application.Sellers.Commands.Add
{
    public record AddResult
    {
        public string SellerId { get; init; }
        public string TaxId { get; init; }
        public string Name { get; init; }
    }
}
