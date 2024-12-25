using JetBrains.Annotations;

namespace Mmu.SimapReader.Infrastructure.Settings.Provisioning.Models
{
    [PublicAPI]
    public class AppSettings
    {
        public const string SectionKey = "AppSettings";
        public string DocumentIntelligenceApiKey { get; set; } = default!;
        public string DocumentIntelligenceEndpoint { get; set; } = default!;
        public string TextAnalyticsApiKey { get; set; } = default!;
        public string TextAnalyticsEndpoint { get; set; } = default!;
    }
}