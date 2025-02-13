using Digital.Lib.Net.Entities.Models;

namespace Digital.Lib.Net.Authentication.Models.Authorizations;

public interface IAuthorizationKey : IEntity
{
    public string Key { get; }
    public Guid ApiUserId { get; }
    public DateTime? ExpiredAt { get; }
}