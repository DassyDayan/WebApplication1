using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public partial class TMatriculationParams
{
    [Key]
    public int IMatriculationParamId { get; set; }

    public int IMatriculationId { get; set; }

    public string NvParamName { get; set; } = null!;

    public int IParamType { get; set; }

    public int ICreateByUserId { get; set; }

    public DateTime DtCreateDate { get; set; }

    public int ILastModifyUserId { get; set; }

    public DateTime DtLastModifyDate { get; set; }

    public int ISysRowStatus { get; set; }
}