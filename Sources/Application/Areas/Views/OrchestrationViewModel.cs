using System.Collections.ObjectModel;
using JetBrains.Annotations;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.CommandManagement.Components.CommandBars.ViewData;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.ViewModels;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.ViewModels.Behaviors;
using Mmu.Mlh.WpfCoreExtensions.Areas.ViewExtensions.Grids.InformationGrids.ViewData;
using Mmu.SimapReader.Infrastructure.Informations;

namespace Mmu.SimapReader.Areas.Views
{
    [UsedImplicitly]
    public class OrchestrationViewModel : ViewModelBase, IInitializableViewModel, INavigatableViewModel
    {
        private readonly CommandContainer _commandContainer;

        private string _selectedZipFile;

        public OrchestrationViewModel(CommandContainer commandContainer)
        {
            _commandContainer = commandContainer;
        }

        public CommandsViewData Commands => _commandContainer.Commands;

        public string HeadingDescription => "Zip Upload";
        public InformationEntries InfoEntries { get; } = new();

        public ObservableCollection<InformationGridEntryViewData> InformationEntries => InfoEntries.Collection;
        public string NavigationDescription => "Zip Upload";
        public int NavigationSequence => 1;

        public string SelectedZipFile
        {
            get => _selectedZipFile;
            set => OnPropertyChanged(value, ref _selectedZipFile);
        }

        public async Task InitializeAsync(params object[] initParams)
        {
            await _commandContainer.InitializeAsync(this);
        }
    }
}