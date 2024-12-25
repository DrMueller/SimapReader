using Mmu.SimapReader.Areas.Models;
using Mmu.SimapReader.Infrastructure.Informations;

namespace Mmu.SimapReader.Areas.Services
{
    public interface IUnzipService
    {
        Task<UnzipResult> UnzipAsync(
            InformationEntries infoEntries,
            string zipFilePath);
    }
}