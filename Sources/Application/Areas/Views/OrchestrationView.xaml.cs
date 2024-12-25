using System.Windows.Controls;
using Mmu.Mlh.WpfCoreExtensions.Areas.MvvmShell.Views.Interfaces;

namespace Mmu.SimapReader.Areas.Views
{
    /// <summary>
    ///     Interaction logic for ZipUploadView.xaml
    /// </summary>
    public partial class ZipUploadView : UserControl, IViewMap<OrchestrationViewModel>
    {
        public ZipUploadView()
        {
            InitializeComponent();
        }
    }
}