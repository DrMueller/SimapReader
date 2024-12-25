using System.IO;
using System.IO.Compression;
using JetBrains.Annotations;
using Mmu.SimapReader.Areas.Models;
using Mmu.SimapReader.Infrastructure.Informations;

namespace Mmu.SimapReader.Areas.Services.Implementation
{
    [UsedImplicitly]
    public class UnzipService : IUnzipService
    {
        public Task<UnzipResult> UnzipAsync(InformationEntries infoEntries, string zipFilePath)
        {
            var tempPath = Path.GetTempPath();
            var subDir = "SIMAPREader_Import_" + Guid.NewGuid();
            var fullDir = Path.Combine(tempPath, subDir);

            infoEntries.Add($"Ungezippt zu {fullDir}..");

            if (!Directory.Exists(fullDir))
            {
                Directory.CreateDirectory(fullDir);
            }

            ExtractToDirectoryRecursive(zipFilePath, fullDir);

            var result = new UnzipResult(true, fullDir);

            return Task.FromResult(result);
        }

        private static void ExtractToDirectoryRecursive(string zipFilePath, string destinationDir, int currentDepth = 0, int maxDepth = 4)
        {
            if (currentDepth > maxDepth)
            {
                return;
            }

            ZipFile.ExtractToDirectory(zipFilePath, destinationDir);

            foreach (var file in Directory.GetFiles(destinationDir, "*.zip", SearchOption.AllDirectories))
            {
                var subDir = Path.Combine(destinationDir, Path.GetFileNameWithoutExtension(file));
                if (!Directory.Exists(subDir))
                {
                    Directory.CreateDirectory(subDir);
                }

                ExtractToDirectoryRecursive(file, subDir, currentDepth + 1);
                File.Delete(file);
            }
        }
    }
}