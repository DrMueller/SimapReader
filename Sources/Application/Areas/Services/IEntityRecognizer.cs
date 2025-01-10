using Mmu.SimapReader.Areas.Models;
using Mmu.SimapReader.Infrastructure.Informations;

namespace Mmu.SimapReader.Areas.Services
{
    public interface IEntityRecognizer
    {
        Task<EntityRecognitionResult> RecognizeAsync(
            InformationEntries infoEntries,
            IReadOnlyCollection<FileTransformations> transformations);
    }
}