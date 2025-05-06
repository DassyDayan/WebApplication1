using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public partial class TMatriculationInstitution
{
    [Key]
    public int IMatriculationInstitutionId { get; set; }

    public int IInstitutionId { get; set; }

    public int IMatriculationId { get; set; }

    public int? IRegistrationType { get; set; }

    public int IDeliveryType { get; set; }

    public int? IDeliveryModeratorId { get; set; }

    public int? ILaboratoryRooms { get; set; }

    public int ICreateByUserId { get; set; }

    public DateTime DtCreateDate { get; set; } = DateTime.Now;

    public int? ILastModifyUserId { get; set; }

    public DateTime DtLastModifyDate { get; set; } = DateTime.Now;

    public int ISysRowStatus { get; set; }
}