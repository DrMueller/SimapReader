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
                new AzureKeyCredential(settingsProvider.AppSettings.TextAnalyticsEndpoint));

            foreach (var transformation in transformations)
            {
                var groupedText = Extensions.CustomChunk(transformation.Content, 5000);
                foreach (var grp in groupedText)
                {
                    var response = await client.RecognizeEntitiesAsync(grp, "de");
                    Console.WriteLine("Named Entities:");
                    var skills = response.Value.Where(f => f.Category == "Skill");

                    foreach (var entity in skills)
                    {
                        Console.WriteLine($"\tText: {entity.Text},\tCategory: {entity.Category},\tSub-Category: {entity.SubCategory}");
                        Console.WriteLine($"\t\tScore: {entity.ConfidenceScore:F2},\tLength: {entity.Length},\tOffset: {entity.Offset}\n");
                    }
                }
            }

            return null;
        }
    }
}