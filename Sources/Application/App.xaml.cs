using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Mmu.Mlh.WpfCoreExtensions.Areas.Initialization.Orchestration.Models;
using Mmu.Mlh.WpfCoreExtensions.Areas.Initialization.Orchestration.Services;

namespace Mmu.SimapReader
{
    public partial class App
    {
        [SuppressMessage("Usage", "VSTHRD100:Avoid async void methods", Justification = "WPF expects void")]
        protected override async void OnStartup(StartupEventArgs e)
        {
            var assembly = typeof(App).Assembly;
            var windowConfig = WindowConfiguration.CreateWithDefaultIcon(assembly, "SIMAP Reader", 620, 600);

            await AppStartService.StartAppAsync(new WpfAppConfiguration(assembly, windowConfig, false));
        }
    }
}