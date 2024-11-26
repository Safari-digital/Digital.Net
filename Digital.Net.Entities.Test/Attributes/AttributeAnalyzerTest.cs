using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Digital.Net.Entities.Attributes;
using Digital.Net.Entities.Entities.Models;
using Digital.Net.TestTools;
using Microsoft.EntityFrameworkCore;

namespace Digital.Net.Entities.Test.Attributes;

// ReSharper disable once InconsistentNaming
public class AttributeAnalyzerTest : UnitTest
{
    private enum ETestFlag
    {
        TestValue
    }

    [Index(nameof(UniqueProperty), IsUnique = true)]
    private class TestEntity : EntityBase
    {
        [Required] public string RequiredProperty { get; set; }

        public string UniqueProperty { get; set; }

        [MaxLength(50)] public string MaxLengthProperty { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdentityProperty { get; set; }

        [ForeignKey("ForeignKeyProperty")] public int ForeignKeyProperty { get; set; }

        [Column("form_property")]
        [DataFlag("test_flag")]
        public DateTime FormProperty { get; set; }

        [Secret] public string SecretProperty { get; set; }
    }

    [Fact]
    public void IsRequired_ReturnsTrue_ForRequiredProperty() =>
        Assert.True(AttributeAnalyzer<TestEntity>.IsRequired("RequiredProperty"));

    [Fact]
    public void IsRequired_ReturnsFalse_ForNonRequiredProperty() =>
        Assert.False(AttributeAnalyzer<TestEntity>.IsRequired("UniqueProperty"));

    [Fact]
    public void IsUnique_ReturnsTrue_ForUniqueProperty() =>
        Assert.True(AttributeAnalyzer<TestEntity>.IsUnique("UniqueProperty"));

    [Fact]
    public void IsUnique_ReturnsFalse_ForNonUniqueProperty() =>
        Assert.False(AttributeAnalyzer<TestEntity>.IsUnique("RequiredProperty"));

    [Fact]
    public void MaxLength_ReturnsCorrectLength_ForMaxLengthProperty() =>
        Assert.Equal(50, AttributeAnalyzer<TestEntity>.MaxLength("MaxLengthProperty"));

    [Fact]
    public void MaxLength_ReturnsZero_ForNonMaxLengthProperty() =>
        Assert.Equal(0, AttributeAnalyzer<TestEntity>.MaxLength("RequiredProperty"));

    [Fact]
    public void IsIdentity_ReturnsTrue_ForIdentityProperty() =>
        Assert.True(AttributeAnalyzer<TestEntity>.IsIdentity("IdentityProperty"));

    [Fact]
    public void IsIdentity_ReturnsFalse_ForNonIdentityProperty() =>
        Assert.False(AttributeAnalyzer<TestEntity>.IsIdentity("RequiredProperty"));

    [Fact]
    public void IsForeignKey_ReturnsTrue_ForForeignKeyProperty() =>
        Assert.True(AttributeAnalyzer<TestEntity>.IsForeignKey("ForeignKeyProperty"));

    [Fact]
    public void IsForeignKey_ReturnsFalse_ForNonForeignKeyProperty() =>
        Assert.False(AttributeAnalyzer<TestEntity>.IsForeignKey("RequiredProperty"));

    [Fact]
    public void IsSecret_ReturnsTrue_ForSecretProperty() =>
        Assert.True(AttributeAnalyzer<TestEntity>.IsSecret("SecretProperty"));

    [Fact]
    public void GetPath_ReturnsCorrectPath_ForColumnProperty() =>
        Assert.Equal("form_property", AttributeAnalyzer<TestEntity>.GetPath("FormProperty"));

    [Fact]
    public void GetPath_ReturnsPropertyName_ForNonColumnProperty() =>
        Assert.Equal("RequiredProperty", AttributeAnalyzer<TestEntity>.GetPath("RequiredProperty"));

    [Fact]
    public void GetDataFlag_ReturnsCorrectFlag_ForFormProperty() =>
        Assert.Equal("test_flag", AttributeAnalyzer<TestEntity>.GetDataFlag("FormProperty"));

    [Fact]
    public void GetDataFlag_ReturnsNull_ForNonFormProperty() =>
        Assert.Null(AttributeAnalyzer<TestEntity>.GetDataFlag("RequiredProperty"));
}