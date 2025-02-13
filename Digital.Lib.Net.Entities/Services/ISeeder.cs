using Digital.Lib.Net.Core.Messages;
using Digital.Lib.Net.Entities.Models;

namespace Digital.Lib.Net.Entities.Services;

public interface ISeeder<T> where T : Entity
{
    /// <summary>
    ///     Seed data into the database. If the data already exists, it will not be seeded.
    /// </summary>
    /// <remarks>
    ///     The data is compared to existing entities and will not be seeded if they already exist,
    ///     based on the following criteria:
    ///     <ul>
    ///         <li>Id is not compared.</li>
    ///         <li>CreatedAt is not compared.</li>
    ///         <li>UpdatedAt is not compared.</li>
    ///         <li>Any boolean values aren't compared.</li>
    ///     </ul>
    /// </remarks>
    /// <param name="data">
    ///     A list of entities to seed into the database.
    /// </param>
    public Task<Result<List<T>>> SeedAsync(List<T> data);
}