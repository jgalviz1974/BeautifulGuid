using System.Globalization;

namespace Gasolutions.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for converting GUIDs to a more human-friendly string format.
    /// </summary>
    public static class GUIDToBeautifulGuidExtensions
    {
        private const int UUIDLength = 36;
        private static readonly char[] CrockfordBase32Chars = "0123456789ABCDEFGHJKMNPQRSTVWXYZ".ToCharArray();

        /// <summary>
        /// Converts a GUID to a beautiful GUID string.
        /// </summary>
        /// <param name="guid">The GUID to convert.</param>
        /// <returns>A string representing the beautiful GUID.</returns>
        /// <exception cref="ArgumentException">Thrown when the GUID is not in a valid format.</exception>
        public static string ToBeautifulGuid(this Guid guid)
        {
            string uuid = guid.ToString();

            // Basic length validation
            if (uuid.Length != UUIDLength)
            {
                throw new ArgumentException($"Invalid UUID length: expected {UUIDLength} characters, got {uuid.Length}");
            }

            // Select the 5 parts of the UUID string
            string s1 = uuid.Substring(0, 8);
            string s2 = uuid.Substring(9, 4);
            string s3 = uuid.Substring(14, 4);
            string s4 = uuid.Substring(19, 4);
            string s5 = uuid.Substring(24, 12);

            // Decode each part of the string to uint64
            ulong n1 = ulong.Parse(s1, NumberStyles.HexNumber);
            ulong n2 = ulong.Parse(s2 + s3, NumberStyles.HexNumber);     // Combine s2 and s3
            ulong n3 = ulong.Parse(s4 + s5.Substring(0, 4), NumberStyles.HexNumber); // Combine s4 and the first 4 characters of s5
            ulong n4 = ulong.Parse(s5.Substring(4), NumberStyles.HexNumber);    // The last 8 characters of s5

            // Encode each uint64 in Crockford base32 format
            string e1 = EncodeBase32(n1);
            string e2 = EncodeBase32(n2);
            string e3 = EncodeBase32(n3);
            string e4 = EncodeBase32(n4);

            // Build and return the key
            return $"{e1}-{e2}-{e3}-{e4}";
        }

        /// <summary>
        /// Encodes a ulong value to a Crockford base32 string.
        /// </summary>
        /// <param name="value">The value to encode.</param>
        /// <returns>A string representing the encoded value in Crockford base32.</returns>
        private static string EncodeBase32(ulong value)
        {
            char[] buffer = new char[13]; // 13 is the maximum size for Crockford base32
            int index = buffer.Length;

            do
            {
                buffer[--index] = CrockfordBase32Chars[value & 31];
                value >>= 5;
            }
            while (value != 0);

            return new string(buffer, index, buffer.Length - index);
        }
    }
}
