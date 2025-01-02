using JetBrains.Annotations;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.ViewModels;
using Mmu.SimapReader.Areas.ViewModels;

namespace Mmu.SimapReader.Areas.Views.NerResults
{
    [UsedImplicitly]
    public class NerResultsViewModel : ViewModelBase
    {
        private IReadOnlyCollection<EntityRecognitionResultEntryViewModel> _result = new
            List<EntityRecognitionResultEntryViewModel>();

        public IReadOnlyCollection<EntityRecognitionResultEntryViewModel> Result
        {
            get => _result;
            set => OnPropertyChanged(value, ref _result);
        }
    }
}