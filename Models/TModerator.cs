namespace WebApplication1.Models
{
    public class TModerator
    {
        public int IModeratorId { get; set; }

        public string NvFirstName { get; set; } = null!;

        public string NvLastName { get; set; } = null!;

        public string? NvRegion { get; set; }

        public string? NvEmail { get; set; }

        public string? NvPhone { get; set; }

        public string? NvMobile { get; set; }

        public string? NvDeliveryAddress { get; set; }

        public int? ICityType { get; set; }

        public int? IUserId { get; set; }

        public bool BIsActive { get; set; }

        public int? INumberBox { get; set; }

        public int ICreateByUserId { get; set; }

        public DateTime DtCreateDate { get; set; }

        public int ILastModifyUserId { get; set; }

        public DateTime DtLastModifyDate { get; set; }

        public int ISysRowStatus { get; set; }
    }
}