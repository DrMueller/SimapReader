namespace Mmu.SimapReader.Areas.Models
{
    public record EntityRecognitionResultEntry(
        string Category,
        string SubCategory,
        string RecognizedWord,
        double Confidence,
        string WordFilePath);
}