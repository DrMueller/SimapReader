using Azure;
using Azure.AI.TextAnalytics;
using JetBrains.Annotations;
using Mmu.SimapReader.Areas.Models;
using Mmu.SimapReader.Infrastructure.Informations;
using Mmu.SimapReader.Infrastructure.Settings;
using Mmu.SimapReader.Infrastructure.Settings.Provisioning.Services;

namespace Mmu.SimapReader.Areas.Services.Implementation
{
    [UsedImplicitly]
    public class EntityRecognizer(ISettingsProvider settingsProvider) : IEntityRecognizer
    {
        public async Task<EntityRecognitionResult> RecognizeAsync(
            InformationEntries infoEntries,
            IReadOnlyCollection<WordTransformations> transformations)
        {
            var client = new TextAnalyticsClient(
                new Uri(settingsProvider.AppSettings.TextAnalyticsEndpoint),
                new AzureKeyCredential(settingsProvider.AppSettings.TextAnalyticsApiKey));

            var resultEntries = new List<EntityRecognitionResultEntry>();

            foreach (var transformation in transformations)
            {
                var groupedText = Extensions
                    .CustomChunk(transformation.Content, 5000)
                    .ToList();

                var chunkCount = groupedText.Count();
                var cnt = 1;
                foreach (var grp in groupedText)
                {
                    infoEntries.Add($"Analysiere Chunk {cnt++}/{chunkCount} der Datei {transformation.FileName}..");

                    var response = await client.RecognizeEntitiesAsync(grp, "de");

                    foreach (var entry in response.Value)
                    {
                        resultEntries.Add(new EntityRecognitionResultEntry(
                            entry.Category.ToString(),
                            entry.SubCategory,
                            entry.Text,
                            entry.ConfidenceScore,
                            transformation.FilePath));
                    }
                }
            }

            return new EntityRecognitionResult(resultEntries);
        }
    }
}