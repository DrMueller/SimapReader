using System.IO;
using Azure;
using Azure.AI.DocumentIntelligence;
using JetBrains.Annotations;
using Mmu.SimapReader.Areas.Models;
using Mmu.SimapReader.Infrastructure.Informations;
using Mmu.SimapReader.Infrastructure.Settings.Provisioning.Services;

namespace Mmu.SimapReader.Areas.Services.Implementation
{
    [UsedImplicitly]
    public class WordTransformer(ISettingsProvider settingsProvider) : IWordTransformer
    {
        public async Task<IReadOnlyCollection<WordTransformations>> TransformWordsAsyncs(
            InformationEntries infoEntries,
            string wordsFilePath)
        {
            var client = new DocumentIntelligenceClient(
                new Uri(settingsProvider.AppSettings.DocumentIntelligenceEndpoint),
                new AzureKeyCredential(settingsProvider.AppSettings.DocumentIntelligenceApiKey));

            var wordFiles = Directory.GetFiles(wordsFilePath, "*.docx", SearchOption.AllDirectories);

            var result = new List<WordTransformations>();

            foreach (var wordFilePath in wordFiles)
            {
                await using var stream = new FileStream(wordFilePath, FileMode.Open, FileAccess.Read);

                infoEntries.Add($"Analysiere {Path.GetFileName(wordFilePath)}..");

                var operation = await client.AnalyzeDocumentAsync(
                    WaitUntil.Completed,
                    "prebuilt-read",
                    await BinaryData.FromStreamAsync(stream));

                await operation.WaitForCompletionAsync();

                result.Add(new WordTransformations(
                    wordFilePath,
                    operation.Value.Content));
            }

            return result;
        }
    }
}