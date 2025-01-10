using System.IO;
using JetBrains.Annotations;
using Mmu.SimapReader.Areas.ViewModels;
using Mmu.SimapReader.Infrastructure.Informations;

namespace Mmu.SimapReader.Areas.Services.Implementation
{
    [UsedImplicitly]
    public class Orchestrator(
        IUnzipService unzipper,
        IFileTransformer fileTransformer,
        IEntityRecognizer recognizer,
        IResultAdapter resultAdapter)
        : IOrchestrator
    {
        public async Task<IReadOnlyCollection<EntityRecognitionResultEntryViewModel>> ProcessAsync(
            InformationEntries infoEntries, 
            string zipFilePath)
        {
            string? unzipFilePath = null;
            try
            {
                var unzipResult = await unzipper.UnzipAsync(infoEntries, zipFilePath);
                
                if (!unzipResult.WasSuccess)
                {
                    infoEntries.Add($"Unzipping to '{zipFilePath}' failed.");
                }
                
                unzipFilePath = unzipResult.UnzipFilePath;
                var transformedWords = await fileTransformer.TransformFilesAsync(infoEntries, unzipResult.UnzipFilePath);
                var result = await recognizer.RecognizeAsync(infoEntries, transformedWords);
                return resultAdapter.Adapt(result);
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

            return new List<EntityRecognitionResultEntryViewModel>();
        }
    }
}