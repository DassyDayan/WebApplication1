namespace WebApplication1.Dto.Classes
{
    public class TModeratorDTO
    {
        public int IModeratorId { get; set; }
        public string NvFirstName { get; set; } = null!;
        public string NvLastName { get; set; } = null!;
        public string? NvRegion { get; set; }
        public int ISysRowStatus { get; set; }
    }
}
