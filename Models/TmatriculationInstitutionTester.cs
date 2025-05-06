using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public partial class TMatriculationInstitutionTester
{
    [Key]
    public int IMatriculationInstitutionTesterId { get; set; }

    public int IMatriculationInstitutionId { get; set; }

    public string NvTesterName { get; set; } = null!;

    public int ICreateByUserId { get; set; }

    public DateTime DtCreateDate { get; set; } = DateTime.Now;

    public int ILastModifyUserId { get; set; }

    public DateTime DtLastModifyDate { get; set; } = DateTime.Now;

    public int ISysRowStatus { get; set; }
}