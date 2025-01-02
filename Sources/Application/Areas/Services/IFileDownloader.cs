using Mmu.SimapReader.Areas.ViewModels;
using Mmu.SimapReader.Infrastructure.Informations;

namespace Mmu.SimapReader.Areas.Services
{
    public interface IFileDownloader
    {
        Task DownloadLogsAsync(InformationEntries infoEntries);
        Task DownloadResultsAsync(IReadOnlyCollection<EntityRecognitionResultEntryViewModel> results);
    }
}