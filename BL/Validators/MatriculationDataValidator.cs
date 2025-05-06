using FluentValidation;
using WebApplication1.Models;
using WebApplication1.DAL.Interfaces;

namespace WebApplication1.BL.Validators
{
    public class UpdateMatriculationDataRequestValidator : AbstractValidator<UpdateMatriculationDataRequest>
    {

        public UpdateMatriculationDataRequestValidator(ITModeratorDL _moderatorDL)
        {
            RuleFor(x => x.MorningTesters)
                .GreaterThan(0).WithMessage("MorningTesters must be greater than 0.");

            RuleFor(x => x.EveningTesters)
                .GreaterThan(0).WithMessage("EveningTesters must be greater than 0.");

            RuleFor(x => x.ModeratorId)
                .GreaterThan(0).WithMessage("ModeratorId is required.")
                .MustAsync(async (moderatorId, cancellation) =>
                {
                    if (_moderatorDL == null) return false;
                    return await _moderatorDL.IsModeratorExistsAsync(moderatorId, cancellation);
                })
           .WithMessage("ModeratorId does not exist.");

            RuleFor(x => x.LaboratoryRooms)
                .GreaterThan(0).WithMessage("LaboratoryRooms must be greater than 0.")
                .Must((model, labRooms) =>
            labRooms >= Math.Ceiling((model.MorningTesters + model.EveningTesters) / 20.0))
                .WithMessage("Number of laboratory rooms must be sufficient for up to 20 testers per room.");

            RuleFor(x => x.CoordinatorName)
                .NotEmpty().WithMessage("CoordinatorName is required.");

            RuleFor(x => x.CoordinatorEmail)
                .NotEmpty().WithMessage("CoordinatorEmail is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.CoordinatorPhone)
                .NotEmpty().WithMessage("CoordinatorPhone is required.")
                .Matches(@"^[0-9\-\+]{9,15}$").WithMessage("מספר טלפון לא תקין");

            RuleFor(x => x.AccompanyingTeachers)
                .NotNull().WithMessage("AccompanyingTeachers is required.")
                .Must((model, teachers) => teachers.Count == model.LaboratoryRooms)
                .WithMessage("The number of accompanying teachers must match the number of laboratory rooms.");
        }
    }
}
