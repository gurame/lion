using Lion.Core.Application._Common.Interfaces;

namespace Lion.Infrastructure.Services;

public class UUID : IUUID
{
    public string Id => Guid.NewGuid().ToString();
}
