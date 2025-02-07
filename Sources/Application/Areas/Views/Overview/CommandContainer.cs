using JetBrains.Annotations;
using Mmu.Mlh.WpfCoreExtensions.Areas.Aspects.ApplicationInformations.Models;
using Mmu.Mlh.WpfCoreExtensions.Areas.Aspects.ApplicationInformations.Services;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.CommandManagement.Commands;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.CommandManagement.Components.CommandBars.ViewData;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.CommandManagement.ViewModelCommands;
using Mmu.SimapReader.Areas.Services;

namespace Mmu.SimapReader.Areas.Views.Overview
{
    [UsedImplicitly]
    public class CommandContainer(
        IOrchestrator orchestrator,
        IFileDownloader fileDownloader,
        IInformationPublisher infoPublisher,
        ITextFileFactory textFileFactory)
        : IViewModelCommandContainer<OrchestrationViewModel>
    {
        private OrchestrationViewModel _context = default!;

        private bool _isRunning;

        public CommandsViewData Commands { get; private set; } = default!;

        private IViewModelCommand CreateTextFiles
        {
            get
            {
                return new ViewModelCommand(
                    "Create Text files",
                    new AsyncRelayCommand(
                        async () => { await textFileFactory.CreateAllAsync(_context.InfoEntries); }
                    ));
            }
        }

        private IViewModelCommand DownloadLogs
        {
            get
            {
                return new ViewModelCommand(
                    "Download Logs",
                    new AsyncRelayCommand(
                        async () => { await fileDownloader.DownloadLogsAsync(_context.InfoEntries); },
                        () => _context.InfoEntries.Collection.Any() && !_isRunning));
            }
        }

        private IViewModelCommand DownloadNerResults
        {
            get
            {
                return new ViewModelCommand(
                    "Download results",
                    new AsyncRelayCommand(
                        async () => { await fileDownloader.DownloadResultsAsync(_context.NerResultsViewVm.Result); },
                        () => _context.NerResultsViewVm.Result.Any() && !_isRunning));
            }
        }

        private IViewModelCommand StartRecognition
        {
            get
            {
                return new ViewModelCommand(
                    "Start",
                    new AsyncRelayCommand(
                        async () =>
                        {
                            _isRunning = true;
                            infoPublisher.Publish(InformationEntry.CreateInfo("Work work, okey dokey..", true));
                            _context.InfoEntries.Clear();
                            _context.NerResultsViewVm.Result = await orchestrator.ProcessAsync(
                                _context.InfoEntries,
                                _context.SelectedZipFile);
                            infoPublisher.Publish(InformationEntry.CreateInfo("Work's done", false, 10));
                            _isRunning = false;
                        }, () => !string.IsNullOrEmpty(_context.SelectedZipFile) && !_isRunning));
            }
        }

        public Task InitializeAsync(OrchestrationViewModel context)
        {
            _context = context;

            Commands = new CommandsViewData(
                CreateTextFiles,
                StartRecognition,
                DownloadLogs,
                DownloadNerResults);

            return Task.CompletedTask;
        }
    }
}