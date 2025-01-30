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
            var transformations = await transformer.TransformFilesAsync(
                infoEntries,
                @"C:\Users\matthias.mueller\Dropbox\Arbeit\Schulisches\BFH CAS KI\Transferarbeit\SIMAP\Sprint-BBL\SIMAP -Sprint-BBL-pruned");

            foreach (var transformation in transformations)
            {
                var fileName = Path.ChangeExtension(
                    Path.GetFileName(transformation.FilePath),
                    ".txt");

                var path = Path.Combine(
                    @"C:\Users\matthias.mueller\Dropbox\Arbeit\Schulisches\BFH CAS KI\Transferarbeit\SIMAP\Sprint-BBL\Text",
                    fileName);

                await File.WriteAllTextAsync(path, transformation.Content);
            }
        }
    }
}