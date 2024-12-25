namespace Mmu.SimapReader.Areas.Models
{
    public class EntityRecognitionResult(IReadOnlyCollection<EntityRecognitionResultEntry> entries)
    {
        public IReadOnlyCollection<EntityRecognitionResultEntry> Entries { get; } = entries;
    }
}