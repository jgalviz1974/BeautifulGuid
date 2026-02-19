namespace Gasolutions.Core.BeautifulGuid
{
    /// <summary>
    /// Class with extension methods for converting 'beautiful' base32-encoded GUID strings into standard UUID hex strings.
    /// </summary>
    public static class ToStringExtensions
    {
        private static readonly char[] CrockfordBase32Chars = "0123456789ABCDEFGHJKMNPQRSTVWXYZ".ToCharArray();

        /// <summary>
        /// Converts a 'beautiful' base32-encoded GUID string into a standard UUID hex string.
        /// Expected input format: four base32 parts separated by '-', for example: "PART1-PART2-PART3-PART4".
        /// </summary>
        /// <param name="key">The composite key to convert.</param>
        /// <returns>UUID formatted string in hexadecimal groups.</returns>
        public static string BeautifulGuidToString(this string key)
        {
            if (key.Contains(" "))
            {
                throw new ArgumentException("Invalid beautiful GUID format. Spaces are not allowed.", nameof(key));
            }

            string[] parts = key.Split('-');

            if (parts.Length < 4)
            {
                throw new ArgumentException("Invalid beautiful GUID format. Expected four '-' separated parts.", nameof(key));
            }

            // Seleccionar las 4 partes de la cadena de la clave
            string s1 = parts[0];
            string s2 = parts[1];
            string s3 = parts[2];
            string s4 = parts[3];

            // Decodificar cada parte de la cadena en la parte original del UUID
            string n1 = DecodeBase32(s1);
            string n2 = DecodeBase32(s2);
            string n3 = DecodeBase32(s3);
            string n4 = DecodeBase32(s4);

            // Seleccionar las 4 partes de las partes decodificadas
            string n2a = n2.Substring(0, 4);
            string n2b = n2.Substring(4, 4);
            string n3a = n3.Substring(0, 4);
            string n3b = n3.Substring(4, 4);

            // Construir y devolver la cadena UUID
            return $"{n1}-{n2a}-{n2b}-{n3a}-{n3b}{n4}";
        }

        private static string DecodeBase32(string input)
        {
            ulong value = 0;
            foreach (char c in input)
            {
                int index = Array.IndexOf(CrockfordBase32Chars, c);
                if (index < 0)
                {
                    throw new ArgumentException($"Invalid base32 character '{c}' in input.", nameof(input));
                }

                value = (value << 5) | (uint)index;
            }

            return value.ToString("X").PadLeft(8, '0');
        }
    }
}
