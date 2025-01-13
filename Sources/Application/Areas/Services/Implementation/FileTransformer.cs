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
    public class FileTransformer(ISettingsProvider settingsProvider) : IFileTransformer
    {
        public async Task<IReadOnlyCollection<FileTransformations>> TransformFilesAsync(
            InformationEntries infoEntries,
            string documentsFilePath)
        {
            var client = new DocumentIntelligenceClient(
                new Uri(settingsProvider.AppSettings.DocumentIntelligenceEndpoint),
                new AzureKeyCredential(settingsProvider.AppSettings.DocumentIntelligenceApiKey));

            var wordFiles = Directory.GetFiles(documentsFilePath, "*.*", SearchOption.AllDirectories);
            var result = new List<FileTransformations>();

            foreach (var wordFilePath in wordFiles)
            {
                await using var stream = new FileStream(wordFilePath, FileMode.Open, FileAccess.Read);

                infoEntries.Add($"Transforming {Path.GetFileName(wordFilePath)}..");

                var operation = await client.AnalyzeDocumentAsync(
                    WaitUntil.Completed,
                    "prebuilt-read",
                    await BinaryData.FromStreamAsync(stream));

                await operation.WaitForCompletionAsync();

                result.Add(new FileTransformations(
                    wordFilePath,
                    operation.Value.Content));
            }

            return result;
        }
    }
}