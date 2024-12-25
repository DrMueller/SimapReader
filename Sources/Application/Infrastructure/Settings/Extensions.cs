namespace Mmu.SimapReader.Infrastructure.Settings
{
    internal static class Extensions
    {
        public static IEnumerable<string> CustomChunk(string value, int chunkSize)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (chunkSize <= 0)
            {
                throw new ArgumentOutOfRangeException("chunkSize", "Chunk size should be positive");
            }

            return Enumerable
                .Range(0, value.Length / chunkSize + (value.Length % chunkSize == 0 ? 0 : 1))
                .Select(index => (index + 1) * chunkSize < value.Length
                    ? value.Substring(index * chunkSize, chunkSize)
                    : value.Substring(index * chunkSize));
        }
    }
}