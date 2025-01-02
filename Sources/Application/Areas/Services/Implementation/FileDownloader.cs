using System.Diagnostics;
using System.IO;
using System.Text;
using JetBrains.Annotations;
using Mmu.SimapReader.Areas.ViewModels;
using Mmu.SimapReader.Infrastructure.Extensions;
using Mmu.SimapReader.Infrastructure.Informations;

namespace Mmu.SimapReader.Areas.Services.Implementation
{
    [UsedImplicitly]
    public class FileDownloader : IFileDownloader
    {
        public async Task DownloadLogsAsync(InformationEntries infoEntries)
        {
            var sb = new StringBuilder();

            infoEntries.Collection
                .Select(f => f.Message)
                .ToList()
                .ForEach(f => sb.AppendLine(f));

            await WriteAndOpenAsync(sb.ToString());
        }

        public async Task DownloadResultsAsync(IReadOnlyCollection<EntityRecognitionResultEntryViewModel> results)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Word;Category;Average Confidence;Amount");

            foreach (var entry in results)
            {
                sb.AppendLine(
                    $"{entry.RecognizedWord.Clean()}" +
                    $";{entry.Category.Clean()}" +
                    $";{entry.AverageConfidence}" +
                    $";{entry.FoundAmount}");
            }

            await WriteAndOpenAsync(sb.ToString());
        }

        private static async Task WriteAndOpenAsync(string text)
        {
            var tempFileName = Path.GetTempFileName();
            await File.WriteAllTextAsync(tempFileName, text);
            Process.Start("notepad.exe", tempFileName);
        }
    }
}