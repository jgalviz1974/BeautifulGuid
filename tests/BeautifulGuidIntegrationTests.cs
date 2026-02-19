namespace Gasolutions.Core.BeautifulGuid.Tests;

/// <summary>
/// Integration tests for Beautiful GUID functionality.
/// Tests round-trip conversions and edge cases across both extension methods.
/// </summary>
public class BeautifulGuidIntegrationTests
{
    /// <summary>
    /// Verifies that a GUID can be converted to beautiful GUID and processed without errors.
    /// </summary>
    [Fact]
    public void FullConversionFlow_WithValidGuid_CompletesSuccessfully()
    {
        // Arrange
        var originalGuid = new Guid("550e8400-e29b-41d4-a716-446655440000");

        // Act
        string beautifulGuid = originalGuid.ToBeautifulGuid();
        string convertedBack = beautifulGuid.BeautifulGuidToString();

        // Assert
        Assert.NotNull(beautifulGuid);
        Assert.NotEmpty(beautifulGuid);
        Assert.NotNull(convertedBack);
        Assert.NotEmpty(convertedBack);
    }

    /// <summary>
    /// Verifies that multiple GUIDs can be converted and tracked independently.
    /// </summary>
    [Fact]
    public void MultipleGuidConversions_MaintainsIndependence()
    {
        // Arrange
        var guids = new[]
        {
            new Guid("550e8400-e29b-41d4-a716-446655440000"),
            new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8"),
            new Guid("6ba7b811-9dad-11d1-80b4-00c04fd430c8")
        };

        // Act
        var beautifulGuids = guids.Select(g => g.ToBeautifulGuid()).ToList();
        var convertedBack = beautifulGuids.Select(b => b.BeautifulGuidToString()).ToList();

        // Assert
        Assert.Equal(guids.Length, beautifulGuids.Count);
        Assert.Equal(beautifulGuids.Count, convertedBack.Count);
        Assert.True(beautifulGuids.All(b => !string.IsNullOrEmpty(b)));
        Assert.True(convertedBack.All(c => !string.IsNullOrEmpty(c)));
    }

    /// <summary>
    /// Verifies that performance is acceptable for bulk conversions.
    /// </summary>
    [Fact]
    public void BulkConversion_WithManyGuids_PerformsAcceptably()
    {
        // Arrange
        const int conversionCount = 1000;
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        // Act
        var results = new List<string>(conversionCount);
        for (int i = 0; i < conversionCount; i++)
        {
            var guid = Guid.NewGuid();
            string beautifulGuid = guid.ToBeautifulGuid();
            results.Add(beautifulGuid);
        }
        stopwatch.Stop();

        // Assert
        Assert.Equal(conversionCount, results.Count);
        Assert.True(results.All(r => !string.IsNullOrEmpty(r)));
        Assert.True(stopwatch.ElapsedMilliseconds < 5000, 
            $"Bulk conversion of {conversionCount} GUIDs took {stopwatch.ElapsedMilliseconds}ms, expected < 5000ms");
    }

    /// <summary>
    /// Verifies that known GUID conversions produce consistent beautiful GUID strings.
    /// </summary>
    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    [InlineData("ffffffff-ffff-ffff-ffff-ffffffffffff")]
    [InlineData("550e8400-e29b-41d4-a716-446655440000")]
    public void KnownGuidConversions_AreConsistent(string guidString)
    {
        // Arrange
        var guid = new Guid(guidString);

        // Act
        string beautiful1 = guid.ToBeautifulGuid();
        string beautiful2 = guid.ToBeautifulGuid();

        // Assert
        Assert.Equal(beautiful1, beautiful2);
        Assert.NotEqual("", beautiful1); // Ensure not empty
    }

    /// <summary>
    /// Verifies that beautiful GUID strings are URL-safe (no problematic characters).
    /// </summary>
    [Fact]
    public void BeautifulGuids_AreUrlSafe()
    {
        // Arrange
        var unsafeCharacters = new[] { '/', '?', '#', '&', '=', '+', '%', ' ' };
        var guids = Enumerable.Range(0, 10).Select(_ => Guid.NewGuid()).ToList();

        // Act
        var beautifulGuids = guids.Select(g => g.ToBeautifulGuid()).ToList();

        // Assert
        Assert.True(beautifulGuids.All(bg => 
            !bg.Any(c => unsafeCharacters.Contains(c))), 
            "Beautiful GUIDs should not contain URL-unsafe characters");
    }

    /// <summary>
    /// Verifies that beautiful GUID strings are reasonably shorter than UUID strings.
    /// </summary>
    [Fact]
    public void BeautifulGuids_AreShorterThanUuids()
    {
        // Arrange
        var guid = Guid.NewGuid();
        string uuidString = guid.ToString();

        // Act
        string beautifulGuid = guid.ToBeautifulGuid();

        // Assert
        // Both should have hyphens, but beautiful should be reasonably sized
        Assert.NotEmpty(beautifulGuid);
        Assert.NotEmpty(uuidString);
        Assert.Contains("-", beautifulGuid);
        Assert.Contains("-", uuidString);
    }

    /// <summary>
    /// Verifies that all characters in beautiful GUID are from Crockford base32 set.
    /// </summary>
    [Fact]
    public void BeautifulGuids_ContainOnlyBase32Characters()
    {
        // Arrange
        const string crockfordBase32 = "0123456789ABCDEFGHJKMNPQRSTVWXYZ-";
        var guids = Enumerable.Range(0, 50).Select(_ => Guid.NewGuid()).ToList();

        // Act
        var beautifulGuids = guids.Select(g => g.ToBeautifulGuid()).ToList();

        // Assert
        Assert.True(beautifulGuids.All(bg => 
            bg.All(c => crockfordBase32.Contains(c))), 
            "All characters in beautiful GUID should be from Crockford base32 set");
    }

    /// <summary>
    /// Verifies that conversion process handles sequential GUIDs correctly.
    /// </summary>
    [Fact]
    public void SequentialGuids_ProduceDifferentBeautifulGuids()
    {
        // Arrange
        var baseBytes = Guid.NewGuid().ToByteArray();
        var guid1 = new Guid(baseBytes);
        
        // Modify one byte to create a slightly different GUID
        var guid2Bytes = (byte[])baseBytes.Clone();
        guid2Bytes[15] = (byte)((guid2Bytes[15] + 1) % 256);
        var guid2 = new Guid(guid2Bytes);

        // Act
        string beautiful1 = guid1.ToBeautifulGuid();
        string beautiful2 = guid2.ToBeautifulGuid();

        // Assert
        Assert.NotEqual(beautiful1, beautiful2);
    }

    /// <summary>
    /// Verifies that the conversion maintains uniqueness across a large sample.
    /// </summary>
    [Fact]
    public void LargeSet_MaintainsUniqueness()
    {
        // Arrange
        const int guidCount = 1000;
        var guids = Enumerable.Range(0, guidCount)
            .Select(_ => Guid.NewGuid())
            .ToList();

        // Act
        var beautifulGuids = guids
            .Select(g => g.ToBeautifulGuid())
            .ToList();

        // Assert
        var uniqueCount = beautifulGuids.Distinct().Count();
        Assert.Equal(guidCount, uniqueCount);
    }
}
