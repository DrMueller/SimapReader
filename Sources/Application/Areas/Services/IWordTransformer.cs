using Mmu.SimapReader.Areas.Models;
using Mmu.SimapReader.Infrastructure.Informations;

namespace Mmu.SimapReader.Areas.Services
{
    public interface IWordTransformer
    {
        Task<IReadOnlyCollection<WordTransformations>> TransformWordsAsyncs(
            InformationEntries infoEntries,
            string wordsFilePath);
    }
}