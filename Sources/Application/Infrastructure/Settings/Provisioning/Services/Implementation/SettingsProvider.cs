using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Mmu.SimapReader.Infrastructure.Settings.Provisioning.Models;

namespace Mmu.SimapReader.Infrastructure.Settings.Provisioning.Services.Implementation
{
    [UsedImplicitly]
    public class SettingsProvider(
        IOptions<AppSettings> appSettings
    )
        : ISettingsProvider
    {
        public AppSettings AppSettings => appSettings.Value;
    }
}