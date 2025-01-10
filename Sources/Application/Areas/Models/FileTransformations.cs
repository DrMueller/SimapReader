using System.IO;
using Mmu.Mlh.LanguageExtensions.Areas.Invariance;

namespace Mmu.SimapReader.Areas.Models
{
    public class FileTransformations
    {
        public FileTransformations(
            string filePath,
            string content)
        {
            Guard.StringNotNullOrEmpty(() => filePath);
            FilePath = filePath;
            Content = content;
        }

        public string Content { get; }
        public string FileName => Path.GetFileName(FilePath);
        public string FilePath { get; }
    }
}