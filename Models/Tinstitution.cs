namespace WebApplication1.Models;
using System.ComponentModel.DataAnnotations;

public partial class TInstitution
{
    [Key]
    public int IInstitutionId { get; set; }

    public string? NvOwnership { get; set; }

    public string? NvSchoolName { get; set; }

    public string? NvSchoolSymbol { get; set; }

    public string? NvBiologyCoordinatorName { get; set; }

    public string? NvCoordinatorPhone { get; set; }

    public string? NvCoordinatorMail { get; set; }

    public string? NvResponsibleDistributionName { get; set; }

    public string? NvResponsibleDistributionPhone { get; set; }

    public string? NvProvincialAdvisorName { get; set; }

    public string? NvProvincialAdvisorPhone { get; set; }

    public int? INumTestedMorning5Units { get; set; }

    public int? INumTestedNoon5Units { get; set; }

    public int? INumTestedAfterNoon5Units { get; set; }

    public int? INumTested3Units { get; set; }

    public int? INumUnits { get; set; }

    public bool? BSelfPickup { get; set; }

    public int? ISchoolType { get; set; }

    public string? NvComments { get; set; }

    public int? ICityType { get; set; }

    public int? IZipCode { get; set; }

    public string? NvAddress { get; set; }

    public int? IEducationLevel { get; set; }

    public string? NvPhone { get; set; }

    public string? NvMobile { get; set; }

    public string? NvFax { get; set; }

    public string? NvSecretariatMail { get; set; }

    public int? IDeliveryCityType { get; set; }

    public string? NvDeliveryAddress { get; set; }

    public int? IDeliveryZipCode { get; set; }

    public bool BIsDeliveryByCourier { get; set; }

    public int? INumStudents { get; set; }

    public int? IDaysWaitingPeriodForPayment { get; set; }

    public int? ICreatedByUserId { get; set; }

    public DateTime? DtCreateDate { get; set; }

    public int? ILastModifyUserId { get; set; }

    public DateTime? DtLastModifyDate { get; set; }

    public int? ISysRowStatus { get; set; }

    public bool? BWinOlympiad { get; set; }

    public int? IEducationId { get; set; }

    //public virtual Teducation? IEducation { get; set; }
}
