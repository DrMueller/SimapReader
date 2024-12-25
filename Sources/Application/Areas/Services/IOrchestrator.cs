using Mmu.SimapReader.Infrastructure.Informations;

namespace Mmu.SimapReader.Areas.Services
{
    public interface IOrchestrator
    {
        Task ProcessAsync(
            InformationEntries infoEntries,
            string zipFilePath);
    }
}