using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.CommandManagement.Commands;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.CommandManagement.Components.CommandBars.ViewData;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.CommandManagement.ViewModelCommands;
using Mmu.SimapReader.Areas.Services;

namespace Mmu.SimapReader.Areas.Views
{
    public class CommandContainer : IViewModelCommandContainer<OrchestrationViewModel>
    {
        private OrchestrationViewModel _context = default!;
        private readonly IOrchestrator _orchestrator;

        public CommandContainer(
            IOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        public CommandsViewData Commands { get; private set; }

        private IViewModelCommand StartRecognition
        {
            get
            {
                return new ViewModelCommand(
                    "Start",
                    new AsyncRelayCommand(
                        async () =>
                        {
                            _context.InfoEntries.Clear();
                            await _orchestrator.ProcessAsync(
                                _context.InfoEntries,
                                _context.SelectedZipFile);
                        }, () => !string.IsNullOrEmpty(_context.SelectedZipFile)));
            }
        }

        public Task InitializeAsync(OrchestrationViewModel context)
        {
            _context = context;

            Commands = new CommandsViewData(StartRecognition);

            return Task.CompletedTask;
        }
    }
}