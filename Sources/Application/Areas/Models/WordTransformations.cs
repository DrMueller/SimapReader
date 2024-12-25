using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.SimapReader.Areas.Models
{
    public class WordTransformations
    {
        public WordTransformations(
            string fileName,
            string content)
        {
            Guard.StringNotNullOrEmpty(() => fileName);
            FileName = fileName;
            Content = content;
        }

        public string Content { get; }
        public string FileName { get; }
    }
}