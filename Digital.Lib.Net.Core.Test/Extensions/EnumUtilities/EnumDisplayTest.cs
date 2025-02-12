using System.ComponentModel.DataAnnotations;
using Digital.Lib.Net.Core.Extensions.EnumUtilities;
using Digital.Lib.Net.TestTools;

namespace Digital.Lib.Net.Core.Test.Extensions.EnumUtilities;

public class EnumDisplayTest : UnitTest
{
    [Fact]
    public void GetDisplayName_ReturnsEnumDisplayName_WhenAttributeIsSet() =>
        Assert.Equal("Test of very simple case", ETest.TestEnumValue.GetDisplayName());

    [Fact]
    public void GetDisplayName_ReturnsEmptyString_WhenAttributeIsNotSet() =>
        Assert.Equal(string.Empty, ETest.TestEnumValue2.GetDisplayName());



    [Fact]
    public void GetEnumDisplayNames_ReturnsEnumDisplayNames() =>
        Assert.Equal(
            ["Test of very simple case", string.Empty],
            EnumDisplay.GetEnumDisplayNames<ETest>()
        );

    [Fact]
    public void ToReferenceString_ReturnsCorrectString() =>
        Assert.Equal("TEST_ENUM_VALUE", ETest.TestEnumValue.ToReferenceString());

    private enum ETest
    {
        [Display(Name = "Test of very simple case")]
        TestEnumValue,
        TestEnumValue2
    }
}
