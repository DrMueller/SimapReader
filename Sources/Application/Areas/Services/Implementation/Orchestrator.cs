using System.IO;
using JetBrains.Annotations;
using Mmu.SimapReader.Infrastructure.Informations;

namespace Mmu.SimapReader.Areas.Services.Implementation
{
    [UsedImplicitly]
    public class Orchestrator(
        IUnzipService unzipper,
        IWordTransformer wordTransformer,
        IEntityRecognizer recognizer)
        : IOrchestrator
    {
        public async Task ProcessAsync(InformationEntries infoEntries, string zipFilePath)
        {
            string? unzipFilePath = null;
            try
            {
                var unzipResult = await unzipper.UnzipAsync(infoEntries, zipFilePath);
                unzipFilePath = unzipResult.UnzipFilePath;
                var transformedWords = await wordTransformer.TransformWordsAsyncs(infoEntries, unzipResult.UnzipFilePath);
                var recognizedEntities = await recognizer.RecognizeAsync(infoEntries, transformedWords);
                here dispaly recognized entities
            }
            catch (Exception ex)
            {
                infoEntries.Add($"Error: {ex.Message}");
            }
            finally
            {
                if (!string.IsNullOrEmpty(unzipFilePath)
                    && Directory.Exists(unzipFilePath))
                {
                    Directory.Delete(unzipFilePath, true);
                }
            }
        }
    }
}