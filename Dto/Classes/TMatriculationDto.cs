using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto.Classes
{
    public class TMatriculationDto
    {
        [Key]
        public int IMatriculationId { get; set; }
        public string NvMatriculationName { get; set; } = null!;
        public DateTime DtMatriculationDate { get; set; }
        public DateTime DtStudentsLastUpdateDate { get; set; }

    }
}
