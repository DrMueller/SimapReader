using Mmu.SimapReader.Infrastructure.Informations;

namespace Mmu.SimapReader.Areas.Services
{
    public interface ITextFileFactory
    {
        Task CreateAllAsync(InformationEntries infoEntries);
    }
}