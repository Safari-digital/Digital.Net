using Digital.Net.Core.Random;
using Digital.Net.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Digital.Net.Entities.Test.TestUtilities.Models;

[Index(nameof(Name), IsUnique = true)]
public class FakeRole : EntityWithId
{
    public string Name { get; set; } = Randomizer.GenerateRandomString();
    public ERole Role { get; set; } = ERole.User;
}

public enum ERole
{
    SuperAdmin,
    Admin,
    User
}