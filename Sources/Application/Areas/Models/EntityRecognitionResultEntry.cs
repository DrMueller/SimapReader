namespace Mmu.SimapReader.Areas.Models
{
    public record EntityRecognitionResultEntry(
        string Category,
        string RecognizedWord,
        double Confidence);
}