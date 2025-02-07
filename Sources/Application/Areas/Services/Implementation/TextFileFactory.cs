using System.IO;
using JetBrains.Annotations;
using Mmu.SimapReader.Infrastructure.Informations;

namespace Mmu.SimapReader.Areas.Services.Implementation
{
    [UsedImplicitly]
    public class TextFileFactory(
        IFileTransformer transformer) : ITextFileFactory
    {
        public async Task CreateAllAsync(InformationEntries infoEntries)
        {
            const string inputPath = "C:\\Users\\matthias.mueller\\Desktop\\CAS\\Text";

            var outputPath = $"{inputPath}\\Output";

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            var transformations = await transformer.TransformFilesAsync(
                infoEntries,
                inputPath);

            foreach (var transformation in transformations)
            {
                var fileName = Path.ChangeExtension(
                    Path.GetFileName(transformation.FilePath),
                    ".txt");

                var path = Path.Combine(
                    outputPath,
                    fileName);

                await File.WriteAllTextAsync(path, transformation.Content);
            }
        }
    }
}