using Mmu.SimapReader.Infrastructure.Settings.Provisioning.Models;

namespace Mmu.SimapReader.Infrastructure.Settings.Provisioning.Services
{
    public interface ISettingsProvider
    {
        AppSettings AppSettings { get; }
    }
}