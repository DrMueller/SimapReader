using JetBrains.Annotations;
using Mmu.SimapReader.Areas.Models;
using Mmu.SimapReader.Areas.ViewModels;

namespace Mmu.SimapReader.Areas.Services.Implementation
{
    [UsedImplicitly]
    public class ResultAdapter : IResultAdapter
    {
        public IReadOnlyCollection<EntityRecognitionResultEntryViewModel> Adapt(EntityRecognitionResult recognitionResult)
        {
            var grps = recognitionResult.Entries.GroupBy(f => new { f.RecognizedWord, f.Category });
            var result = new List<EntityRecognitionResultEntryViewModel>();

            foreach (var entry in grps)
            {
                var avgConfidence = entry.Sum(f => f.Confidence) / entry.Count();
                avgConfidence = Math.Round(avgConfidence, 2);

                result.Add(new EntityRecognitionResultEntryViewModel
                {
                    RecognizedWord = entry.Key.RecognizedWord,
                    AverageConfidence = avgConfidence,
                    Category = entry.Key.Category,
                    FoundAmount = entry.Count()
                });
            }

            result = result
                .OrderByDescending(f => f.AverageConfidence)
                .ThenByDescending(f => f.FoundAmount)
                .ThenBy(f => f.RecognizedWord)
                .ThenBy(f => f.Category)
                .ToList();

            return result;
        }
    }
}