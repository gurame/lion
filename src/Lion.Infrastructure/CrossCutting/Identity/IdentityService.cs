using Lion.Core.Application._Common.Interfaces;

namespace Lion.Infrastructure.CrossCutting.Identity;

public class IdentityService : IIdentityService
{
    public string GetUserId()
    {
        return "70436116";
    }

    public string GetUserName()
    {
        return "gurame";
    }
}
