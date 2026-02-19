# Beautiful GUID Tests

Comprehensive test suite for the Beautiful GUID extension methods library.

## Overview

This project contains unit tests and integration tests for the `Gasolutions.Core.BeautifulGuid` library, which provides extension methods to convert GUIDs into readable and user-friendly string formats.

## Test Suites

### 1. GUIDToBeautifulGuidExtensionsTests
Tests the conversion of standard GUIDs to beautiful, human-readable strings.

**Key Test Cases:**
- Valid GUID conversion
- Format validation (4 parts separated by hyphens)
- Uniqueness across different GUIDs
- Consistency (same input = same output)
- Crockford base32 character validation
- Special GUID handling (all zeros, all ones)
- Performance with random GUIDs

### 2. ToStringExtensionsTests
Tests the reverse conversion from beautiful GUID strings back to standard UUID format.

**Key Test Cases:**
- Valid beautiful GUID to UUID conversion
- UUID format validation (8-4-4-4-12)
- Invalid format detection
- Hexadecimal validation
- Round-trip verification
- Error handling for malformed input
- Null and empty string handling

### 3. BeautifulGuidIntegrationTests
End-to-end integration tests covering complete workflows and edge cases.

**Key Test Cases:**
- Full conversion flow validation
- Multiple GUID conversions independence
- Bulk conversion performance
- URL-safety of beautiful GUIDs
- Crockford base32 character set compliance
- Sequential GUID handling
- Uniqueness in large datasets

## Running the Tests

```bash
# Run all tests
dotnet test

# Run specific test class
dotnet test --filter "ClassName=GUIDToBeautifulGuidExtensionsTests"

# Run with verbose output
dotnet test -v d

# Run with code coverage
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover
```

## Test Coverage Goals

- **Overall Coverage:** 95%+
- **Line Coverage:** 95%+
- **Branch Coverage:** 90%+

## Test Statistics

- **Total Test Cases:** 35+
- **Theory Tests:** 10+
- **Fact Tests:** 25+

## Dependencies

- xunit (v2.6.6)
- Microsoft.NET.Test.Sdk (v17.8.2)
- coverlet.collector (v6.0.0)

## Key Features Tested

? GUID to Beautiful GUID conversion
? Beautiful GUID to UUID reverse conversion
? Format validation
? Character set compliance
? Round-trip conversion integrity
? Performance benchmarking
? Edge case handling
? Error conditions

## Notes

- All tests follow AAA (Arrange-Act-Assert) pattern
- Tests are isolated and can run in any order
- No external dependencies or mocking required
- All test methods include XML documentation
