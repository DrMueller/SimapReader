namespace Mmu.SimapReader.Areas.Models
{
    public record UnzipResult(
        bool WasSuccess,
        string UnzipFilePath);
}