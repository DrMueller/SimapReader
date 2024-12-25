using JetBrains.Annotations;
using Lamar;
using Microsoft.Extensions.DependencyInjection;
using Mmu.SimapReader.Infrastructure.Settings.Config.Services;
using Mmu.SimapReader.Infrastructure.Settings.Provisioning.Models;

namespace Mmu.SimapReader.Infrastructure
{
    [UsedImplicitly]
    public class ServiceRegistryCollection : ServiceRegistry
    {
        public ServiceRegistryCollection()
        {
            Scan(
                scanner =>
                {
                    scanner.AssemblyContainingType<ServiceRegistryCollection>();
                    scanner.WithDefaultConventions();
                });

            var config = ConfigurationFactory.Create();
            this.Configure<AppSettings>(config.GetSection(AppSettings.SectionKey));
        }
    }
}