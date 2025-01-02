using Mmu.SimapReader.Areas.ViewModels;
using Mmu.SimapReader.Infrastructure.Informations;

namespace Mmu.SimapReader.Areas.Services
{
    public interface IOrchestrator
    {
        Task<IReadOnlyCollection<EntityRecognitionResultEntryViewModel>> ProcessAsync(
            InformationEntries infoEntries,
            string zipFilePath);
    }
}