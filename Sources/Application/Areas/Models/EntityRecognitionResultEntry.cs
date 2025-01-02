using JetBrains.Annotations;

namespace Mmu.SimapReader.Areas.Models
{
    [PublicAPI]
    public record EntityRecognitionResultEntry(
        string Category,
        string SubCategory,
        string RecognizedWord,
        double Confidence,
        string WordFilePath);
}