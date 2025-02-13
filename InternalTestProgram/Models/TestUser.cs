using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Digital.Lib.Net.Authentication.Models;
using Digital.Lib.Net.Core.Random;
using Digital.Lib.Net.Entities.Attributes;
using Digital.Lib.Net.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace InternalTestProgram.Models;

[Table("TestUser"), Index(nameof(Username), nameof(Email), IsUnique = true)]
public class TestUser : EntityGuid, IApiUser
{
    [Column("Username"), MaxLength(24), Required, RegexValidation("^[a-zA-Z0-9.'@_-]{6,24}$")]
    public string Username { get; set; } = Randomizer.GenerateRandomString();

    [Column("Password"), MaxLength(64), Required, Secret]
    public string Password { get; set; } = Randomizer.GenerateRandomString();

    public string Login { get; set; } = Randomizer.GenerateRandomString();
    public bool IsActive { get; set; } = true;

    [Column("Email"), MaxLength(254), Required, RegexValidation(@"^[^@]+@[^@]+\.[^@]{2,253}$")]
    public string Email { get; set; } = Randomizer.GenerateRandomString();

    [Column("Role"), ReadOnly]
    public virtual TestRole? Role { get; set; }

    [Column("State")]
    public TestState State { get; set; } = TestState.StateValue1;

    [Column("NestedObject")]
    public virtual TestNestedObject NestedObject { get; set; } = new();

    public void Update(NullableTestUser payload)
    {
        Username = payload.Username ?? Username;
        Password = payload.Password ?? Password;
        Login = payload.Login ?? Login;
        Email = payload.Email ?? Email;
        Role = payload.Role ?? Role;
        IsActive = payload.IsActive ?? IsActive;
    }
}

public enum TestState
{
    StateValue1,
    StateValue2,
    StateValue3
}

public class NullableTestUser
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Login { get; set; }
    public bool? IsActive { get; set; }
    public string? Email { get; set; }
    public virtual TestRole? Role { get; set; }
}