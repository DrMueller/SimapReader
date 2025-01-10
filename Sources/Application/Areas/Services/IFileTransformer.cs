using Mmu.SimapReader.Areas.Models;
using Mmu.SimapReader.Infrastructure.Informations;

namespace Mmu.SimapReader.Areas.Services
{
    public interface IFileTransformer
    {
        Task<IReadOnlyCollection<FileTransformations>> TransformFilesAsync(
            InformationEntries infoEntries,
            string documentsFilePath);
    }
}