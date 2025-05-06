using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models;

public partial class TMatriculation
{
    [Key]
    public int IMatriculationId { get; set; }

    public int IYearId { get; set; }

    public string NvMatriculationName { get; set; } = null!;

    public DateTime DtMatriculationDate { get; set; }

    public int IUnits { get; set; }

    public bool? BCreatedOrders { get; set; }

    public DateTime DtStudentsLastUpdateDate { get; set; }

    public int ICreateByUserId { get; set; }

    public DateTime DtCreateDate { get; set; }

    public int ILastModifyUserId { get; set; }

    public DateTime DtLastModifyDate { get; set; }

    public int ISysRowStatus { get; set; }
}