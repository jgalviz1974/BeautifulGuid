# Changelog - Gasolutions.Core.BeautifulGuid

All notable changes to this project will be documented in this file.

## [1.0.8]
### Added	
- Add CHANGELOG.md file to document changes and updates

## [1.0.7]

### Added
- Comprehensive XML documentation for all extension methods
- Complete test suites with 35+ test cases
- Support for round-trip conversions (GUID → Beautiful GUID → UUID)
- URL-safe GUID formatting
- Performance benchmarking tests

### Changed
- Updated documentation to English for international audience
- Enhanced Crockford base32 encoding/decoding
- Improved type safety with better null handling

### Fixed
- GUID conversion accuracy
- Null value handling in round-trip conversions
- Character set validation improvements

### Documentation
- Added comprehensive XML documentation for all extension methods
- Created test suite documentation with 35+ test cases
- Documented beautiful GUID format specifications

---

## [1.0.6]

### Initial Release
- ToBeautifulGuid() extension method for GUID to readable string conversion
- BeautifulGuidToString() extension method for reverse conversion
- Crockford base32 encoding support
- Support for multiple readable GUID formats
- URL-safe formatting options
