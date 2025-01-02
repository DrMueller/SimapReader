using System.Collections.ObjectModel;
using JetBrains.Annotations;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.CommandManagement.Components.CommandBars.ViewData;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.ViewModels;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.ViewModels.Behaviors;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.ViewModels.Services;
using Mmu.Mlh.WpfCoreExtensions.Areas.ViewExtensions.Grids.InformationGrids.ViewData;
using Mmu.SimapReader.Areas.Views.NerResults;
using Mmu.SimapReader.Infrastructure.Informations;

namespace Mmu.SimapReader.Areas.Views.Overview
{
    [PublicAPI]
    public class OrchestrationViewModel(
        IViewModelFactory vmFactory,
        CommandContainer commandContainer)
        : ViewModelBase, IInitializableViewModel, INavigatableViewModel
    {
        private string _selectedZipFile = string.Empty;

        public CommandsViewData Commands => commandContainer.Commands;

        public string HeadingDescription => "Zip Upload";
        public InformationEntries InfoEntries { get; } = new();

        public ObservableCollection<InformationGridEntryViewData> InformationEntries => InfoEntries.Collection;
        public string NavigationDescription => "Zip Upload";
        public int NavigationSequence => 1;

        public NerResultsViewModel NerResultsViewVm { get; private set; } = default!;

        public string SelectedZipFile
        {
            get => _selectedZipFile;
            set => OnPropertyChanged(value, ref _selectedZipFile);
        }

        public async Task InitializeAsync(params object[] initParams)
        {
            NerResultsViewVm = await vmFactory.CreateAsync<NerResultsViewModel>();
            await commandContainer.InitializeAsync(this);
        }
    }
}