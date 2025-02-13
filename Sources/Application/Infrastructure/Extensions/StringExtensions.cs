namespace Mmu.SimapReader.Infrastructure.Extensions
{
    internal static class StringExtensions
    {
        internal static string Clean(this string str)
        {
            return str
                .Replace("\"", string.Empty)
                .Replace(Environment.NewLine, " ")
                .Replace("\n", " ")
                .Replace(";", " ")
                .Trim();
        }
    }
}