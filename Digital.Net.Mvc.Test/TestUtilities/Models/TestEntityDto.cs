namespace Digital.Net.Mvc.Test.TestUtilities.Models;

public class TestIdEntityDto
{
    public TestIdEntityDto()
    {
    }

    public TestIdEntityDto(TestIdEntity entity)
    {
        Id = entity.Id;
        Username = entity.Username;
        Password = entity.Password;
        Email = entity.Email;
    }

    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}

public class TestGuidEntityDto
{
    public TestGuidEntityDto()
    {
    }

    public TestGuidEntityDto(TestGuidEntity entity)
    {
        Id = entity.Id;
        Username = entity.Username;
        Password = entity.Password;
        Email = entity.Email;
    }

    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}