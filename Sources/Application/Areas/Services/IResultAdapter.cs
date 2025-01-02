using Mmu.SimapReader.Areas.Models;
using Mmu.SimapReader.Areas.ViewModels;

namespace Mmu.SimapReader.Areas.Services
{
    public interface IResultAdapter
    {
        IReadOnlyCollection<EntityRecognitionResultEntryViewModel> Adapt(
            EntityRecognitionResult recognitionResult);
    }
}