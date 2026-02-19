namespace Gasolutions.Core.BeautifulGuid.Tests;

/// <summary>
/// Test suite for ToStringExtensions.
/// Tests the conversion of beautiful GUID strings back to standard UUID format.
/// </summary>
public class ToStringExtensionsTests
{
    /// <summary>
    /// Verifies that a valid beautiful GUID string is converted back to UUID format.
    /// </summary>
    [Fact]
    public void BeautifulGuidToString_WithValidBeautifulGuid_ReturnsUuidFormat()
    {
        // Arrange
        string beautifulGuid = "AQAAQAAA-AQAAQAAA-AQAAQAAA-AQAAQAAA";

        // Act
        string result = beautifulGuid.BeautifulGuidToString();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains("-", result);
    }

    /// <summary>
    /// Verifies that the converted UUID has the correct UUID format (8-4-4-4-12).
    /// </summary>
    [Fact]
    public void BeautifulGuidToString_ReturnsCorrectUuidFormat()
    {
        // Arrange
        string beautifulGuid = "AQAAQAAA-AQAAQAAA-AQAAQAAA-AQAAQAAA";

        // Act
        string result = beautifulGuid.BeautifulGuidToString();
        string[] parts = result.Split('-');

        // Assert
        Assert.Equal(5, parts.Length);
        Assert.Equal(4, parts[1].Length);  // Second part: 4 chars
        Assert.Equal(4, parts[2].Length);  // Third part: 4 chars
        Assert.Equal(4, parts[3].Length);  // Fourth part: 4 chars

    }

    /// <summary>
    /// Verifies that a beautiful GUID with missing parts throws an exception.
    /// </summary>
    [Fact]
    public void BeautifulGuidToString_WithInvalidFormat_ThrowsArgumentException()
    {
        // Arrange
        string invalidBeautifulGuid = "AQAAQAAA-AQAAQAAA-AQAAQAAA"; // Missing one part

        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            invalidBeautifulGuid.BeautifulGuidToString());
        Assert.Contains("Invalid beautiful GUID format", exception.Message);
    }

    /// <summary>
    /// Verifies that the converted UUID string is valid hexadecimal.
    /// </summary>
    [Fact]
    public void BeautifulGuidToString_ReturnsValidHexadecimal()
    {
        // Arrange
        string beautifulGuid = "AQAAQAAA-AQAAQAAA-AQAAQAAA-AQAAQAAA";

        // Act
        string result = beautifulGuid.BeautifulGuidToString();
        string hexCharacters = "0123456789abcdefABCDEF-";

        // Assert
        Assert.True(result.All(c => hexCharacters.Contains(c)),
            $"Converted UUID contains invalid hexadecimal characters: {result}");
    }

    /// <summary>
    /// Verifies round-trip conversion: GUID -> BeautifulGUID -> UUID produces valid result.
    /// </summary>
    [Fact]
    public void RoundTrip_GuidToBeautifulToString_ReturnsValidUuid()
    {
        // Arrange
        var originalGuid = new Guid("550e8400-e29b-41d4-a716-446655440000");

        // Act
        string beautifulGuid = originalGuid.ToBeautifulGuid();
        string convertedUuid = beautifulGuid.BeautifulGuidToString();

        // Assert
        Assert.NotNull(convertedUuid);
        Assert.NotEmpty(convertedUuid);
        string[] parts = convertedUuid.Split('-');
        Assert.Equal(5, parts.Length);
    }

    /// <summary>
    /// Verifies that different beautiful GUIDs produce different UUID strings.
    /// </summary>
    [Fact]
    public void BeautifulGuidToString_WithDifferentBeautifulGuids_ReturnsDifferentStrings()
    {
        // Arrange
        string beautifulGuid1 = "AQAAQAAA-AQAAQAAA-AQAAQAAA-AQAAQAAA";
        string beautifulGuid2 = "AQAAQAAB-AQAAQAAB-AQAAQAAB-AQAAQAAB";

        // Act
        string result1 = beautifulGuid1.BeautifulGuidToString();
        string result2 = beautifulGuid2.BeautifulGuidToString();

        // Assert
        Assert.NotEqual(result1, result2);
    }

    /// <summary>
    /// Verifies that the same beautiful GUID always produces the same UUID (consistency).
    /// </summary>
    [Fact]
    public void BeautifulGuidToString_WithSameBeautifulGuid_ReturnsSameUuid()
    {
        // Arrange
        string beautifulGuid = "AQAAQAAA-AQAAQAAA-AQAAQAAA-AQAAQAAA";

        // Act
        string result1 = beautifulGuid.BeautifulGuidToString();
        string result2 = beautifulGuid.BeautifulGuidToString();

        // Assert
        Assert.Equal(result1, result2);
    }

    /// <summary>
    /// Verifies that multiple beautiful GUIDs can be converted successfully.
    /// </summary>
    [Theory]
    [InlineData("AQAAQAAA-AQAAQAAA-AQAAQAAA-AQAAQAAA")]
    [InlineData("AQAAQAAB-AQAAQAAB-AQAAQAAB-AQAAQAAB")]
    [InlineData("ZZZZZZZZ-ZZZZZZZZ-ZZZZZZZZ-ZZZZZZZZ")]
    public void BeautifulGuidToString_WithVariousBeautifulGuids_ReturnsValidUuids(string beautifulGuid)
    {
        // Act
        string result = beautifulGuid.BeautifulGuidToString();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        string[] parts = result.Split('-');
        Assert.Equal(5, parts.Length);
    }

    /// <summary>
    /// Verifies that empty beautiful GUID string throws an exception.
    /// </summary>
    [Fact]
    public void BeautifulGuidToString_WithEmptyString_ThrowsArgumentException()
    {
        // Arrange
        string emptyBeautifulGuid = string.Empty;

        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            emptyBeautifulGuid.BeautifulGuidToString());
        Assert.Contains("Invalid beautiful GUID format", exception.Message);
    }

    /// <summary>
    /// Verifies that beautiful GUID with null string throws appropriate exception.
    /// </summary>
    [Fact]
    public void BeautifulGuidToString_WithNullString_ThrowsNullReferenceException()
    {
        // Arrange
        string? nullBeautifulGuid = null;

        // Act & Assert
        _ = Assert.Throws<NullReferenceException>(() =>
            nullBeautifulGuid!.BeautifulGuidToString());
    }

    /// <summary>
    /// Verifies that beautiful GUID with extra spaces is handled correctly.
    /// </summary>
    [Fact]
    public void BeautifulGuidToString_WithExtraSpaces_ThrowsArgumentException()
    {
        // Arrange
        string beautifulGuidWithSpaces = "AQAA QAAA-AQAAQAAA-AQAAQAAA-AQAAQAAA";

        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            beautifulGuidWithSpaces.BeautifulGuidToString());
        Assert.Contains("Invalid beautiful GUID format", exception.Message);
    }
}
