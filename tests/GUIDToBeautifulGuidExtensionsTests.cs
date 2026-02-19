namespace Gasolutions.Core.BeautifulGuid.Tests;

/// <summary>
/// Test suite for GUIDToBeautifulGuidExtensions.
/// Tests the conversion of GUIDs to beautiful readable string format.
/// </summary>
public class GUIDToBeautifulGuidExtensionsTests
{
    /// <summary>
    /// Verifies that a valid GUID is converted to a beautiful GUID string.
    /// </summary>
    [Fact]
    public void ToBeautifulGuid_WithValidGuid_ReturnsBeautifulString()
    {
        // Arrange
        var guid = new Guid("550e8400-e29b-41d4-a716-446655440000");

        // Act
        string result = guid.ToBeautifulGuid();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains("-", result);
    }

    /// <summary>
    /// Verifies that a beautiful GUID has the expected format (4 parts separated by hyphens).
    /// </summary>
    [Fact]
    public void ToBeautifulGuid_ReturnsCorrectFormat()
    {
        // Arrange
        var guid = new Guid("550e8400-e29b-41d4-a716-446655440000");

        // Act
        string result = guid.ToBeautifulGuid();
        var parts = result.Split('-');

        // Assert
        Assert.Equal(4, parts.Length);
        Assert.True(parts.All(p => !string.IsNullOrEmpty(p)));
    }

    /// <summary>
    /// Verifies that different GUIDs produce different beautiful GUID strings.
    /// </summary>
    [Fact]
    public void ToBeautifulGuid_WithDifferentGuids_ReturnsDifferentStrings()
    {
        // Arrange
        var guid1 = new Guid("550e8400-e29b-41d4-a716-446655440000");
        var guid2 = new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8");

        // Act
        string result1 = guid1.ToBeautifulGuid();
        string result2 = guid2.ToBeautifulGuid();

        // Assert
        Assert.NotEqual(result1, result2);
    }

    /// <summary>
    /// Verifies that the same GUID always produces the same beautiful GUID string (consistency).
    /// </summary>
    [Fact]
    public void ToBeautifulGuid_WithSameGuid_ReturnsSameString()
    {
        // Arrange
        var guid = new Guid("550e8400-e29b-41d4-a716-446655440000");

        // Act
        string result1 = guid.ToBeautifulGuid();
        string result2 = guid.ToBeautifulGuid();

        // Assert
        Assert.Equal(result1, result2);
    }

    /// <summary>
    /// Verifies that multiple GUIDs can be converted successfully.
    /// </summary>
    [Theory]
    [InlineData("550e8400-e29b-41d4-a716-446655440000")]
    [InlineData("6ba7b810-9dad-11d1-80b4-00c04fd430c8")]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    [InlineData("ffffffff-ffff-ffff-ffff-ffffffffffff")]
    public void ToBeautifulGuid_WithVariousGuids_ReturnsValidBeautifulGuids(string guidString)
    {
        // Arrange
        var guid = new Guid(guidString);

        // Act
        string result = guid.ToBeautifulGuid();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        var parts = result.Split('-');
        Assert.Equal(4, parts.Length);
    }

    /// <summary>
    /// Verifies that the beautiful GUID string contains only valid Crockford base32 characters.
    /// </summary>
    [Fact]
    public void ToBeautifulGuid_ReturnsOnlyValidBase32Characters()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var validChars = "0123456789ABCDEFGHJKMNPQRSTVWXYZ-";

        // Act
        string result = guid.ToBeautifulGuid();

        // Assert
        Assert.True(result.All(c => validChars.Contains(c)), 
            $"Beautiful GUID contains invalid characters: {result}");
    }

    /// <summary>
    /// Verifies that empty parts are not allowed in beautiful GUID.
    /// </summary>
    [Fact]
    public void ToBeautifulGuid_DoesNotReturnEmptyParts()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        string result = guid.ToBeautifulGuid();
        var parts = result.Split('-');

        // Assert
        Assert.True(parts.All(p => !string.IsNullOrEmpty(p)), 
            "Beautiful GUID should not contain empty parts");
    }

    /// <summary>
    /// Verifies that special GUIDs like all zeros and all ones are handled correctly.
    /// </summary>
    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    [InlineData("ffffffff-ffff-ffff-ffff-ffffffffffff")]
    public void ToBeautifulGuid_WithSpecialGuids_ReturnsValidResult(string guidString)
    {
        // Arrange
        var guid = new Guid(guidString);

        // Act
        string result = guid.ToBeautifulGuid();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    /// <summary>
    /// Verifies that the beautiful GUID string length is reasonable (not excessively long).
    /// </summary>
    [Fact]
    public void ToBeautifulGuid_ReturnsReasonableLength()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        string result = guid.ToBeautifulGuid();

        // Assert
        // Expected format: XXXXXXXX-XXXXXXXX-XXXXXXXX-XXXXXXXX (approx 30+ chars)
        Assert.True(result.Length > 20 && result.Length < 60, 
            $"Beautiful GUID length should be between 20 and 60 characters, got {result.Length}");
    }

    /// <summary>
    /// Verifies that a GUID generated from random values produces a valid beautiful GUID.
    /// </summary>
    [Fact]
    public void ToBeautifulGuid_WithRandomGuids_AlwaysReturnsValidResult()
    {
        // Arrange
        int iterations = 100;

        // Act & Assert
        for (int i = 0; i < iterations; i++)
        {
            var randomGuid = Guid.NewGuid();
            string result = randomGuid.ToBeautifulGuid();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            var parts = result.Split('-');
            Assert.Equal(4, parts.Length);
        }
    }
}
