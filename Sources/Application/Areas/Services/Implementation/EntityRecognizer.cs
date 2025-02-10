using Azure;
using Azure.AI.TextAnalytics;
using JetBrains.Annotations;
using Mmu.SimapReader.Areas.Models;
using Mmu.SimapReader.Infrastructure.Extensions;
using Mmu.SimapReader.Infrastructure.Informations;
using Mmu.SimapReader.Infrastructure.Settings.Provisioning.Services;

namespace Mmu.SimapReader.Areas.Services.Implementation
{
    [UsedImplicitly]
    public class EntityRecognizer(ISettingsProvider settingsProvider) : IEntityRecognizer
    {
        public async Task<EntityRecognitionResult> RecognizeAsync(
            InformationEntries infoEntries,
            IReadOnlyCollection<FileTransformations> transformations)
        {
            infoEntries.Add("Starting recognizing..");

            var client = new TextAnalyticsClient(
                new Uri(settingsProvider.AppSettings.TextAnalyticsEndpoint),
                new AzureKeyCredential(settingsProvider.AppSettings.TextAnalyticsApiKey));

            const string projectName = "CAS3";
            const string deploymentName = "SIMAPDeployment";

            var actions = new TextAnalyticsActions
            {
                RecognizeCustomEntitiesActions = new List<RecognizeCustomEntitiesAction>
                {
                    new(projectName, deploymentName)
                }
            };

            var documentInputs = new List<TextDocumentInput>();
            var i = 0;
            foreach (var transformation in transformations)
            {
                var chunkedContent = SplitString(transformation.Content, 5000);
                foreach (var cc in chunkedContent)
                {
                    i++;
                    documentInputs.Add(new TextDocumentInput(i.ToString(), cc));
                }
            }

            var chunkedInputs = documentInputs.Chunk(5);
            var resultEntries = new List<EntityRecognitionResultEntry>();

            foreach (var inputs in chunkedInputs)
            {
                var operation = await client.StartAnalyzeActionsAsync(inputs, actions);
                await operation.WaitForCompletionAsync();

                await foreach (var documentsInPage in operation.Value)
                {
                    IReadOnlyCollection<RecognizeCustomEntitiesActionResult> customEntitiesActionResults = documentsInPage.RecognizeCustomEntitiesResults;
                    foreach (var customEntitiesActionResult in customEntitiesActionResults)
                    {
                        foreach (var documentResults in customEntitiesActionResult.DocumentsResults)
                        {
                            foreach (var entity in documentResults.Entities)
                            {
                                resultEntries.Add(new EntityRecognitionResultEntry(
                                    entity.Category.ToString().Clean(),
                                    entity.SubCategory?.Clean() ?? string.Empty,
                                    entity.Text.Clean(),
                                    entity.ConfidenceScore));
                            }
                        }
                    }
                }
            }

            infoEntries.Add("Recognizing done..");

            return new EntityRecognitionResult(resultEntries);
        }

        private static List<string> SplitString(string str, int partSize)
        {
            var parts = new List<string>();
            for (var i = 0; i < str.Length; i += partSize)
            {
                parts.Add(str.Substring(i, Math.Min(partSize, str.Length - i)));
            }

            return parts;
        }
    }
}