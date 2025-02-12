﻿using Digital.Lib.Net.Core.Extensions.FormFileUtilities;
using Digital.Lib.Net.TestTools;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Digital.Lib.Net.Core.Test.Extensions.FormFileUtilities;

public class FormFileInfoTest : UnitTest
{
    private readonly Mock<IFormFile> _mockFormFile = new();

    [Fact]
    public void GetExtension_ReturnsExtension()
    {
        _mockFormFile
            .Setup(f => f.FileName)
            .Returns("test.txt");

        var result = _mockFormFile.Object.GetExtension();
        Assert.Equal(".txt", result);
    }

    [Fact]
    public void GenerateFileName_ReturnsCorrectFileNameLength_WhenExtensionLengthIs4()
    {
        _mockFormFile
            .Setup(f => f.FileName)
            .Returns("test.txt");

        var result = _mockFormFile.Object.GenerateFileName();
        Assert.Equal(64, result.Length);
        Assert.EndsWith(".txt", result);
    }

    [Fact]
    public void GenerateFileName_ReturnsCorrectFileNameLength_WhenExtensionLengthIs3()
    {
        _mockFormFile
            .Setup(f => f.FileName)
            .Returns("test.cs");

        var result = _mockFormFile.Object.GenerateFileName();
        Assert.Equal(64, result.Length);
        Assert.EndsWith(".cs", result);
    }
}