using JetBrains.Annotations;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.Views.Interfaces;

namespace Mmu.SimapReader.Areas.Views.Overview
{
    [UsedImplicitly]
    public partial class ZipUploadView : IViewMap<OrchestrationViewModel>
    {
        public ZipUploadView()
        {
            InitializeComponent();
        }
    }
}