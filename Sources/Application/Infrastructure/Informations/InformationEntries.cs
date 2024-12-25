using System.Collections.ObjectModel;
using Mmu.Mlh.WpfCoreExtensions.Areas.ViewExtensions.Grids.InformationGrids.ViewData;

namespace Mmu.SimapReader.Infrastructure.Informations
{
    public class InformationEntries
    {
        public ObservableCollection<InformationGridEntryViewData> Collection { get; } = new();

        public void Add(string value)
        {
            Collection.Insert(0, new InformationGridEntryViewData(value));
        }

        public void Clear()
        {
            Collection.Clear();
        }
    }
}