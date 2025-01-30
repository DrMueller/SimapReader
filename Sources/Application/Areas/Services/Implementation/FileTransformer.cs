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

            var filePaths = Directory.GetFiles(documentsFilePath, "*.*", SearchOption.AllDirectories);
            var result = new List<FileTransformations>();

            foreach (var filePath in filePaths)
            {
                await using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                infoEntries.Add($"Transforming {Path.GetFileName(filePath)}..");

                var operation = await client.AnalyzeDocumentAsync(
                    WaitUntil.Completed,
                    "prebuilt-read",
                    await BinaryData.FromStreamAsync(stream));

                await operation.WaitForCompletionAsync();

                result.Add(new FileTransformations(
                    filePath,
                    operation.Value.Content));
            }

            return result;
        }
    }
}