namespace Mmu.SimapReader.Areas.ViewModels
{
    public class EntityRecognitionResultEntryViewModel
    {
        public double AverageConfidence { get; init; }
        public required string Category { get; init; }
        public int FoundAmount { get; init; }
        public required string RecognizedWord { get; init; }
    }
}