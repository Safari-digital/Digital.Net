using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Safari.Net.Data.Models;

/// <summary>
///     Base class for entities with an integer primary key
/// </summary>
public abstract class EntityWithId : EntityBase
{
    [Column("id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
}